using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Dispatcher;
using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Behaviors
{
    /// <summary>
    /// Декоратор обработки исходящей очереди событий команды.
    /// </summary>
    /// <typeparam name="TCommand">Тип команды.</typeparam>
    /// <typeparam name="TOutput">Маркерный интерфейс для регистрации.</typeparam>
    public class ProcessOutputEventsBehavior<TCommand, TOutput> : IPipelineBehavior<TCommand, TOutput>
    {
        private readonly IMessageDispatcher _dispatcher;

        public ProcessOutputEventsBehavior(IMessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <inheritdoc />
        public async Task<TOutput> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TOutput> next)
        {
            var result = await next()
                .ConfigureAwait(continueOnCapturedContext: false);

            // INFO: Констрейнт на дженерики не работает со стандартным .NET CORE DI
            // поэтому приходится делать так
            if (request is Command command)
            {
                if (command.HasOutputEvents)
                {
                    await command.ProcessOutputEventsAsync(_dispatcher.DispatchEventAsync, cancellationToken)
                        .ConfigureAwait(continueOnCapturedContext: false);
                }
            }

            return result;
        }
    }
}