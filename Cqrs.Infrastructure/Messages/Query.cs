namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс запроса.
    /// </summary>
    /// <typeparam name="TResult">Тип возвращаемого результата. Маркерный тип для сопоставления сообщений и обработчиков.</typeparam>
    public abstract class Query<TResult> : Message, IQuery<TResult> { }
}