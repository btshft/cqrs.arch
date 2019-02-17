using System.Linq;
using AutoMapper;
using Cqrs.AppServices.Application.EventHandlers;
using Cqrs.AppServices.Application.Validation;
using Cqrs.AppServices.Application.Workflow;
using Cqrs.Contracts.Application;
using Cqrs.Domain;
using Cqrs.Domain.Data;
using Cqrs.Infrastructure.Behaviors;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.DependencyInjection;
using Cqrs.Infrastructure.Dispatcher;
using Cqrs.Infrastructure.Mapper;
using Cqrs.Infrastructure.Validation;
using Cqrs.Infrastructure.Workflow;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cqrs.ComponentRegistrar
{
    /// <summary>
    /// Компонент регистрации зависимостей приложения.
    /// </summary>
    public class ApplicationRegistrar
    {
        public static void RegisterComponents(IServiceCollection services)
        {
            // Core componensts
            services.AddScoped(typeof(IComponentProvider<>), typeof(ComponentProvider<>));
            
            RegisterMediator(services);
            RegisterDataComponents(services);
            RegisterTypeMapper(services);
            RegisterValidators(services);
            RegisterWorkflows(services);
        }

        private static void RegisterWorkflows(IServiceCollection services)
        {
            services.AddWorkflow<ApplicationGuaranteeWorkflow, ApplicationGuaranteeWorkflowRegistry, ApplicationGuaranteeWorkflowCoordinator>();
        }
        
        private static void RegisterValidators(IServiceCollection services)
        {
            var closedValidatorTypes = typeof(CreateApplicationDraftCommandValidator).Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    ValidatorType = t,
                    MessageType = t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Where(i => i.GetGenericTypeDefinition() == typeof(IMessageValidator<>))
                        .Select(i => i.GetGenericArguments()[0])
                        .FirstOrDefault()
                })
                .Where(e => e.MessageType != null)
                .ToArray();

            foreach (var validatorType in closedValidatorTypes)
            {
                services.AddScoped(typeof(IMessageValidator<>).MakeGenericType(validatorType.MessageType),
                    validatorType.ValidatorType);
            }
        }
        
        private static void RegisterMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationEventsHandler));
            services.AddScoped<IMessageDispatcher, MessageDispatcher>();
            
            // Decorators
            // ProcessOutputEventsBehavior - должен стоять раньше чем TransactionalBehavior
            // т.к. он должен обрабатывать события после завершения транзакции.
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ProcessOutputEventsBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(MessageValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));
        }

        private static void RegisterDataComponents(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            services.AddScoped<IDataSession, EntityFrameworkDataSession>();
            services.AddScoped<IDataSessionFactory, EntityFrameworkDataSessionFactory>();
            services.AddScoped<ApplicationDbContext>();
        }

        private static void RegisterTypeMapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Application, ApplicationDto>()
                    .ForMember(d => d.Status,
                        opt => opt.MapFrom(s => (int)s.Status));
            });
            
            services.AddScoped<ITypeMapper, TypeMapper>();
        }
    }
}