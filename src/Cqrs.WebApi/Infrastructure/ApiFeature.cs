using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.WebApi.Infrastructure
{
    public abstract class ApiFeature
    {
        public abstract void RegisterFeature(IServiceCollection services, IConfiguration configuration);
        public abstract void UseFeature(IApplicationBuilder app, IConfiguration configuration);
    }
}