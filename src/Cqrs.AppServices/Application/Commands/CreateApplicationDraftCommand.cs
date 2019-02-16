using Cqrs.Contracts.Application;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Commands
{
    /// <summary>
    /// Команда создания черновика заявки.
    /// </summary>
    [Transactional]
    public class CreateApplicationDraftCommand : Command
    {
        public CreateApplicationDraftCommand(ApplicationDraftCreationDto creation)
        {
            ApplicationCreation = creation;
        }
        
        public ApplicationDraftCreationDto ApplicationCreation { get; }
    }
}