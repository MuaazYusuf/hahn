using System.Text.Json.Serialization;
using Domain.Entities.User;

namespace Application.DTOs.Auth
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public Role Role { get; set; }
    }
}