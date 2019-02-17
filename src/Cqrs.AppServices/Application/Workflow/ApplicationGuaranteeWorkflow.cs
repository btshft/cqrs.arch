using System;
using System.Collections.Generic;
using Cqrs.Infrastructure.Messages;
using Cqrs.Infrastructure.Workflow;

namespace Cqrs.AppServices.Application.Workflow
{
    /// <summary>
    /// Процесс работы с обеспечением по заявке.
    /// </summary>
    public class ApplicationGuaranteeWorkflow : IWorkflow
    {
        /// <summary>
        /// Идентификатор процесса.
        /// </summary>
        public Guid WorkflowId { get; }

        /// <summary>
        /// Признак завершенности процесса.
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Исходящие команды.
        /// </summary>
        public Queue<ICommand> OutputCommands { get; }
        
        /// <summary>
        /// Заявка.
        /// </summary>
        public Domain.Application Application { get; }
        
        /// <summary>
        /// Инициирует экземпляр <see cref="ApplicationGuaranteeWorkflow"/>.
        /// </summary>
        /// <param name="application">Заявка.</param>
        public ApplicationGuaranteeWorkflow(Domain.Application application)
        {
            Application = application;
            IsCompleted = false;
            OutputCommands = new Queue<ICommand>();
            WorkflowId = application.GuaranteeWorkflowId;
        }
    }
}