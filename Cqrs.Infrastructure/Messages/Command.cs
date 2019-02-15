using MediatR;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс команды.
    /// </summary>
    public abstract class Command : Message, ICommand
    { }
}