using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Application.Events
{
    /// <summary>
    /// Событие отзыва заявки.
    /// </summary>
    public class ApplicationWithdrawn : Event
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int ApplicationId { get; }

        public ApplicationWithdrawn(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}