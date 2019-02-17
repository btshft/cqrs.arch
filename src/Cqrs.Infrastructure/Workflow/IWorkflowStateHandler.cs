using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Обработчик состояния процесса.
    /// </summary>
    /// <typeparam name="TWorkflowEvent">Тип события, инициировавший переход состояния.</typeparam>
    /// <typeparam name="TWorkflow">Процесс.</typeparam>
    public interface IWorkflowStateHandler<in TWorkflowEvent, TWorkflow>
        where TWorkflowEvent : class, IEvent
        where TWorkflow : class, IWorkflow
    {
        /// <summary>
        /// Выполняет обработку перехода процесса.
        /// </summary>
        /// <param name="event">Событие.</param>
        /// <param name="envelope">Процесс.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        Task ProcessAsync(TWorkflowEvent @event, WorkflowEnvelope<TWorkflow> envelope, CancellationToken cancellation);
    }
}