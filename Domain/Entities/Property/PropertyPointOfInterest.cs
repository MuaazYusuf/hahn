using Domain.Base;


namespace Domain.Entities
{
    public class PropertyPointOfInterest : BaseEntity<int>
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public int PointOfInterestId { get; set; }
        public PointOfInterest PointOfInterest { get; set; }
    }
}