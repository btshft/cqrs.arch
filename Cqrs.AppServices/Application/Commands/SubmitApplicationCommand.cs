using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Commands
{
    /// <summary>
    /// Команда утверждения заявки.
    /// </summary>
    [Transactional]
    public class SubmitApplicationCommand : Command
    {
        public int ApplicationId { get; }
        
        public SubmitApplicationCommand(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}