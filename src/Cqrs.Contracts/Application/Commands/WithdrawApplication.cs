using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Application.Commands
{
    /// <summary>
    /// Команда отзыва заявки.
    /// </summary>
    public class WithdrawApplication : Command
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int ApplicationId { get; set; }
    }
}