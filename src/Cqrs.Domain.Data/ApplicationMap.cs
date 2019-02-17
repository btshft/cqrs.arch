using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cqrs.Domain.Data
{
    public class ApplicationMap : IEntityTypeConfiguration<Application>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}