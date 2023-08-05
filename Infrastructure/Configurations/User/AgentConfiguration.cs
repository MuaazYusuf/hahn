using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AgentConfiguration : BaseEntityConfiguration<Agent, int>
    {
        public override void Configure(EntityTypeBuilder<Agent> modelBuilder)
        {
            base.Configure(modelBuilder);
            // Agent has many Properties (One-to-Many)
            modelBuilder.HasMany(a => a.OwnedProperties)
                .WithOne(p => p.Agent)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasMany(a => a.ServiceRequests)
                .WithOne(p => p.RequestedBy)
                .HasForeignKey(p => p.RequestedById)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.HasQueryFilter(a => a.IsDeleted == false);
        }
    }
}