using Cqrs.Contracts.Application;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.AppServices.Application.Queries
{
    /// <summary>
    /// Запрос на получение заявки.
    /// </summary>
    public class GetApplicationQuery : Query<ApplicationDto>
    {
        public int ApplicationId { get; }

        public GetApplicationQuery(int applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}