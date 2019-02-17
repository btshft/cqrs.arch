using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Finance.Commands
{
    public class BlockGuarantee : Command
    {
        public int ApplicationId { get; }

        public BlockGuarantee(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}