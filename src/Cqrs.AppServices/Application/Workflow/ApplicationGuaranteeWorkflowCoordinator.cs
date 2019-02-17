using System.Threading;
using System.Threading.Tasks;
using Cqrs.Contracts.Application.Events;
using Cqrs.Contracts.Finance.Commands;
using Cqrs.Contracts.Finance.Events;
using Cqrs.Domain;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Workflow;

namespace Cqrs.AppServices.Application.Workflow
{
    /// <summary>
    /// Координатор бизнес-процесса по работе с обеспечением заявки.
    /// </summary>
    public class ApplicationGuaranteeWorkflowCoordinator :
        WorkflowCoordinator<ApplicationDraftCreated, ApplicationGuaranteeWorkflow>,
        IWorkflowStateHandler<ApplicationSubmitted, ApplicationGuaranteeWorkflow>,
        IWorkflowStateHandler<ApplicationWithdrawn, ApplicationGuaranteeWorkflow>,
        IWorkflowStateHandler<GuaranteeBlocked, ApplicationGuaranteeWorkflow>,
        IWorkflowStateHandler<GuaranteeUnblocked, ApplicationGuaranteeWorkflow>
    {
        private readonly IRepository<Domain.Application> _applicationRepository;
        
        public ApplicationGuaranteeWorkflowCoordinator(
            IWorkflowRegistry<ApplicationGuaranteeWorkflow> registry, 
            IRepository<Domain.Application> applicationRepository)
            : base(registry)
        {
            _applicationRepository = applicationRepository;
        }

        /// <inheritdoc />
        protected override Task<ApplicationGuaranteeWorkflow> CreateWorkflowAsync(ApplicationDraftCreated @event,
            CancellationToken cancellation)
        {
            return Task.FromResult(new ApplicationGuaranteeWorkflow
            {
                GuaranteeState = ApplicationGuaranteeState.Initial,
                IsCompleted = false,
                WorkflowId = @event.WorkflowId
            });
        }

        /// <inheritdoc />
        public async Task ProcessAsync(ApplicationSubmitted @event,
            WorkflowEnvelope<ApplicationGuaranteeWorkflow> envelope, CancellationToken cancellation)
        {
            var application = await _applicationRepository.GetAsync(@event.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);

            var workflow = envelope.Workflow;
            
            // Заявка утверждена, обеспечение не заблокировано - пытаемся разблокировать
            if (application.Status == ApplicationStatus.Submitted &&
                workflow.GuaranteeState == ApplicationGuaranteeState.Initial)
            {
                // Отправляем запрос на блокировку
                envelope.OutputCommands.Enqueue(new BlockGuarantee(
                    workflow.WorkflowId, application.Id));
                
                workflow.GuaranteeState = ApplicationGuaranteeState.BlockWaiting;
            }
        }

        /// <inheritdoc />
        public async Task ProcessAsync(ApplicationWithdrawn @event,
            WorkflowEnvelope<ApplicationGuaranteeWorkflow> envelope, CancellationToken cancellation)
        {
            var application = await _applicationRepository.GetAsync(@event.ApplicationId)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            var workflow = envelope.Workflow;

            // Проверяем, что состояние заявки не изменили
            // По хорошему - здесь нужно сравнивать не статусы, а версии агрегата c переданной в событии
            // в случае применения механизма optimistic concurrency (OCC)
            if (application.Status == ApplicationStatus.Withdrawn)
            {
                // Отправляем запрос на разблокировку / отмену запроса блокировки
                if (workflow.GuaranteeState == ApplicationGuaranteeState.Blocked ||
                    workflow.GuaranteeState == ApplicationGuaranteeState.BlockWaiting)
                {
                    envelope.OutputCommands.Enqueue(new UnblockGuarantee(
                        workflow.WorkflowId, application.Id));
                }
            }
        }

        /// <inheritdoc />
        public Task ProcessAsync(GuaranteeBlocked @event, WorkflowEnvelope<ApplicationGuaranteeWorkflow> envelope,
            CancellationToken cancellation)
        {
            // Финансовый сервис находится в другом домене и ничего
            // про заявку не знает, кроме необходимости разблокировать по ней средства.
            // Поэтому не смотря на событие - следить за актуальным состоянием блокировки
            // задача текущего домена. Поэтому обновляем состояние тут, а не в обработчике команд по блокировке.
            envelope.Workflow.GuaranteeState = ApplicationGuaranteeState.Blocked;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ProcessAsync(GuaranteeUnblocked @event,
            WorkflowEnvelope<ApplicationGuaranteeWorkflow> envelope, CancellationToken cancellation)
        {
            envelope.Workflow.GuaranteeState = ApplicationGuaranteeState.Unblocked;

            return Task.CompletedTask;
        }
    }
}