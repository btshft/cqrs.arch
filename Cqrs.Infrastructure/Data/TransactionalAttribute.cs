using System;

namespace Cqrs.Infrastructure.Data
{
    /// <summary>
    /// Атрибут транзакции.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TransactionalAttribute : Attribute
    {
        
    }
}