using System;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Behaviors;
using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Data
{
    /// <summary>
    /// Атрибут транзакционности действий.
    /// </summary>
    /// <typeparam name="TCommand">Тип команды.</typeparam>
    /// <typeparam name="TOutput">Маркерный интерфейс для регистрации.</typeparam>
    public class TransactionalBehavior<TCommand, TOutput> : PipelineAttributeBehavior<TransactionalAttribute, TCommand, TOutput> 
    {
        private readonly IDataSessionFactory _dataSessionFactory;

        public TransactionalBehavior(IDataSessionFactory dataSessionFactory)
        {
            _dataSessionFactory = dataSessionFactory;
        }

        /// <inheritdoc />
        protected override async Task<TOutput> HandleCore(TCommand command, TransactionalAttribute attribute, CancellationToken cancellation,
            RequestHandlerDelegate<TOutput> next)
        {
            if (!(command is Command))
                return await next()
                    .ConfigureAwait(continueOnCapturedContext: false);
            
            using (var session = _dataSessionFactory.CreateDataSession())
            {
                try
                {
                    var result = await next()
                        .ConfigureAwait(continueOnCapturedContext: false);
                    
                    await session.CommitAsync(cancellation)
                        .ConfigureAwait(continueOnCapturedContext: false);

                    return result;
                }
                catch (Exception)
                {
                    await session.RollbackAsync(cancellation)
                        .ConfigureAwait(continueOnCapturedContext: false);

                    throw;
                }
            }
        }
    }
}