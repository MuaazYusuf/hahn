using Domain.Base;
using Domain.Enums;

namespace Domain.Entities
{
    public class ServiceRequest : BaseEntity<int>
    {
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
        public virtual Agent RequestedBy { get; set; }
        public int RequestedById { get; set; }
        public string Status { get; set; } = ActionStatusEnum.PENDING;
        public DateTime? ActionTakenAt { get; set; }
        public virtual User? AssignedTo { get; set; }
        public string? RejectionReason { get; set; }

        public void SetRequestedBy(Agent requestedBy)
        {
            RequestedBy = requestedBy;
            RequestedById = requestedBy.Id;
        }

        public void AssignTo(User assignedTo)
        {
            AssignedTo = assignedTo;
        }

        public void SetCreatedBy(Agent requestedBy)
        {
            RequestedBy = requestedBy;
        }

        public void SetService(Service service)
        {
            Service = service;
        }

        public void SetProperty(Property property)
        {
            Property = property;
        }
    }
}