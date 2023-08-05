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

            // If an admin created it for agent
            modelBuilder.HasMany(u => u.CreatedProperties)
                .WithOne(p => p.CreatedBy)
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasOne(u => u.Agent)
                .WithOne(a => a.User)
                .HasForeignKey<Agent>(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasOne(u => u.ServiceProvider)
                .WithOne(sp => sp.User)
                .HasForeignKey<ServiceProvider>(sp => sp.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.HasOne(u => u.Worker)
                .WithOne(w => w.User)
                .HasForeignKey<Worker>(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}