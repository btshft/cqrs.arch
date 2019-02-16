using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Dispatcher
{
    /// <summary>
    /// Медиатор.
    /// </summary>
    public class MediatorDispatcher : IMediatorDispatcher
    {
        private readonly IMediator _mediator;

        public MediatorDispatcher(IMediator mediator)
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
            return _mediator.Publish(@event, cancellation);
        }
    }
}