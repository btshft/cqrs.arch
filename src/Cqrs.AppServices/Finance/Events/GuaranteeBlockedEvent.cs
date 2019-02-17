using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Finance.Events
{
    public class GuaranteeBlockedEvent : Event
    {
        public GuaranteeBlockedEvent(Guid workflowId) 
            : base(workflowId)
        {
        }
    }
}