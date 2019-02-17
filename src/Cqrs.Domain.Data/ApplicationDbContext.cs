using Microsoft.EntityFrameworkCore;

namespace Cqrs.Domain.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
            : base(InMemoryApplicationDbContextBuilder.GetOptions()) 
        { }
        
        public DbSet<Application> Applications { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationMap());            
            base.OnModelCreating(modelBuilder);
        }
    }
}