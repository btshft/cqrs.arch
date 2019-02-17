using System;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс события.
    /// </summary>
    public abstract class Event : Message, IEvent 
    {
        /// <inheritdoc />
        public Guid WorkflowId { get; }
        
        protected Event(Guid workflowId)
        {
            WorkflowId = workflowId;
        }
    }
}