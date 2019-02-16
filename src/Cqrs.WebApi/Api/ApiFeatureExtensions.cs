using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.WebApi.Api
{
    public static class ApiFeatureExtensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration configuration)
        {
            var enabledFeatures = configuration
                .GetSection("Api:Features")
                .Get<string[]>();

            var featureTypes = typeof(ApiFeature).Assembly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => typeof(ApiFeature).IsAssignableFrom(t))
                .Select(t => new
                {
                    FeatureType = t, 
                    FeatureName = t.GetCustomAttribute<ApiFeatureAttribute>()?.Name,
                    FeatureOrder = t.GetCustomAttribute<ApiFeatureAttribute>()?.Order
                })
                .Where(f => f.FeatureName != null)
                .Where(f => enabledFeatures.Contains(f.FeatureName, StringComparer.InvariantCultureIgnoreCase))
                .OrderBy(f => f.FeatureOrder)
                .Select(f => f.FeatureType)
                .ToArray();              
            
            foreach (var featureType in featureTypes)
            {
                var feature = (ApiFeature)Activator.CreateInstance(featureType);     
                feature.RegisterFeature(services, configuration);                  
                services.AddSingleton(feature);      
            }
            
            return services;
        }

        public static IApplicationBuilder UseFeatures(this IApplicationBuilder builder, IConfiguration configuration)
        {
            var features = builder.ApplicationServices.GetServices<ApiFeature>()
                .Select(f => new
                {
                    Feature = f,
                    Order = f.GetType()
                        .GetCustomAttribute<ApiFeatureAttribute>().Order
                });
            
            foreach (var feature in features.OrderBy(f => f.Order).Select(f => f.Feature))
                feature.UseFeature(builder, configuration);

            return builder;
        }

        public static bool IsFeatureEnabled<TFeature>(this IConfiguration configuration) where TFeature : ApiFeature
        {
            var featureAttribute = typeof(TFeature).GetCustomAttribute<ApiFeatureAttribute>();
            if (featureAttribute != null)
            {
                var enabledFeatures = configuration.GetSection("Settings:Features")
                    .Get<string[]>();

                if (enabledFeatures.Contains(featureAttribute.Name))
                    return true;
            }

            return false;
        }
    }
}