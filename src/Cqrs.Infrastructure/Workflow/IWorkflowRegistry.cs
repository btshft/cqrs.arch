using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Реестр процессов.
    /// </summary>
    /// <typeparam name="TWorkflow">Тип процесса.</typeparam>
    public interface IWorkflowRegistry<TWorkflow>
        where TWorkflow : IWorkflow
    {
        /// <summary>
        /// Выполняет поиск процесса по индентификатору.
        /// </summary>
        /// <param name="id">Идентификатор процесса.</param>
        /// <param name="cancellation">Токен отмены действия.</param>
        /// <returns>Процесс.</returns>
        Task<TWorkflow> FindAsync(Guid id, CancellationToken cancellation);
        
        /// <summary>
        /// Сохраняет состояние процесса.
        /// </summary>
        /// <param name="workflow">Процесс.</param>
        /// <param name="cancellation">Токен отмены.</param>
        Task PersistAsync(TWorkflow workflow, CancellationToken cancellation);
    }
}