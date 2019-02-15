using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Events
{
    /// <summary>
    /// Событие утверждения заявки.
    /// </summary>
    public class ApplicationSubmittedEvent : Event
    {
        public ApplicationSubmittedEvent(int applicationId)
        {
            ApplicationId = applicationId;
        }

        public int ApplicationId { get; }
    }
}