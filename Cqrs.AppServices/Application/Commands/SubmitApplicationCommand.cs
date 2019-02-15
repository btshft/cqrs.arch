using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Commands
{
    public class SubmitApplicationCommand : Command
    {
        public int ApplicationId { get; }
        
        public SubmitApplicationCommand(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}