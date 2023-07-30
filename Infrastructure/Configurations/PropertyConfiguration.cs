using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PropertyConfiguration : BaseEntityConfiguration<Property, int>
    {
        public override void Configure(EntityTypeBuilder<Property> modelBuilder)
        {
            base.Configure(modelBuilder);
            // Property has one User (One-to-Many)
            modelBuilder.HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.UserId);

            // Property has many Photos (One-to-Many)
            modelBuilder.HasMany(p => p.Photos);

            // Property has many ServiceRequests (One-to-Many)
            modelBuilder.HasMany(p => p.ServiceRequests)
                .WithOne(sr => sr.Property)
                .HasForeignKey(sr => sr.PropertyId);
        }
    }
}