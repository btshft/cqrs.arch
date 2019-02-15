using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cqrs.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyCollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(object id);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(object id);
    }
}