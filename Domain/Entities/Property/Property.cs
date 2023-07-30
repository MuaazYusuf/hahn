using System.Globalization;
using Domain.Base;
using Domain.Enums.Properties;

namespace Domain.Entities
{
    public class Property : BaseEntity<int>
    {
        public static readonly DateTimeFormatInfo PROPERTY_RELATED_DATE_FORMATTER =
        CultureInfo.InvariantCulture.DateTimeFormat;
        public double Price { get; set; }
        public double Surface { get; set; }
        public PropertyType PropertyType { get; set; } = PropertyType.Apartment;
        public int NumberOfRooms { get; set; }
        public string? Description { get; set; }
        public List<Photo>? Photos { get; set; }
        public PropertyAddress? address;
         public List<PropertyPointOfInterest> PropertyPointOfInterests { get; set; } = new List<PropertyPointOfInterest>();

        public PropertyStatus Status { get; set; } = PropertyStatus.NotAvailable;
        public long SaleDate { get; set; }
        public long saleDate;
        public User? User { get; set; }
        public List<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public int UserId { get; set; }

    }
}