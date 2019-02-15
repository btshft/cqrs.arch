using MediatR;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Интерфейс команды.
    /// </summary>
    public interface ICommand : IMessage, IRequest<Unit> 
    { }
}