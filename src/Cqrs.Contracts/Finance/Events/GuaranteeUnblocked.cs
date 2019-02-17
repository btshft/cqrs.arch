using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Finance.Events
{
    public class GuaranteeUnblocked : Event
    {
        public GuaranteeUnblocked(Guid workflowId) 
            : base(workflowId)
        {
        }
    }
}