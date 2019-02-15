using Cqrs.Contracts.Application;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Commands
{
    public class CreateApplicationDraftCommand : Command
    {
        public CreateApplicationDraftCommand(ApplicationDto application)
        {
            Application = application;
        }

        public ApplicationDto Application { get; }
    }
}