using System;
using System.Collections.Generic;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Фабрика для создания <see cref="WorkflowEnvelope{TWorkflow}"/>.
    /// </summary>
    public abstract class WorkflowEnvelope
    {    
        /// <summary>
        /// Создает новый экземпляр <see cref="WorkflowEnvelope{TWorkflow}"/>.
        /// </summary>
        /// <param name="workflow">Процесс.</param>
        /// <typeparam name="TWorkflow">Тип процесса.</typeparam>
        /// <returns>Процесс.</returns>
        public static WorkflowEnvelope<TWorkflow> Create<TWorkflow>(TWorkflow workflow)
            where TWorkflow : class, IWorkflow
        {
            if (workflow == null)
                throw new ArgumentNullException(nameof(workflow));
            
            return new WorkflowEnvelope<TWorkflow>(workflow);
        }
    }
    
    /// <summary>
    /// Обертка над процессом.
    /// </summary>
    /// <typeparam name="TWorkflow">Тип процесса.</typeparam>
    public class WorkflowEnvelope<TWorkflow> : WorkflowEnvelope
        where TWorkflow : class, IWorkflow
    {
        /// <summary>
        /// Исходящие команды.
        /// </summary>
        public Queue<ICommand> OutputCommands { get; }

        /// <summary>
        /// Процесс.
        /// </summary>
        public TWorkflow Workflow { get; }

        /// <summary>
        /// Метаданные.
        /// </summary>
        public Dictionary<string, object> Metadata { get; }
        
        /// <summary>
        /// Инициализирует экземпляр <see cref="WorkflowEnvelope{TWorkflow}"/>.
        /// </summary>
        /// <param name="workflow">Процесс.</param>
        public WorkflowEnvelope(TWorkflow workflow)
        {
            Workflow = workflow;
            OutputCommands = new Queue<ICommand>();
            Metadata = new Dictionary<string, object>();
        }
    }
}