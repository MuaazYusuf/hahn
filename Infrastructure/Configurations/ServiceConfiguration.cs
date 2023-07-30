using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ServiceConfiguration : BaseEntityConfiguration<Service, int>
    {
        public override void Configure(EntityTypeBuilder<Service> modelBuilder)
        {
            base.Configure(modelBuilder);
            // Configure the one-to-many relationship between Service and ServiceRequest
            modelBuilder.HasMany(s => s.ServiceRequests)
                .WithOne(sr => sr.Service)
                .HasForeignKey(sr => sr.ServiceId);
        }
    }
}