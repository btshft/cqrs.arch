using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Finance.Events
{
    public class GuaranteeUnblockedEvent : Event
    {
        public GuaranteeUnblockedEvent(Guid workflowId) 
            : base(workflowId)
        {
        }
    }
}