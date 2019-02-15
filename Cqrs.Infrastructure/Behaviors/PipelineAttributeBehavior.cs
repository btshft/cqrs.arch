using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Cqrs.Infrastructure.Behaviors
{
    /// <summary>
    /// Декоратор на основе атрибута.
    /// </summary>
    /// <typeparam name="TAttribute">Тип атрибута.</typeparam>
    /// <typeparam name="TCommand">Тип команды.</typeparam>
    /// <typeparam name="TOutput">Маркерный тип для регистрации</typeparam>
    public abstract class PipelineAttributeBehavior<TAttribute, TCommand, TOutput> : IPipelineBehavior<TCommand, TOutput>
        where TAttribute : Attribute
    {
        /// <inheritdoc />
        public Task<TOutput> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TOutput> next)
        {
            var attribute = typeof(TCommand).GetCustomAttribute<TAttribute>(inherit: false);
            if (attribute != null)
            {
                return HandleCore(request, attribute, cancellationToken, next);
            }

            return next();
        }

        protected abstract Task<TOutput> HandleCore(TCommand command, TAttribute attribute, CancellationToken cancellation, RequestHandlerDelegate<TOutput> next);
    }
}