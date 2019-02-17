using Cqrs.Contracts.Application.Projections;
using Cqrs.Contracts.Application.Queries;
using Cqrs.Domain;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Filtering;
using Cqrs.Infrastructure.Mapper;

namespace Cqrs.AppServices.Application.QueryHandlers
{
    /// <summary>
    /// Обработчик фильтрации заявок.
    /// </summary>
    public class FilterApplicationsQueryHandler : FilterQueryHandler<FilterApplicationsQuery, Domain.Application, ApplicationProjection>
    {     
        public FilterApplicationsQueryHandler(IRepository<Domain.Application> repository, ITypeMapper typeMapper) 
            : base(repository, typeMapper)
        {
        }
        
        /// <inheritdoc />
        protected override void BuildPredicate(FilterApplicationsQuery query, IPredicateBuilder<Domain.Application> builder)
        {
            if (query.Status.HasValue)
                builder.And(a => a.Status == (ApplicationStatus) query.Status.Value);
        }
    }
}