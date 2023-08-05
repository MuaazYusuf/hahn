using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class PropertyConfiguration : BaseEntityConfiguration<Property, int>
    {
        public override void Configure(EntityTypeBuilder<Property> modelBuilder)
        {
            base.Configure(modelBuilder);
            // Property has one User (One-to-Many)
            modelBuilder.HasOne(p => p.Agent)
                .WithMany(u => u.OwnedProperties)
                .HasForeignKey(p => p.AgentId);
            // If an admin created it for agent
            modelBuilder.HasOne(p => p.CreatedBy)
                .WithMany(u => u.CreatedProperties)
                .HasForeignKey(p => p.CreatedById);
            // Property has many Photos (One-to-Many)
            modelBuilder.HasMany(p => p.Photos);

            // Property has many ServiceRequests (One-to-Many)
            modelBuilder.HasMany(p => p.ServiceRequests)
                .WithOne(sr => sr.Property)
                .HasForeignKey(sr => sr.PropertyId);
            modelBuilder.Property(p => p.Price).IsRequired();
            modelBuilder.Property(p => p.Area).IsRequired();
            modelBuilder.Property(p => p.PropertyType).IsRequired();
            modelBuilder.Property(p => p.NumberOfRooms).IsRequired();
            modelBuilder.Property(p => p.Description).IsRequired();
            modelBuilder.OwnsOne(p => p.Address, builder =>
            {
                builder.Property(a => a.Locality).HasColumnName("Locality");
                builder.Property(a => a.PostalCode).HasColumnName("PostalCode");
                builder.Property(a => a.FormattedAddress).HasColumnName("FormattedAddress");
                builder.ToJson("Address");
            });
            modelBuilder.Property(p => p.AgentId).IsRequired();
            modelBuilder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}