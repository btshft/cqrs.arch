using Microsoft.EntityFrameworkCore;

namespace Cqrs.Domain.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() 
            : base(InMemoryApplicationDbContextBuilder.GetOptions()) 
        { }
        
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationGuaranteeWorkflow> ApplicationGuaranteeWorkflows { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationMap());     
            modelBuilder.ApplyConfiguration(new ApplicationGuaranteeWorkflowMap());     
            
            base.OnModelCreating(modelBuilder);
        }
    }
}