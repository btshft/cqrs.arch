using System.Threading;
using System.Threading.Tasks;
using Cqrs.Contracts.Finance.Commands;
using Cqrs.Contracts.Finance.Events;
using Cqrs.Infrastructure.Handlers;
using MediatR;

namespace Cqrs.AppServices.Finance.CommandHandlers
{
    public class GuaranteeCommandHandlers : 
        ICommandHandler<BlockGuarantee>,
        ICommandHandler<UnblockGuarantee>
    {
        /// <inheritdoc />
        public Task<Unit> Handle(BlockGuarantee command, CancellationToken cancellationToken)
        {   
            // Эмулируем ответ от другого домена
            command.EnqueueOutputEvent(new GuaranteeBlocked(command.WorkflowId.Value));
            
            return Task.FromResult(Unit.Value);
        }

        /// <inheritdoc />
        public Task<Unit> Handle(UnblockGuarantee command, CancellationToken cancellationToken)
        {
            // Эмулируем ответ от другого домена
            command.EnqueueOutputEvent(new GuaranteeUnblocked(command.WorkflowId.Value));
            
            return Task.FromResult(Unit.Value);
        }
    }
}