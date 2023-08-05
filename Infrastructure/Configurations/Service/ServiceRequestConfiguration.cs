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
            // ServiceRequest has one Agent (One-to-Many)
            modelBuilder.HasOne(sr => sr.RequestedBy)
                .WithMany(a => a.ServiceRequests)
                .HasForeignKey(sr => sr.RequestedById)
                .OnDelete(DeleteBehavior.NoAction);

            // ServiceRequest has one Property (One-to-Many)
            modelBuilder.HasOne(sr => sr.Property)
                .WithMany(p => p.ServiceRequests)
                .HasForeignKey(sr => sr.PropertyId)
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