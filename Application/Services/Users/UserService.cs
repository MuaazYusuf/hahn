using Domain.Users.Entities;
using Domain.Users.Interfaces;

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
    }
}