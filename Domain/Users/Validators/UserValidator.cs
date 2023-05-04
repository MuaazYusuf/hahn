using Domain.Users.Interfaces;
using FluentValidation;

namespace Domain.Users.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserValidator _userValidator;
        public UserValidator(IUserValidator userValidator)
        {
            _userValidator = userValidator;
        }

        public async Task<bool> ValidateAsync(User user)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(8).WithMessage("Password must be at least 8 characters long");
            RuleFor(x => x.DateOfBirth).NotEmpty().Must(checkAgeValidity).WithMessage("Invalid date of birth");

            return await _userValidator.ValidateAsync(user);
        }

        protected bool checkAgeValidity(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }
            return false;
        }
    }
}