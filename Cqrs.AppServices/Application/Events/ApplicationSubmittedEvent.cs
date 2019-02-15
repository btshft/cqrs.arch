using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Events
{
    public class ApplicationSubmittedEvent : Event
    {
        public ApplicationSubmittedEvent(int applicationId)
        {
            ApplicationId = applicationId;
        }

        public int ApplicationId { get; }
    }
}