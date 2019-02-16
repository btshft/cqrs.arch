namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс события.
    /// </summary>
    public abstract class Event : Message, IEvent { }
}