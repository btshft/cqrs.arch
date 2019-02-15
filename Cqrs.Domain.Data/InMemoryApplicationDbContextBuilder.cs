using Microsoft.EntityFrameworkCore;

namespace Cqrs.Domain.Data
{
    public static class InMemoryApplicationDbContextBuilder
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("test_db")
                .Options;
        }
    }
}