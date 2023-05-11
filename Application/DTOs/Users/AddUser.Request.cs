using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Users
{
    public class AddUserRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(125)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(125)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(125)]
        public string Password { get; set; }

        [Required]
        [StringLength(125)]
        public string Email { get; set; }
    }
}