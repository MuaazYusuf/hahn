using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PropertyPointOfInterest> PropertyPointOfInterests { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceProvider> ServiceProvider { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Agent> Agent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PointOfInterestConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyPointOfInterestConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceRequestConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new AgentConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceProviderConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}