using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cqrs.Domain.Data
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _context;

        public EntityFrameworkRepository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
            _context = dbContext;
        }

        /// <inheritdoc />
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity, CancellationToken cancellation)
        {
            await _dbSet.AddAsync(entity, cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);

            await _context.SaveChangesAsync(cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(object id, CancellationToken cancellation)
        {
            var entity = await _dbSet.FindAsync(id)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            _dbSet.Remove(entity);
            
            await _context.SaveChangesAsync(cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellation)
        {
            _dbSet.Update(entity);
            
            await _context.SaveChangesAsync(cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(object id)
        {
            return await _dbSet.FindAsync(id)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation)
        {
            return await _dbSet.CountAsync(expression, cancellationToken: cancellation)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}