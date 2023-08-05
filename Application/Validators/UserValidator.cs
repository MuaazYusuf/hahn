using FluentValidation;
using Domain.Entities;
using Application.Base;
using FluentValidation.Results;

namespace Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").MinimumLength(8).WithMessage("Password must be at least 8 characters long").Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(x => x.DateOfBirth).Must(CheckAgeValidity).When(x => x.DateOfBirth.HasValue).WithMessage("Invalid date of birth");
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$").WithMessage("Invalid date of birth");
        }

        protected bool CheckAgeValidity(DateTime? date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Value.Year;

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