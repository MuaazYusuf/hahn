using FluentValidation;
using Domain.Users.Entities;
using Application.Base;
namespace Domain.Users.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(8).WithMessage("Password must be at least 8 characters long");
            RuleFor(x => x.DateOfBirth).NotEmpty().Must((DateTime date) =>
            {
                int currentYear = DateTime.Now.Year;
                int dobYear = date.Year;

                if (dobYear <= currentYear && dobYear > (currentYear - 120))
                {
                    return true;
                }
                return false;
            }).WithMessage("Invalid date of birth");
        }
    }
}