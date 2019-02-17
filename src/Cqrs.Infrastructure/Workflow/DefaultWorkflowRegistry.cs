using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Dispatcher;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Стандартная реализация реестра с использованием сохранения в репозиторий.
    /// </summary>
    /// <typeparam name="TWorkflow">Процесс.</typeparam>
    public class DefaultWorkflowRegistry<TWorkflow> : WorkflowRegistry<TWorkflow> 
        where TWorkflow : class, IWorkflow
    {
        protected IRepository<TWorkflow> WorkflowRepository { get; }
        protected IDataSessionFactory DataSessionFactory { get; }
              
        public DefaultWorkflowRegistry(
            IMessageDispatcher dispatcher, 
            IRepository<TWorkflow> workflowRepository, 
            IDataSessionFactory dataSessionFactory) 
            : base(dispatcher)
        {
            WorkflowRepository = workflowRepository;
            DataSessionFactory = dataSessionFactory;
        }

        /// <inheritdoc />
        public override async Task<TWorkflow> FindCoreAsync(Guid id, CancellationToken cancellation)
        {
            return await WorkflowRepository.GetAsync(id)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        protected override async Task PersistCoreAsync(WorkflowEnvelope<TWorkflow> envelope, CancellationToken cancellation)
        {
            using (var session = DataSessionFactory.CreateDataSession())
            {
                var existingWorkflow = await FindCoreAsync(envelope.Workflow.WorkflowId, cancellation)
                    .ConfigureAwait(continueOnCapturedContext: false);

                if (existingWorkflow != null)
                    await WorkflowRepository.UpdateAsync(envelope.Workflow, cancellation)
                        .ConfigureAwait(continueOnCapturedContext: false);
                else
                    await WorkflowRepository.AddAsync(envelope.Workflow, cancellation)
                        .ConfigureAwait(continueOnCapturedContext: false);

                await session.CommitAsync(cancellation)
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
        }
    }
}