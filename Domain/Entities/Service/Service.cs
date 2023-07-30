using Domain.Base;

namespace Domain.Entities
{
    public class Service : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}