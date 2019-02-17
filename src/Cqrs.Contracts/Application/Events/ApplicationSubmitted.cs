using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Application.Events
{
    /// <summary>
    /// Событие утверждения заявки.
    /// </summary>
    public class ApplicationSubmitted : Event
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int ApplicationId { get; }

        public ApplicationSubmitted(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}