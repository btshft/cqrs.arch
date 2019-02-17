using System.Linq;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.Infrastructure.Workflow
{
    public static class WorkflowRegistrationExtensions
    {
        public static IServiceCollection AddWorkflow<TWorkflow, TRegistry, TCoordinator>(
            this IServiceCollection services)
            where TWorkflow : class, IWorkflow
            where TRegistry : class, IWorkflowRegistry<TWorkflow>
            where TCoordinator : class, IWorkflowCoordinator<TWorkflow>
        {
            services.AddScoped<IWorkflowRegistry<TWorkflow>, TRegistry>();

            var handlingTypes = new
            {
                CoordinatorType = typeof(TCoordinator),
                HandledEvents = typeof(TCoordinator).GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Where(i => i.GetGenericTypeDefinition() == typeof(IWorkflowStateHandler<,>))
                    .Select(i => i.GenericTypeArguments[0])
                    .Distinct()
                    .ToArray()
            };

            foreach (var eventType in handlingTypes.HandledEvents)
            {
                var serviceType = typeof(INotificationHandler<>).MakeGenericType(eventType);
                var implType = handlingTypes.CoordinatorType;
                
                if (services.Any(s => s.ServiceType == serviceType && s.ImplementationType == implType))
                    continue;

                services.AddTransient(serviceType, implType);
            }

            return services;
        }
    }
}