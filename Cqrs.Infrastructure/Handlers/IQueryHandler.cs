using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Handlers
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        
    }
}