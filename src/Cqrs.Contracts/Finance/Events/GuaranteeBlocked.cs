using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Finance.Events
{
    public class GuaranteeBlocked : Event
    {
        public GuaranteeBlocked(Guid workflowId) 
            : base(workflowId)
        {
        }
    }
}