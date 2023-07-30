using Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Users;

namespace Infrastructure.Data.Repositories.Users
{
    public class UserRepository : AsyncBaseRepository<User, int>, IUserRepository
    {
        public UserRepository(EFContext context) : base(context) { }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await entities.FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await entities.FirstOrDefaultAsync(user => user.Email == email);
        }
    }
}