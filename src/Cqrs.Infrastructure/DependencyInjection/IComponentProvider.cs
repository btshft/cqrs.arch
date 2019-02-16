using System.Collections.Generic;

namespace Cqrs.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Провайдер компонентов.
    /// </summary>
    /// <typeparam name="TComponent">Тип компонента.</typeparam>
    public interface IComponentProvider<out TComponent>
    {
        /// <summary>
        /// Возвращает компонент.
        /// </summary>
        /// <returns>Компонент или null.</returns>
        TComponent GetComponent();
        
        /// <summary>
        /// Возвращает обязательный компонент.
        /// Если компонент не был найдент - кидает исключение.
        /// </summary>
        /// <returns>Компонент.</returns>
        TComponent GetRequiredComponent();
        
        /// <summary>
        /// Возвращает перечень компонентов.
        /// </summary>
        /// <returns>Перечень компонентов.</returns>
        IEnumerable<TComponent> GetComponents();
    }
}