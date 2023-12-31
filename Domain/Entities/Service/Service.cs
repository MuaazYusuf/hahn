using Domain.Base;

namespace Domain.Entities
{
    public class Service : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual int ServiceProviderId { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

        public Service(string name)
        {
            Name = name;
        }

        public void SetServiceProvider(ServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProviderId = serviceProvider.Id;
        }
    }
}