using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.Contracts.Application;
using Cqrs.Infrastructure.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.WebApi.Features.Application
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IMediatorDispatcher _dispatcher;

        public ApplicationController(IMediatorDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("draft")]
        public async Task<IActionResult> CreateDraft(ApplicationDto application, CancellationToken cancellation = default(CancellationToken))
        {
            await _dispatcher.DispatchCommandAsync(new CreateApplicationDraftCommand(application), cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Ok();
        }
    }
}