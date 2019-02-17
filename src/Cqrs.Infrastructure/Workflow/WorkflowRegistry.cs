using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Dispatcher;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Реестр процессов.
    /// </summary>
    /// <typeparam name="TWorkflow">Тип процесса.</typeparam>
    public abstract class WorkflowRegistry<TWorkflow> : IWorkflowRegistry<TWorkflow> 
        where TWorkflow : IWorkflow
    {
        /// <summary>
        /// Обработчик команд.
        /// </summary>
        protected IMessageDispatcher Dispatcher { get; }
        
        protected WorkflowRegistry(IMessageDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }
        
        /// <inheritdoc />
        public async Task<TWorkflow> FindAsync(Guid id, CancellationToken cancellation)
        {
            var workflow = await FindCoreAsync(id, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (workflow.OutputCommands != null)
            {
                while (workflow.OutputCommands.TryDequeue(out var command))
                {
                    command.WorkflowId = workflow.WorkflowId;
                        
                    await Dispatcher.DispatchCommandAsync(command, cancellation)
                        .ConfigureAwait(continueOnCapturedContext: false);
                }
            }

            return workflow;
        }

        protected abstract Task<TWorkflow> FindCoreAsync(Guid id, CancellationToken cancellation);
        
        /// <inheritdoc />
        public async Task PersistAsync(TWorkflow workflow, CancellationToken cancellation)
        {
            try
            {
                await PersistCoreAsync(workflow, cancellation)
                    .ConfigureAwait(continueOnCapturedContext: false);
      
                if (workflow.OutputCommands != null)
                {
                    while (workflow.OutputCommands.TryDequeue(out var command))
                    {
                        command.WorkflowId = workflow.WorkflowId;
                        
                        await Dispatcher.DispatchCommandAsync(command, cancellation)
                            .ConfigureAwait(continueOnCapturedContext: false);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Не удалось сохранить процесс '{typeof(TWorkflow)}'", e);
            }
        }

        protected abstract Task PersistCoreAsync(TWorkflow stateMachine, CancellationToken cancellation);
    }
}