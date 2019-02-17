using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Application.Commands
{
    /// <summary>
    /// Команда утверждения заявки.
    /// </summary>
    public class SubmitApplication : Command
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int ApplicationId { get; set; }       
    }
}