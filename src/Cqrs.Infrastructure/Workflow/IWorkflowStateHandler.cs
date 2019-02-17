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
    public interface IWorkflowStateHandler<in TWorkflowEvent, in TWorkflow>
        where TWorkflowEvent : IEvent
        where TWorkflow : IWorkflow
    {
        /// <summary>
        /// Выполняет обработку перехода процесса.
        /// </summary>
        /// <param name="event">Событие.</param>
        /// <param name="workflow">Процесс.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        Task ProcessAsync(TWorkflowEvent @event, TWorkflow workflow, CancellationToken cancellation);
    }
}