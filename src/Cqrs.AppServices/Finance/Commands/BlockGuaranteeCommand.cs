using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Finance.Commands
{
    public class BlockGuaranteeCommand : Command
    {
        public int ApplicationId { get; }

        public BlockGuaranteeCommand(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}