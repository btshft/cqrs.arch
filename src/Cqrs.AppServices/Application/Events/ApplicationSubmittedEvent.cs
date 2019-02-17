using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Events
{
    /// <summary>
    /// Событие утверждения заявки.
    /// </summary>
    public class ApplicationSubmittedEvent : Event
    {
        public ApplicationSubmittedEvent(Domain.Application application)
            : base(application.GuaranteeWorkflowId)
        {
            ApplicationId = application.Id;
        }

        public int ApplicationId { get; }
    }
}