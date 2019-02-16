using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.DependencyInjection;
using Cqrs.Infrastructure.Exceptions;
using Cqrs.Infrastructure.Messages;
using MediatR;

namespace Cqrs.Infrastructure.Validation
{
    /// <summary>
    /// Декоратор валидации сообщений.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    /// <typeparam name="TOutput">Тип результата.</typeparam>
    public class MessageValidationBehavior<TMessage, TOutput> : IPipelineBehavior<TMessage, TOutput>
        where TMessage : IMessage
    {
        private readonly IComponentProvider<IMessageValidator<TMessage>> _validatorProvider;

        public MessageValidationBehavior(IComponentProvider<IMessageValidator<TMessage>> validatorProvider)
        {
            _validatorProvider = validatorProvider;
        }

        /// <inheritdoc />
        public Task<TOutput> Handle(TMessage request, CancellationToken cancellationToken, RequestHandlerDelegate<TOutput> next)
        {
            var errors = _validatorProvider.GetComponents()
                .Aggregate(new List<string>(), (err, validator) =>
                {
                    if (!validator.Validate(request, out var localErrors))
                        err.AddRange(localErrors);

                    return err;
                });
            
            if (errors.Any())
                throw new UserException("Некорректные параметры запроса", $"Запрос содержит ошибки: {string.Join(Environment.NewLine, errors)}");

            return next();
        }
    }
}