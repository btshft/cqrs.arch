using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Events
{
    public class ApplicationDraftCreatedEvent : Event
    {
        public int ApplicationId { get; }
        
        public ApplicationDraftCreatedEvent(Domain.Application application) 
            : base(application.GuaranteeWorkflowId)
        {
            ApplicationId = application.Id;
        }
    }
}