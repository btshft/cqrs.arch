using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.WebApi.Infrastructure.ExceptionHandling
{
    [ApiFeature("ExceptionHandling", order: 0)]
    public class ExceptionHandlingFeature : ApiFeature
    {
        /// <inheritdoc />
        public override void RegisterFeature(IServiceCollection services, IConfiguration configuration)
        {
            // do nothing
        }

        /// <inheritdoc />
        public override void UseFeature(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}