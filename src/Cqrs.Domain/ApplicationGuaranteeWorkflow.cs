using System;
using Cqrs.Infrastructure.Workflow;

namespace Cqrs.Domain
{
    public class ApplicationGuaranteeWorkflow : IWorkflow
    {
        public Guid WorkflowId { get; set; }
        public bool IsCompleted { get; set; }
        
        public ApplicationGuaranteeState GuaranteeState { get; set; }
    }
}