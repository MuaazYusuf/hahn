using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ServiceRequestConfiguration : BaseEntityConfiguration<ServiceRequest, int>
    {
        public override void Configure(EntityTypeBuilder<ServiceRequest> modelBuilder)
        {
            base.Configure(modelBuilder);
            // ServiceRequest has one User (One-to-Many)
            modelBuilder.HasOne(s => s.CreatedBy)
                .WithMany(u => u.ServiceRequests)
                .HasForeignKey(s => s.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            // ServiceRequest has one Property (One-to-Many)
            modelBuilder.HasOne(s => s.Property)
                .WithMany(p => p.ServiceRequests)
                .HasForeignKey(s => s.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);
            // Configure the relationship with Service (one-to-many)
            modelBuilder.HasOne(sr => sr.Service)
                .WithMany(s => s.ServiceRequests)
                .HasForeignKey(sr => sr.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.HasQueryFilter(u => u.IsDeleted == false);
        }
    }
}