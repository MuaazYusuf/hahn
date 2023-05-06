using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            modelBuilder.HasKey(e => e.Id);
            modelBuilder.Property(e => e.CreatedAt).IsRequired();
            modelBuilder.Property(e => e.UpdatedAt).IsRequired(false);
        }
    }
}