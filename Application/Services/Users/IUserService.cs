using Domain.Users.Entities;
using Application.Base;
namespace Application.Services.Users
{
    public interface IUserService : IBaseService<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}