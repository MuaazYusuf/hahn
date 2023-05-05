using Domain.Users.Entities;
using Application.Base;
namespace Application.Services.Users
{
    public interface IUserService : IBaseService<User, int>
    {
        protected Task<User?> GetByUsernameAsync(string username);
        protected Task<User?> GetByEmailAsync(string email);
    }
}