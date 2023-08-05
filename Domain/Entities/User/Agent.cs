using Domain.Base;

namespace Domain.Entities
{
    public class Agent : BaseEntity<int>
    {
        public virtual User? User { get; set; }
        public int? UserId { get; set; }
        public virtual List<Property> OwnedProperties { get; set; } = new List<Property>();
        public virtual List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}