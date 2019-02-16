using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cqrs.Infrastructure.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity, CancellationToken cancellation);
        Task RemoveAsync(object id, CancellationToken cancellation);
        Task UpdateAsync(TEntity entity, CancellationToken cancellation);
        Task<TEntity> GetAsync(object id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation);
    }
}