using System;
using System.Collections.Generic;

namespace Cqrs.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Провайдер компонентов.
    /// </summary>
    /// <typeparam name="TComponent">Тип компонента.</typeparam>
    public class ComponentProvider<TComponent> : IComponentProvider<TComponent>
    {
        private readonly IServiceProvider _serviceProvider;

        public ComponentProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public TComponent GetComponent()
        {
            return (TComponent)_serviceProvider.GetService(typeof(TComponent));
        }

        /// <inheritdoc />
        public TComponent GetRequiredComponent()
        {
            var component = GetComponent();
            if (component == null)
                throw new InvalidOperationException($"Компонент типа '{typeof(TComponent)}' не зарегистрирован");

            return component;
        }

        /// <inheritdoc />
        public IEnumerable<TComponent> GetComponents()
        {
            return (IEnumerable<TComponent>) _serviceProvider.GetService(typeof(IEnumerable<TComponent>));
        }
    }
}