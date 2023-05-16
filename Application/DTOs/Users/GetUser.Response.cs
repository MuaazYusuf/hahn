using System.Text.Json.Serialization;

namespace Application.DTOs.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}