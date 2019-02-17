using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Application.Events
{
    /// <summary>
    /// Событие создания черновика заявки.
    /// </summary>
    public class ApplicationDraftCreated : Event
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int ApplicationId { get; }

        public ApplicationDraftCreated(Guid workflowId, int applicationId) 
            : base(workflowId)
        {
            ApplicationId = applicationId;
        }
    }
}