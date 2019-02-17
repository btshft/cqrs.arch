using System;
using MediatR;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Интерфейс события.
    /// </summary>
    public interface IEvent : IMessage, INotification
    {
        /// <summary>
        /// Идентификатор процесса.
        /// </summary>
        Guid WorkflowId { get; }
    }
}