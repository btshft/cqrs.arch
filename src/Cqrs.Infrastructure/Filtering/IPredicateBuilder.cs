using System;
using System.Linq.Expressions;

namespace Cqrs.Infrastructure.Filtering
{
    /// <summary>
    /// Билдер предиката фильтрации.
    /// </summary>
    public interface IPredicateBuilder<TEntity>
    {
        IPredicateBuilder<TEntity> And(Expression<Func<TEntity, bool>> expression);
        IPredicateBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expression);
    }
}