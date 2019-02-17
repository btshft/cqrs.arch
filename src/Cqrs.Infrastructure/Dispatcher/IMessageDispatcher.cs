using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Dispatcher
{
    /// <summary>
    /// Интерфейс обработчика сообщений.
    /// </summary>
    public interface IMessageDispatcher
    {
        /// <summary>
        /// Отправляет запрос на выполнение.
        /// </summary>
        /// <param name="query">Запрос.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        /// <typeparam name="TResponse">Тип результата запроса.</typeparam>
        /// <returns>Результат запроса.</returns>
        Task<TResponse> DispatchQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellation);
        
        /// <summary>
        /// Отправляет команду на выполнение.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        Task DispatchCommandAsync(ICommand command, CancellationToken cancellation);
        
        /// <summary>
        /// Отправляет событие на выполнение.
        /// </summary>
        /// <param name="event">Событие.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        Task DispatchEventAsync(IEvent @event, CancellationToken cancellation);
    }
}