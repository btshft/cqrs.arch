using Cqrs.Contracts.Application.Projections;
using Cqrs.Infrastructure.Filtering;

namespace Cqrs.Contracts.Application.Queries
{
    /// <summary>
    /// Запрос на получение заявок.
    /// </summary>
    public class FilterApplicationsQuery : FilterQuery<ApplicationProjection>
    {
        /// <inheritdoc />
        public override int? Skip { get; set; }

        /// <inheritdoc />
        public override int Take { get; set; }
        
        /// <summary>
        /// Статус заявки.
        /// </summary>
        public int? Status { get; set; }
    }
}