using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PropertyPointOfInterestConfiguration : BaseEntityConfiguration<PropertyPointOfInterest, int>
    {
        public override void Configure(EntityTypeBuilder<PropertyPointOfInterest> modelBuilder)
        {
            base.Configure(modelBuilder);
            // Configure the many-to-many relationship between Property and PointOfInterest
            modelBuilder.HasKey(pp => new { pp.PropertyId, pp.PointOfInterestId });

            modelBuilder
                .HasOne(pp => pp.Property)
                .WithMany(p => p.PropertyPointOfInterests)
                .HasForeignKey(pp => pp.PropertyId);

            modelBuilder.HasOne(pp => pp.PointOfInterest)
                .WithMany(poi => poi.PropertyPointOfInterests)
                .HasForeignKey(pp => pp.PointOfInterestId);
            modelBuilder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}