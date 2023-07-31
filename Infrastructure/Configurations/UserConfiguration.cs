using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User, int>
    {
        public override void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Property(e => e.Email).IsRequired().HasMaxLength(255);
            modelBuilder.HasIndex(e => e.Email).IsUnique();
            modelBuilder.Property(e => e.Password).IsRequired();
            modelBuilder.HasOne(u => u.CreatedBy)
                        .WithOne()
                        .HasForeignKey<User>(u => u.CreatedById)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);
            modelBuilder.HasOne(u => u.UpdatedBy)
                        .WithOne()
                        .HasForeignKey<User>(u => u.UpdatedById)
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired(false);
            modelBuilder.HasQueryFilter(u => u.IsDeleted == false);
            // User has many Properties (One-to-Many)
            modelBuilder.HasMany(u => u.OwnedProperties)
                .WithOne(p => p.Agent)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.NoAction);
            // If an admin created it for agent
            modelBuilder.HasMany(u => u.CreatedProperties)
                .WithOne(p => p.CreatedBy)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}