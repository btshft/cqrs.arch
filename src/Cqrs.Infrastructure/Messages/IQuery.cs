using MediatR;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Интерфейс модели запроса.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата. Маркерный тип для сопоставления сообщений и обработчиков.</typeparam>
    public interface IQuery<TResult> : IMessage, IRequest<TResult>
    { }
}