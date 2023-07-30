using Domain.Entities;
using Domain.Base;
namespace Domain.Interfaces.Users
{
    public interface IUserRepository : IBaseAsyncRepository<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}