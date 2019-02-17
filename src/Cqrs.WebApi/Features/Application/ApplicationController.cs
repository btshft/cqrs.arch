using System.Threading;
using System.Threading.Tasks;
using Cqrs.AppServices.Application.Commands;
using Cqrs.AppServices.Application.Queries;
using Cqrs.Contracts.Application;
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
        public async Task<IActionResult> FilterApplications([FromQuery] ApplicationFilterDto filter, CancellationToken cancellation = default)
        {
            var applications = await _dispatcher.DispatchQueryAsync(new FilterApplicationsQuery(filter), cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(int id, CancellationToken cancellation = default)
        {
            var application = await _dispatcher.DispatchQueryAsync(new GetApplicationQuery(id), cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Ok(application);
        }

        [HttpPost("draft")]
        public async Task<IActionResult> CreateDraft(ApplicationDraftCreationDto creation, CancellationToken cancellation = default)
        {
            await _dispatcher.DispatchCommandAsync(new CreateApplicationDraftCommand(creation), cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            return Ok();
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(ApplicationSubmitRequest request, CancellationToken cancellation = default)
        {
            await _dispatcher.DispatchCommandAsync(new SubmitApplicationCommand(request.ApplicationId), cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            return Ok();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw(ApplicationWithdrawRequest request, CancellationToken cancellation = default)
        {
            await _dispatcher.DispatchCommandAsync(new WithdrawApplicationCommand(request.ApplicationId), cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            return Ok();
        }
    }
}