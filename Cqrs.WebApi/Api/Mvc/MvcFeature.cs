using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Cqrs.WebApi.Api.Mvc
{
    [ApiFeature("Mvc", order: 0)]
    public class MvcFeature : ApiFeature
    {
        /// <inheritdoc />
        public override void RegisterFeature(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore(o =>
                {
                    o.OutputFormatters.RemoveType<XmlDataContractSerializerOutputFormatter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonFormatters(s => s.Formatting = Formatting.Indented)
                .AddApiExplorer();
        }

        /// <inheritdoc />
        public override void UseFeature(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseMvc();
        }
    }
}