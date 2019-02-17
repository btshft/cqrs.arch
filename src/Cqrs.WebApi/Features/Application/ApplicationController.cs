using System.Threading;
using System.Threading.Tasks;
using Cqrs.Contracts.Application.Commands;
using Cqrs.Contracts.Application.Queries;
using Cqrs.Infrastructure.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.WebApi.Features.Application
{
    [ApiController, Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IMessageDispatcher _dispatcher;

        public ApplicationController(IMessageDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> FilterApplications([FromQuery] FilterApplicationsQuery filter, CancellationToken cancellation = default)
        {
            var applications = await _dispatcher.DispatchQueryAsync(filter, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(int id, CancellationToken cancellation = default)
        {
            var application = await _dispatcher.DispatchQueryAsync(
                    new GetApplicationQuery{ ApplicationId = id}, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Ok(application);
        }

        [HttpPost("draft")]
        public async Task<IActionResult> CreateDraft(CreateApplicationDraft creation, CancellationToken cancellation = default)
        {
            await _dispatcher.DispatchCommandAsync(creation, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Ok();
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(SubmitApplication request, CancellationToken cancellation = default)
        {
            await _dispatcher.DispatchCommandAsync(request, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawApplication request, CancellationToken cancellation = default)
        {
            await _dispatcher.DispatchCommandAsync(request, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            return Ok();
        }
    }
}