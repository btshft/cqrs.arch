using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Интерфейс команды.
    /// </summary>
    public interface ICommand : IMessage, IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор процесса.
        /// </summary>
        Guid? WorkflowId { get; set; }
        
        /// <summary>
        /// Признак наличия исходящих событий.
        /// </summary>
        bool HasOutputEvents { get; }
        
        /// <summary>
        /// Добавляет исходящее событие в очередь для обработки.
        /// </summary>
        /// <param name="event">Событие.</param>
        void EnqueueOutputEvent(Event @event);
        
        /// <summary>
        /// Выполняет обработку событий. Деструктивная операция.
        /// </summary>
        /// <param name="eventHandler">Обработчик событий.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        Task ProcessOutputEventsAsync(Func<Event, CancellationToken, Task> eventHandler, CancellationToken cancellation);
    }
}