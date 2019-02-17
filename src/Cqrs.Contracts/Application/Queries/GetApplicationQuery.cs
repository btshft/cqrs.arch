using Cqrs.Contracts.Application.Projections;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Contracts.Application.Queries
{
    /// <summary>
    /// Запрос на получение заявки.
    /// </summary>
    public class GetApplicationQuery : Query<ApplicationProjection>
    {
        /// <summary>
        /// Идентификатор заявки.
        /// </summary>
        public int ApplicationId { get; set; }
    }
}