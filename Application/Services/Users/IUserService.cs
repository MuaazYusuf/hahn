using Domain.Entities.User;
using Application.Base;
namespace Application.Services.Users
{
    public interface IUserService : IBaseService<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);

        Task LogoutAsync(string email);
    }
}