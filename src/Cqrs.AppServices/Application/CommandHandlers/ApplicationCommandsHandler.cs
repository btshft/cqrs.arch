using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.AppServices.Application.Events;
using Cqrs.Domain.Models;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Exceptions;
using Cqrs.Infrastructure.Handlers;
using MediatR;

namespace Cqrs.AppServices.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команд по заявке.
    /// </summary>
    public class ApplicationCommandsHandler : 
        ICommandHandler<CreateApplicationDraftCommand>,
        ICommandHandler<SubmitApplicationCommand>
    {
        private readonly IRepository<Domain.Models.Application> _applicationRepository;

        public ApplicationCommandsHandler(IRepository<Domain.Models.Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(CreateApplicationDraftCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Models.Application
            {
                Status = ApplicationStatus.Draft
            };

            await _applicationRepository.AddAsync(entity, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Unit.Value;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(SubmitApplicationCommand command, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetAsync(command.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (application == null)
                throw new UserException("Заявка не найдена", "Не удалось найти заявку для утверждения");
            
            application.Status = ApplicationStatus.Submitted;
            
            command.EnqueueOutputEvent(new ApplicationSubmittedEvent(application.Id));
            
            return Unit.Value;
        }
    }
}