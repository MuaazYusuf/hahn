using static BCrypt.Net.BCrypt;

namespace Application.Helpers
{
    public static class PasswordHelper
    {
        public static string Hash(string password)
        {
            return HashPassword(password, GenerateSalt());
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return Verify(password, hashedPassword);
        }
    }
}