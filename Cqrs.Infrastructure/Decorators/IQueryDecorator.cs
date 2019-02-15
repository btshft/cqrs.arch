using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Decorators
{
    public interface IQueryDecorator<in TQuery, TResult> : IPipelineBehavior<TQuery, TResult>
        where TQuery : IQuery<TResult> 
    { }
}