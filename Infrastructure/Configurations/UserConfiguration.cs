using Domain.Entities.User;
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
        }
    }
}