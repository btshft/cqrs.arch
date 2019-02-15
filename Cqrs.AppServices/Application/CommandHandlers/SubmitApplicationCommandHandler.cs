using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.Domain.Models;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Handlers;
using MediatR;

namespace Cqrs.AppServices.Application.CommandHandlers
{
    public class SubmitApplicationCommandHandler : ICommandHandler<SubmitApplicationCommand>
    {
        private readonly IRepository<Domain.Models.Application> _applicationRepository;

        public SubmitApplicationCommandHandler(IRepository<Domain.Models.Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(SubmitApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetAsync(request.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);

            application.Status = ApplicationStatus.Submitted;
            return Unit.Value;
        }
    }
}