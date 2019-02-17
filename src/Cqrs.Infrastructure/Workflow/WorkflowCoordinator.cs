using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Handlers;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Координатор процесса.
    /// </summary>
    /// <typeparam name="TInitial">Начальное событие, инициирующее выполнение процесса.</typeparam>
    /// <typeparam name="TWorkflow">Процесс.</typeparam>
    public abstract class WorkflowCoordinator<TInitial, TWorkflow> : 
        IWorkflowCoordinator<TWorkflow>, 
        IEventHandler<IEvent>, 
        IWorkflowStateHandler<TInitial, TWorkflow>
            where TInitial : class, IEvent 
            where TWorkflow : class, IWorkflow
    {
        /// <summary>
        /// Реестр процесса.
        /// </summary>
        protected IWorkflowRegistry<TWorkflow> Registry { get; }
        
        /// <summary>
        /// Текущий активный процесс.
        /// </summary>
        protected WorkflowEnvelope<TWorkflow> Envelope { get; private set; }
        
        protected Lazy<IReadOnlyCollection<Type>> SupportedEventTypes { get; }
        
        protected WorkflowCoordinator(IWorkflowRegistry<TWorkflow> registry)
        {
            Registry = registry;
            SupportedEventTypes = new Lazy<IReadOnlyCollection<Type>>(GetSupportedEventTypes);
        }
                 
        /// <inheritdoc />
        public async Task Handle(IEvent @event, CancellationToken cancellationToken)
        {
            if (!SupportedEventTypes.Value.Contains(@event.GetType()))
                return;
      
            if (Envelope == null)
            {
                if (@event is TInitial initial)
                {
                    await ProcessAsync(initial, envelope: null, cancellationToken)
                        .ConfigureAwait(continueOnCapturedContext: false);
                    
                    return;
                }

                var workflow = await Registry.FindAsync(@event.WorkflowId, cancellationToken)
                    .ConfigureAwait(continueOnCapturedContext: false);
                    
                if (workflow == null)
                    throw new InvalidOperationException(
                        $"Не удалось получить процесс '{typeof(TWorkflow)}' для события '{@event}'");
                    
                Envelope = WorkflowEnvelope.Create(workflow);
            }
            
            if (Envelope.Workflow.IsCompleted || 
                Envelope.Workflow.WorkflowId != @event.WorkflowId)
                return;
            
            var methodRef = typeof(WorkflowCoordinator<TInitial, TWorkflow>)
                .GetMethod(nameof(DispatchWorkflowState), BindingFlags.Instance | BindingFlags.NonPublic);
            
            var method = methodRef.MakeGenericMethod(@event.GetType());

            await ((Task) method.Invoke(this, new object[] { @event, Envelope, cancellationToken }))
                .ConfigureAwait(continueOnCapturedContext: false);

            await Registry.PersistAsync(Envelope, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task ProcessAsync(TInitial @event, WorkflowEnvelope<TWorkflow> envelope, CancellationToken cancellation)
        {
            if (Envelope != null)
                return;
            
            var existingWorkflow = await Registry.FindAsync(@event.WorkflowId, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            if (existingWorkflow != null)
            {
                Envelope = WorkflowEnvelope.Create(existingWorkflow);
                return;
            }
            
            await InitAsync(@event, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            await Registry.PersistAsync(Envelope, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Инициирует начало процесса.
        /// </summary>
        /// <param name="workflow">Процесс.</param>
        /// <param name="event">Инициирующее событие.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        protected virtual async Task InitAsync(TInitial @event, CancellationToken cancellation)
        {
            var workflow = await CreateWorkflowAsync(@event, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            if (workflow == null)
                throw new InvalidOperationException($"Не удалось создать процесс типа '{typeof(TWorkflow)}' для события: {@event}");
            
            Envelope = new WorkflowEnvelope<TWorkflow>(workflow);
        }
        
        /// <summary>
        /// Создает новый экземпляр процесса. Вызывается на этапе инициализации (при получении события <see cref="TInitial"/>).
        /// </summary>
        /// <param name="event">Инициирующее событие.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        /// <returns>Экземпляр процесса.</returns>
        protected abstract Task<TWorkflow> CreateWorkflowAsync(TInitial @event, CancellationToken cancellation);

        private Task DispatchWorkflowState<TEvent>(TEvent @event, WorkflowEnvelope<TWorkflow> envelope, CancellationToken cancellation) 
            where TEvent : class, IEvent
        {
            if (this is IWorkflowStateHandler<TEvent, TWorkflow> processState)
                return processState.ProcessAsync(@event, envelope, cancellation);
            
            throw new InvalidOperationException(
                $"Шаг процесса типа {typeof(IWorkflowStateHandler<TEvent, TWorkflow>)} не поддерживается");
        }
        
        private IReadOnlyCollection<Type> GetSupportedEventTypes()
        {
            return GetType().GetInterfaces()
                .Where(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IWorkflowStateHandler<,>))
                .Where(i => i.GenericTypeArguments[1] == typeof(TWorkflow))
                .Select(i => i.GetGenericArguments()[0])
                .ToList();
        }
    }
}