using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Handlers
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> 
        where TEvent : IEvent
    {
        
    }
}