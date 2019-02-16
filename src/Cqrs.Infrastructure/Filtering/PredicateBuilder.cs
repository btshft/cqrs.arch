using System;
using System.Linq.Expressions;

namespace Cqrs.Infrastructure.Filtering
{
    /// <summary>
    /// Билдер предиката фильтрации.
    /// </summary>
    public class PredicateBuilder<TEntity> : IPredicateBuilder<TEntity>
    {
        public Expression<Func<TEntity, bool>> Expression { get; private set; }

        public PredicateBuilder()
        {
            Expression = PredicateExpression.True<TEntity>();
        }

        /// <inheritdoc />
        public IPredicateBuilder<TEntity> And(Expression<Func<TEntity, bool>> expression)
        {
            Expression = PredicateExpression.And(Expression, expression);
            return this;
        }

        /// <inheritdoc />
        public IPredicateBuilder<TEntity> Or(Expression<Func<TEntity, bool>> expression)
        {
            Expression = PredicateExpression.Or(Expression, expression);
            return this;
        }
    }
}