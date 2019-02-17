using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Finance.Commands
{
    public class UnblockGuarantee : Command
    {
        public int ApplicationId { get; }
        
        public UnblockGuarantee(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}