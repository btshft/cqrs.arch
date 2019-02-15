using AutoMapper;
using Cqrs.AppServices.Application.CommandHandlers;
using Cqrs.Domain.Data;
using Cqrs.Infrastructure.Data;
using Cqrs.Infrastructure.Dispatcher;
using Cqrs.Infrastructure.Mapper;
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
            RegisterMediator(services);
            RegisterDataComponents(services);
        }

        private static void RegisterMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateApplicationCommandHandler));
            services.AddScoped<IMediatorDispatcher, MediatorDispatcher>();
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
            services.AddAutoMapper();
            services.AddScoped<ITypeMapper, TypeMapper>();
        }
    }
}