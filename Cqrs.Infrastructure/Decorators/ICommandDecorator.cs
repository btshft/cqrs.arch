using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Decorators
{
    public interface ICommandDecorator<in TCommand> : IPipelineBehavior<TCommand, Unit>
        where TCommand : ICommand 
    { }
}