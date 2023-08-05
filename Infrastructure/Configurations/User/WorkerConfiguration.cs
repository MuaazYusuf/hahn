using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class WorkerConfiguration : BaseEntityConfiguration<Worker, int>
    {
        public override void Configure(EntityTypeBuilder<Worker> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.HasQueryFilter(w => w.IsDeleted == false);
        }
    }
}