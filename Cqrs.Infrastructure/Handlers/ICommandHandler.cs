using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Handlers
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
        where TCommand : ICommand
    {       
    }
}