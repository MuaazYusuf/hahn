using Domain.Base;

namespace Domain.Entities
{
    public class Photo : BaseEntity<int>
    {
        public string? Url { get; set; }
        public string? Description { get; set; }
        public int? BelongsToId { get; set; }
        public dynamic BelongsToEntity;

        public Photo()
        {
        }

        public Photo(string url, string description)
        {
            Url = url;
            Description = description;
        }
    }
}