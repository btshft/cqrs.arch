using System;
using System.Collections.Generic;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Workflow
{
    /// <summary>
    /// Бизнес-процесс.
    /// </summary>
    public interface IWorkflow
    {
        /// <summary>
        /// Идентификатор процесса.
        /// </summary>
        Guid WorkflowId { get; }
        
        /// <summary>
        /// Признак завершенности процесса.
        /// </summary>
        bool IsCompleted { get; }
             
        /// <summary>
        /// Исходящие команды.
        /// </summary>
        Queue<ICommand> OutputCommands { get; }
    }
}