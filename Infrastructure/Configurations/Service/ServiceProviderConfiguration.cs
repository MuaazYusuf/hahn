using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ServiceProviderConfiguration : BaseEntityConfiguration<ServiceProvider, int>
    {
        public override void Configure(EntityTypeBuilder<ServiceProvider> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.HasMany(sp => sp.Workers)
                .WithOne(w => w.ServiceProvider)
                .HasForeignKey(w => w.ServiceProviderId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.HasQueryFilter(sp => sp.IsDeleted == false);
        }
    }
}