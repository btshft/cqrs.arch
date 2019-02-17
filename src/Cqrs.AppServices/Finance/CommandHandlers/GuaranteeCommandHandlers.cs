using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Finance.Commands;
using Cqrs.AppServices.Finance.Events;
using Cqrs.Infrastructure.Handlers;
using MediatR;

namespace Cqrs.AppServices.Finance.CommandHandlers
{
    public class GuaranteeCommandHandlers : 
        ICommandHandler<BlockGuaranteeCommand>,
        ICommandHandler<UnblockGuaranteeCommand>
    {
        /// <inheritdoc />
        public Task<Unit> Handle(BlockGuaranteeCommand command, CancellationToken cancellationToken)
        {   
            // Эмулируем ответ от другого домена
            command.EnqueueOutputEvent(new GuaranteeBlockedEvent(command.WorkflowId.Value));
            
            return Task.FromResult(Unit.Value);
        }

        /// <inheritdoc />
        public Task<Unit> Handle(UnblockGuaranteeCommand command, CancellationToken cancellationToken)
        {
            // Эмулируем ответ от другого домена
            command.EnqueueOutputEvent(new GuaranteeUnblockedEvent(command.WorkflowId.Value));
            
            return Task.FromResult(Unit.Value);
        }
    }
}