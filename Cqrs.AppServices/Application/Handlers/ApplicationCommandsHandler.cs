using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.Domain.Models;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Handlers;
using Cqrs.Infrastructure.Mapper;
using MediatR;

namespace Cqrs.AppServices.Application.Handlers
{
    public class ApplicationCommandsHandler : 
        ICommandHandler<CreateApplicationDraftCommand>,
        ICommandHandler<SubmitApplicationCommand>
    {
        private readonly IRepository<Domain.Models.Application> _applicationRepository;
        private readonly ITypeMapper _mapper;

        public ApplicationCommandsHandler(IRepository<Domain.Models.Application> applicationRepository, ITypeMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(CreateApplicationDraftCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Models.Application>(request.Application);

            await _applicationRepository.AddAsync(entity)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Unit.Value;
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