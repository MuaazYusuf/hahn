using Domain.Base;
using Domain.Enums.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public string? Username { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int? CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User? CreatedBy { get; set; }

        public int? UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User? UpdatedBy { get; set; }

        public string Role { get; set; }
        public virtual Agent? Agent { get; set; }
        public virtual Worker? Worker { get; set; }
        public virtual ServiceProvider? ServiceProvider { get; set; }
        public virtual List<Property> CreatedProperties { get; set; } = new List<Property>();

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}