using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Exceptions
{
    public class ExceptionEnrichmentDecorator<TMessage, TOutput> : IPipelineBehavior<TMessage, TOutput>
        where TMessage : IMessage
    {
        public async Task<TOutput> Handle(TMessage request, CancellationToken cancellationToken, RequestHandlerDelegate<TOutput> next)
        {
            try
            {
                return await next()
                    .ConfigureAwait(continueOnCapturedContext: false);
            }
            catch (Exception exception)
            {
                throw new EnrichedException(request, exception);
            }
        }
    }
}