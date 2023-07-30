using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PhotoConfiguration : BaseEntityConfiguration<Photo, int>
    {
        public override void Configure(EntityTypeBuilder<Photo> modelBuilder)
        {
            base.Configure(modelBuilder);
        }
    }
}