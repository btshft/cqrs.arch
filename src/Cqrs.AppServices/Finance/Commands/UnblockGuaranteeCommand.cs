using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Finance.Commands
{
    public class UnblockGuaranteeCommand : Command
    {
        public int ApplicationId { get; }
        
        public UnblockGuaranteeCommand(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}