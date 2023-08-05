using System.Globalization;
using Domain.Base;
using Domain.Enums.Properties;

namespace Domain.Entities
{
    public class Property : BaseEntity<int>
    {
        public double Price { get; set; }
        public double Area { get; set; }
        public string PropertyType { get; set; } = PropertyTypeEnum.APARTMENT;
        public int? NumberOfRooms { get; set; }
        public string Description { get; set; }
        public PropertyAddress Address { get; set; }
        public string Status { get; set; } = PropertyStatusEnum.NOT_AVAILABLE;
        public List<Photo>? Photos { get; set; }
        public virtual Agent Agent { get; set; }
        public int AgentId { get; set; }
        public virtual List<PropertyPointOfInterest> PropertyPointOfInterests { get; set; } = new List<PropertyPointOfInterest>();
        public DateTime? SaleDate { get; set; }
        public virtual List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public virtual User CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public Property
        (
            double price,
            double area,
            string description,
            string status,
            string propertyType,
            int? numberOfRooms
        )
        {
            Price = price;
            Area = area;
            NumberOfRooms = numberOfRooms;
            Description = description;
            Status = status;
            PropertyType = propertyType;
        }

        // Setter methods for navigation properties
        public void SetAgent(Agent agent)
        {
            Agent = agent;
            AgentId = agent.Id;
        }

        public void SetAddress(PropertyAddress address)
        {
            Address = address;

        }

        public void SetPropertyPointOfInterests(List<PropertyPointOfInterest> propertyPointOfInterests)
        {
            PropertyPointOfInterests = propertyPointOfInterests;
        }

        public void SetPhotos(List<Photo> photos)
        {
            Photos = photos;
        }

        public void SetCreatedBy(User createdBy)
        {
            CreatedBy = createdBy;
            CreatedById = createdBy.Id;
        }
    }
}