using Domain.Base;


namespace Domain.Entities
{
    public class PropertyPointOfInterest : BaseEntity<int>
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int PointOfInterestId { get; set; }
        public PointOfInterest PointOfInterest { get; set; }

        public void SetPointOfInterest(PointOfInterest pointOfInterest)
        {
            PointOfInterest = pointOfInterest;
            PointOfInterestId = pointOfInterest.Id;
        }

        public void SetProperty(Property property)
        {
            Property = property;
            PropertyId = property.Id;
        }
    }
}