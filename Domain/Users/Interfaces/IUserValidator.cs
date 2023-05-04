namespace Domain.Users.Interfaces
{
    public interface IUserValidator
    {
        Task<bool> ValidateAsync(User user);
    }
}