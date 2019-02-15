using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Handlers;
using Cqrs.Infrastructure.Mapper;
using MediatR;

namespace Cqrs.AppServices.Application.CommandHandlers
{
    public class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationDraftCommand>
    {
        private readonly IRepository<Domain.Models.Application> _applicationRepository;
        private readonly ITypeMapper _mapper;

        public CreateApplicationCommandHandler(IRepository<Domain.Models.Application> applicationRepository, ITypeMapper mapper)
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
    }
}