using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Contracts.Application.Commands;
using Cqrs.Contracts.Application.Events;
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
        ICommandHandler<CreateApplicationDraft>,
        ICommandHandler<SubmitApplication>,
        ICommandHandler<WithdrawApplication>
    {
        private readonly IRepository<Domain.Application> _applicationRepository;

        public ApplicationCommandsHandler(IRepository<Domain.Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(CreateApplicationDraft command, CancellationToken cancellationToken)
        {
            var entity = new Domain.Application
            {
                Status = ApplicationStatus.Draft,
                GuaranteeWorkflowId = Guid.NewGuid()
            };

            await _applicationRepository.AddAsync(entity, cancellationToken)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            command.EnqueueOutputEvent(new ApplicationDraftCreated(
                entity.GuaranteeWorkflowId, entity.Id));
            
            return Unit.Value;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(SubmitApplication command, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetAsync(command.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (application == null)
                throw new UserException("Заявка не найдена", "Не удалось найти заявку для утверждения");

            application.Status = ApplicationStatus.Submitted;
            
            command.EnqueueOutputEvent(new ApplicationSubmitted(
                application.GuaranteeWorkflowId, application.Id));
            
            return Unit.Value;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(WithdrawApplication command, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetAsync(command.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);

            if (application == null)
                throw new UserException("Заявка не найдена", "Не удалось найти заявку для утверждения");

            application.Status = ApplicationStatus.Withdrawn;
            
            command.EnqueueOutputEvent(new ApplicationWithdrawn(
                application.GuaranteeWorkflowId, application.Id));
            
            return Unit.Value;
        }
    }
}