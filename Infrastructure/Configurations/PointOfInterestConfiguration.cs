using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PointOfInterestConfiguration : BaseEntityConfiguration<PointOfInterest, int>
    {
        public override void Configure(EntityTypeBuilder<PointOfInterest> modelBuilder)
        {
            base.Configure(modelBuilder);
        }
    }
}