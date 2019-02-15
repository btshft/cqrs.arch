using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cqrs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cqrs.Domain.Data
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public EntityFrameworkRepository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<TEntity>();
        }
        
        /// <inheritdoc />
        public async Task<IReadOnlyCollection<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync()
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity)
                .ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <inheritdoc />
        public async Task RemoveAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id)
                .ConfigureAwait(continueOnCapturedContext: false);
            
            _dbSet.Remove(entity);
        }

        /// <inheritdoc />
        public Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(object id)
        {
            return await _dbSet.FindAsync(id)
                .ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}