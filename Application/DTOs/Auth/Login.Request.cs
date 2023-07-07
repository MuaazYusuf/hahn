using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!; // Add required when upgrading to NET core 7
    }
}