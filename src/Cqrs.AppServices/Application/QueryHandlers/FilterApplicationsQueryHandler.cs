using Cqrs.AppServices.Application.Queries;
using Cqrs.Contracts.Application;
using Cqrs.Domain.Models;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Filtering;
using Cqrs.Infrastructure.Mapper;

namespace Cqrs.AppServices.Application.QueryHandlers
{
    /// <summary>
    /// Обработчик фильтрации заявок.
    /// </summary>
    public class FilterApplicationsQueryHandler : FilterQueryHandler<FilterApplicationsQuery, Domain.Models.Application, ApplicationDto>
    {     
        public FilterApplicationsQueryHandler(IRepository<Domain.Models.Application> repository, ITypeMapper typeMapper) 
            : base(repository, typeMapper)
        {
        }
        
        /// <inheritdoc />
        protected override void BuildPredicate(FilterApplicationsQuery query, IPredicateBuilder<Domain.Models.Application> builder)
        {
            var filter = query.Filter;
            if (filter.Status.HasValue)
                builder.And(a => a.Status == (ApplicationStatus) filter.Status.Value);
        }
    }
}