using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Events
{
    /// <summary>
    /// Событие отзыва заявки.
    /// </summary>
    public class ApplicationWithdrawnEvent : Event
    {
        public ApplicationWithdrawnEvent(Domain.Application application)
            : base(application.GuaranteeWorkflowId)
        {
            ApplicationId = application.Id;
        }
        
        public int ApplicationId { get; }
    }
}