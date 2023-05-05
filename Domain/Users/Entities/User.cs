using Domain.Base;

namespace Domain.Users.Entities
{
    public class User : BaseEntity<int>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}