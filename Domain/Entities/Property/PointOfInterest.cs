using Domain.Base;


namespace Domain.Entities
{
    public class PointOfInterest : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<PropertyPointOfInterest> PropertyPointOfInterests { get; set; } = new List<PropertyPointOfInterest>();

        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}