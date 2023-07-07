using Domain.Entities.User;
using Domain.Base;
namespace Domain.Users.Interfaces
{
    public interface IUserRepository : IBaseAsyncRepository<User, int>
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}