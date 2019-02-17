using Cqrs.AppServices.Application.Queries;
using Cqrs.Contracts.Application;
using Cqrs.Domain;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Filtering;
using Cqrs.Infrastructure.Mapper;

namespace Cqrs.AppServices.Application.QueryHandlers
{
    /// <summary>
    /// Обработчик фильтрации заявок.
    /// </summary>
    public class FilterApplicationsQueryHandler : FilterQueryHandler<FilterApplicationsQuery, Domain.Application, ApplicationDto>
    {     
        public FilterApplicationsQueryHandler(IRepository<Domain.Application> repository, ITypeMapper typeMapper) 
            : base(repository, typeMapper)
        {
        }
        
        /// <inheritdoc />
        protected override void BuildPredicate(FilterApplicationsQuery query, IPredicateBuilder<Domain.Application> builder)
        {
            var filter = query.Filter;
            if (filter.Status.HasValue)
                builder.And(a => a.Status == (ApplicationStatus) filter.Status.Value);
        }
    }
}