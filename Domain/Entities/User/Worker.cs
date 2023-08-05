using Domain.Base;

namespace Domain.Entities
{
    public class Worker : BaseEntity<int>
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public int ServiceProviderId { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public void SetUser(User user)
        {
            User = user;
            UserId = user.Id;
        }

        public void SetServiceProvider(ServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            ServiceProviderId = serviceProvider.Id;
        }
    }
}