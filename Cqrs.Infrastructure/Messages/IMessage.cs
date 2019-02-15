using System;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Интерфейс сообщения.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Уникальный идентификатор сообщения.
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Идентификатор потока обмена.
        /// </summary>
        Guid CorrelationId { get; }
        
        /// <summary>
        /// Отметка времени создания сообщения (UTC).
        /// </summary>
        DateTime Timestamp { get; }
    }
}