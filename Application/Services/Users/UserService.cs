using Domain.Users.Entities;
using Domain.Users.Interfaces;
using Application.DBExceptions;
namespace Application.Services.Users
{
    public class UserService : BaseService<User, int>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public new async Task<User> AddAsync(User user)
        {
            var exists = await this.GetByEmailAsync(user.Email);
            if (exists != null)
            {
                throw new DbExecutionException("User with this email already exists");
            }
            return await base.AddAsync(user);
        }

    }
}