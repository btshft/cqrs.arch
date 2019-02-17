using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Queries;
using Cqrs.Contracts.Application;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Exceptions;
using Cqrs.Infrastructure.Handlers;
using Cqrs.Infrastructure.Mapper;

namespace Cqrs.AppServices.Application.QueryHandlers
{
    /// <summary>
    /// Обработчик получения заявки.
    /// </summary>
    public class GetApplicationQueryHandler : IQueryHandler<GetApplicationQuery, ApplicationDto>
    {
        private readonly IRepository<Domain.Application> _applicationRepository;
        private readonly ITypeMapper _typeMapper;

        public GetApplicationQueryHandler(IRepository<Domain.Application> applicationRepository, ITypeMapper typeMapper)
        {
            _applicationRepository = applicationRepository;
            _typeMapper = typeMapper;
        }

        /// <inheritdoc />
        public async Task<ApplicationDto> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            var application = await _applicationRepository.GetAsync(request.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            if (application == null)
                throw new UserException("Заявка не найдена", "Запрошенная заявка не была найдена в системе");

            return _typeMapper.Map<ApplicationDto>(application);
        }
    }
}