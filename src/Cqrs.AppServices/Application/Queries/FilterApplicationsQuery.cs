using Cqrs.Contracts.Application;
using Cqrs.Infrastructure.Filtering;

namespace Cqrs.AppServices.Application.Queries
{
    /// <summary>
    /// Запрос на получение заявок.
    /// </summary>
    public class FilterApplicationsQuery : FilterQuery<ApplicationDto>
    {
        public FilterApplicationsQuery(ApplicationFilterDto filter)
        {
            Filter = filter;
        }

        public ApplicationFilterDto Filter { get; }

        /// <inheritdoc />
        public override int? Skip => Filter.Skip;

        /// <inheritdoc />
        public override int Take => Filter.Take;
    }
}