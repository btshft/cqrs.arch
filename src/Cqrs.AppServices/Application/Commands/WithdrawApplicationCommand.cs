using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Commands
{
    /// <summary>
    /// Команда отзыва заявки.
    /// </summary>
    [Transactional]
    public class WithdrawApplicationCommand : Command
    {
        public WithdrawApplicationCommand(int applicationId)
        {
            ApplicationId = applicationId;
        }

        public int ApplicationId { get; }
    }
}