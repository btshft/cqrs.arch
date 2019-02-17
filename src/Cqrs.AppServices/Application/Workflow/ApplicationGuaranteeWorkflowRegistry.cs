using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Dispatcher;
using Cqrs.Infrastructure.Workflow;

namespace Cqrs.AppServices.Application.Workflow
{
    /// <summary>
    /// Реестр бизнес-процессов по заявке.
    /// </summary>
    public class ApplicationGuaranteeWorkflowRegistry : WorkflowRegistry<ApplicationGuaranteeWorkflow>
    {
        private readonly IRepository<Domain.Application> _applicationRepository;


        public ApplicationGuaranteeWorkflowRegistry(
            IMessageDispatcher dispatcher,
            IRepository<Domain.Application> applicationRepository) : base(dispatcher)
        {
            _applicationRepository = applicationRepository;
        }

        /// <inheritdoc />
        protected override Task<ApplicationGuaranteeWorkflow> FindCoreAsync(Guid id, CancellationToken cancellation)
        {
            var application = _applicationRepository.Query(a => a.GuaranteeWorkflowId == id)
                .FirstOrDefault();

            return application != null 
                ? Task.FromResult(new ApplicationGuaranteeWorkflow(application)) 
                : Task.FromResult<ApplicationGuaranteeWorkflow>(null);
        }

        /// <inheritdoc />
        protected override async Task PersistCoreAsync(ApplicationGuaranteeWorkflow guaranteeWorkflow, CancellationToken cancellation)
        {
            await _applicationRepository.UpdateAsync(guaranteeWorkflow.Application, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}