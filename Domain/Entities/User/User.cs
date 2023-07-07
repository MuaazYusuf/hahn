using Domain.Base;
using System.Text.Json.Serialization;

namespace Domain.Entities.User
{
    public class User : BaseEntity<int>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}