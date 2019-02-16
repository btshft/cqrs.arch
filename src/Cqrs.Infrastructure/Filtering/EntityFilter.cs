using System;
using System.Linq.Expressions;

namespace Cqrs.Infrastructure.Filtering
{
    public class EntityFilter<TEntity, TProjection>
    {
        public FilterQuery<TProjection> Query { get; }
        public Expression<Func<TEntity, bool>> Predicate { get; }
        
        public EntityFilter(FilterQuery<TProjection> query, Expression<Func<TEntity, bool>> predicate)
        {
            Query = query;
            Predicate = predicate;
        }
    }
    
    public class EntityFilter<TQuery, TEntity, TProjection> : EntityFilter<TEntity, TProjection>
        where TQuery : FilterQuery<TProjection>
    {
        public TQuery TypedQuery { get; }
        
        public EntityFilter(TQuery query, Expression<Func<TEntity, bool>> predicate) 
            : base(query, predicate)
        {
            TypedQuery = query;
        }
    }
}