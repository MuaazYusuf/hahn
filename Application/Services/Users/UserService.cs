using Domain.Entities;
using Application.DBExceptions;
using Domain.Interfaces.Users;

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
                throw new DbExecutionException("Email", "Email already exists");
            }
            return await base.AddAsync(user);
        }

        public async Task LogoutAsync(string email)
        {
            User? user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                await _userRepository.UpdateAsync(user);
            }
        }
    }
}