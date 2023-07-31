using System.Globalization;
using Domain.Base;
using Domain.Enums.Properties;

namespace Domain.Entities
{
    public class Property : BaseEntity<int>
    {
        public double Price { get; set; }
        public double Area { get; set; }
        public PropertyType PropertyType { get; set; } = PropertyType.Apartment;
        public int NumberOfRooms { get; set; }
        public string Description { get; set; }
        public PropertyAddress Address { get; set; }
        public PropertyStatus Status { get; set; } = PropertyStatus.NotAvailable;
        public List<Photo>? Photos { get; set; }
        public virtual User Agent { get; set; }
        public int AgentId { get; set; }
        public virtual List<PropertyPointOfInterest> PropertyPointOfInterests { get; set; } = new List<PropertyPointOfInterest>();
        public DateTime? SaleDate { get; set; }
        public virtual List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public virtual User? CreatedBy { get; set; }
        public int? CreatedById { get; set; }

        public Property
        (
            double price,
            double area,
            int numberOfRooms,
            string description,
            int agentId,
            PropertyStatus status,
            PropertyType propertyType,
            int? createdById
        )
        {
            Price = price;
            Area = area;
            NumberOfRooms = numberOfRooms;
            Description = description;
            AgentId = agentId;
            Status = status;
            PropertyType = propertyType;
            CreatedById = createdById;
        }

        // Setter methods for navigation properties
        public void SetAgent(User agent)
        {
            Agent = agent;
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
        }
    }
}