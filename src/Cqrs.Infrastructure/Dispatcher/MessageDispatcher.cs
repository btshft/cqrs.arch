using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Dispatcher
{
    /// <summary>
    /// Медиатор.
    /// </summary>
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IMediator _mediator;

        public MessageDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <inheritdoc />
        public Task<TResponse> DispatchQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellation)
        {
            return _mediator.Send(query, cancellation);
        }

        /// <inheritdoc />
        public Task DispatchCommandAsync(ICommand command, CancellationToken cancellation)
        {
            return _mediator.Send(command, cancellation);
        }

        /// <inheritdoc />
        public Task DispatchEventAsync(IEvent @event, CancellationToken cancellation)
        {
            var publishRef = typeof(IMediator)
                .GetMethods()
                .Single(m => m.IsGenericMethodDefinition && m.Name == nameof(IMediator.Publish));

            var publishMethod = publishRef.MakeGenericMethod(@event.GetType());
            return (Task) publishMethod.Invoke(_mediator, new object[] { @event, cancellation });
        }
    }
}