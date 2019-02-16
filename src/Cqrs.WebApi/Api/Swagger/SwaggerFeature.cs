using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Cqrs.WebApi.Api.Swagger
{
    [ApiFeature("Swagger", order: 2)]
    public class SwaggerFeature : ApiFeature
    {
        /// <inheritdoc />
        public override void RegisterFeature(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(o => 
            {
                o.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "CQRS Example"
                });
            });
        }

        /// <inheritdoc />
        public override void UseFeature(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}