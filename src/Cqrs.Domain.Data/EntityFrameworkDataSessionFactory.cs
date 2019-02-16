using Cqrs.Infrastructure.Data;

namespace Cqrs.Domain.Data
{
    public class EntityFrameworkDataSessionFactory : IDataSessionFactory
    {
        private readonly ApplicationDbContext _dbContext;

        public EntityFrameworkDataSessionFactory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public IDataSession CreateDataSession()
        {
            return new EntityFrameworkDataSession(_dbContext);
        }
    }
}