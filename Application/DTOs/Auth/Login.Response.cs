using System.Text.Json.Serialization;

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
    }
}