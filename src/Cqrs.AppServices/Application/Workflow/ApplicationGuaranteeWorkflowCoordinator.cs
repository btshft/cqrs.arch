using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Events;
using Cqrs.AppServices.Finance.Commands;
using Cqrs.AppServices.Finance.Events;
using Cqrs.Domain;
using Cqrs.Infrastructure.Workflow;

namespace Cqrs.AppServices.Application.Workflow
{
    /// <summary>
    /// Координатор бизнес-процесса по работе с обеспечением заявки.
    /// </summary>
    public class ApplicationGuaranteeWorkflowCoordinator : WorkflowCoordinator<ApplicationDraftCreatedEvent, ApplicationGuaranteeWorkflow>,
          IWorkflowStateHandler<ApplicationSubmittedEvent, ApplicationGuaranteeWorkflow>,
          IWorkflowStateHandler<ApplicationWithdrawnEvent, ApplicationGuaranteeWorkflow>,
          IWorkflowStateHandler<GuaranteeBlockedEvent, ApplicationGuaranteeWorkflow>,
          IWorkflowStateHandler<GuaranteeUnblockedEvent, ApplicationGuaranteeWorkflow>
    {
        public ApplicationGuaranteeWorkflowCoordinator(IWorkflowRegistry<ApplicationGuaranteeWorkflow> registry) 
            : base(registry)
        {
        }

        /// <inheritdoc />
        protected override Task<ApplicationGuaranteeWorkflow> CreateWorkflowAsync(
            ApplicationDraftCreatedEvent @event, CancellationToken cancellation)
        {
            // Бизнес-процесс инициируется во время подачи заявки
            return Registry.FindAsync(@event.WorkflowId.Value, cancellation);
        }

        /// <inheritdoc />
        public Task ProcessAsync(ApplicationSubmittedEvent @event, ApplicationGuaranteeWorkflow workflow,
            CancellationToken cancellation)
        {
            // Заявка утверждена, обеспечение не заблокировано - пытаемся разблокировать
            if (workflow.Application.Status == ApplicationStatus.Submitted &&
                workflow.Application.GuaranteeState == ApplicationGuaranteeState.Initial)
            {
                // Отправляем запрос на блокировку
                workflow.OutputCommands.Enqueue(new BlockGuaranteeCommand(
                    workflow.WorkflowId, workflow.Application.Id));
                
                workflow.Application.GuaranteeState = ApplicationGuaranteeState.BlockWaiting;
            }
            
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ProcessAsync(ApplicationWithdrawnEvent @event, ApplicationGuaranteeWorkflow workflow,
            CancellationToken cancellation)
        {
            if (workflow.Application.Status == ApplicationStatus.Withdrawn)
            {
                var guaranteeState = workflow.Application.GuaranteeState;
                switch (guaranteeState)
                {
                    case ApplicationGuaranteeState.BlockWaiting:
                    case ApplicationGuaranteeState.Blocked:
                        // Отправляем запрос на разблокировку / отмену запроса блокировки
                        workflow.OutputCommands.Enqueue(new UnblockGuaranteeCommand(
                            workflow.WorkflowId, workflow.Application.Id));

                        // Обновляем статус блокировки
                        workflow.Application.GuaranteeState = ApplicationGuaranteeState.UnblockWaiting;                   
                        break;
                    
                    case ApplicationGuaranteeState.UnblockWaiting:
                    case ApplicationGuaranteeState.Unblocked:
                        // Ничего не делаем, т.к. либо нужно подождать, либо деньги уже были разблокированы
                        break;
                }
            }
            
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ProcessAsync(GuaranteeBlockedEvent @event,
            ApplicationGuaranteeWorkflow workflow,
            CancellationToken cancellation)
        {
            // Финансовый сервис находится в другом домене и ничего
            // про заявку не знает, кроме необходимости разблокировать по ней средства.
            // Поэтому не смотря на событие - следить за актуальным состоянием блокировки
            // задача текущего домена. Поэтому обновляем состояние тут, а не в обработчике команд по блокировке.
            workflow.Application.GuaranteeState = ApplicationGuaranteeState.Blocked;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ProcessAsync(GuaranteeUnblockedEvent @event,
            ApplicationGuaranteeWorkflow workflow,
            CancellationToken cancellation)
        {
            workflow.Application.GuaranteeState = ApplicationGuaranteeState.Unblocked;
            return Task.CompletedTask;
        }
    }
}