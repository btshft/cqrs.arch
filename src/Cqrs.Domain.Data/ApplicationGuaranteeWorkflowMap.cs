using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cqrs.Domain.Data
{
    public class ApplicationGuaranteeWorkflowMap : IEntityTypeConfiguration<ApplicationGuaranteeWorkflow>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<ApplicationGuaranteeWorkflow> builder)
        {
            builder.HasKey(a => a.WorkflowId);
        }
    }
}