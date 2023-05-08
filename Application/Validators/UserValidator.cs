using FluentValidation;
using Domain.Users.Entities;
using Application.Base;
using FluentValidation.Results;

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
            RuleFor(x => x.DateOfBirth).NotEmpty().Must(checkAgeValidity).WithMessage("Invalid date of birth");
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

        public override async Task<ValidationResult> ValidateAsync(ValidationContext<User> context, CancellationToken cancellationToken = default)
        {
            var result = await base.ValidateAsync(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            return result;
        }
    }
}