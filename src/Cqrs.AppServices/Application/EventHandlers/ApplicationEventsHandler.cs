using System.Threading;
using System.Threading.Tasks;
using Cqrs.Contracts.Application.Events;
using Cqrs.Infrastructure.Handlers;
using Microsoft.Extensions.Logging;

namespace Cqrs.AppServices.Application.EventHandlers
{
    /// <summary>
    /// Обработчик событий по заявке.
    /// </summary>
    public class ApplicationEventsHandler : IEventHandler<ApplicationSubmitted>
    {
        private readonly ILogger<ApplicationEventsHandler> _logger;

        public ApplicationEventsHandler(ILogger<ApplicationEventsHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public Task Handle(ApplicationSubmitted notification, CancellationToken cancellationToken)
        {
            // Отправка уведомлений, смс, email и тд
            
            _logger.LogInformation($"Была утверждена заявка '{notification.ApplicationId}'");
            
            return Task.CompletedTask;
        }
    }
}