using Domain.Base;

namespace Domain.Entities
{
    public class ServiceProvider : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public List<Worker> Workers { get; set; } = new List<Worker>();

        public ServiceProvider(string name)
        {
            Name = name;
        }

        public void SetUser(User user)
        {
            User = user;
            UserId = user.Id;
        }
    }
}