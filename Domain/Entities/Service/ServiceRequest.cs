using Domain.Base;

namespace Domain.Entities
{
    public class ServiceRequest : BaseEntity<int>
    {
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }

        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
    }
}