using System;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс сообщения.
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <inheritdoc />
        public Guid Id { get; }

        /// <inheritdoc />
        public Guid CorrelationId { get; }

        /// <inheritdoc />
        public DateTime Timestamp { get; }

        protected Message(Guid correlationId)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            CorrelationId = correlationId;
        }
        
        protected Message() : this(Guid.NewGuid())
        { }
    }
}