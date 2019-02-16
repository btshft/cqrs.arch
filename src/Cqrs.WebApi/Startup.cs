using Cqrs.ComponentRegistrar;
using Cqrs.WebApi.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cqrs.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();         
            services.AddLogging(o => o.AddConsole());
            
            services.AddFeatures(Configuration);
            
            ApplicationRegistrar.RegisterComponents(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseFeatures(Configuration);
        }
    }
}