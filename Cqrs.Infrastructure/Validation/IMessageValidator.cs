using System.Collections.Generic;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Validation
{
    /// <summary>
    /// Валидатор сообщений.
    /// </summary>
    /// <typeparam name="TMessage">Тип сообщения.</typeparam>
    public interface IMessageValidator<in TMessage> where TMessage : IMessage
    {
        /// <summary>
        /// Выполняет валидацию сообшения.
        /// </summary>
        /// <param name="message">Сообщение для валидации.</param>
        /// <param name="errors">Коллекция ошибок.</param>
        /// <returns>Признак корректности сообщения.</returns>
        bool Validate(TMessage message, out IReadOnlyCollection<string> errors);
    }
}