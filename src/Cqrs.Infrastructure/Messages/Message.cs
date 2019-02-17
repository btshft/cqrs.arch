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
        public Guid? WorkflowId { get; set; }

        /// <inheritdoc />
        public DateTime Timestamp { get; }

        protected Message(Guid workflowId)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            WorkflowId = workflowId;
        }

        protected Message()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var separator = $";{Environment.NewLine}";
            return $"Type: {GetType()}{separator} Id: {Id}{separator} WorkflowId: {WorkflowId}{separator} Timestamp: {Timestamp:u}";
        }
    }
}