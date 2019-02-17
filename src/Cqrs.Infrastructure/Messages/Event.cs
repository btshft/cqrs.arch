using System;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс события.
    /// </summary>
    public abstract class Event : Message, IEvent 
    {
        protected Event(Guid workflowId) 
            : base(workflowId)
        {
        }
    }
}