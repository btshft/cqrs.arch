using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.AppServices.Application.Events;
using Cqrs.Domain;
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
        ICommandHandler<SubmitApplicationCommand>,
        ICommandHandler<WithdrawApplicationCommand>
    {
        private readonly IRepository<Domain.Application> _applicationRepository;

        public ApplicationCommandsHandler(IRepository<Domain.Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(CreateApplicationDraftCommand command, CancellationToken cancellationToken)
        {
            var entity = new Domain.Application
            {
                Status = ApplicationStatus.Draft,
                GuaranteeWorkflowId = Guid.NewGuid()
            };

            await _applicationRepository.AddAsync(entity, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            command.EnqueueOutputEvent(new ApplicationDraftCreatedEvent(entity));
            
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
            
            command.EnqueueOutputEvent(new ApplicationSubmittedEvent(application));
            
            return Unit.Value;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(WithdrawApplicationCommand command, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetAsync(command.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (application == null)
                throw new UserException("Заявка не найдена", "Не удалось найти заявку для утверждения");

            application.Status = ApplicationStatus.Withdrawn;
            
            command.EnqueueOutputEvent(new ApplicationWithdrawnEvent(application));
            
            return Unit.Value;
        }
    }
}