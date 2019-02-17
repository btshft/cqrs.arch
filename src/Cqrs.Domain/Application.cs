using System;

namespace Cqrs.Domain
{
    public class Application
    {
        public int Id { get; set; }
        public ApplicationStatus Status { get; set; }
        public Guid GuaranteeWorkflowId { get; set; }
    }
}