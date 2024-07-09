
namespace Briefly.Core.Features.Auth.Commands.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.FirstName)
           .NotEmpty().WithMessage("First name is required.")
           .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&./^_]+$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character from @$!%*?&./^_");

            RuleFor(user => user.ConfirmPassword)
           .NotEmpty().WithMessage("Confirm password is required.")
           .Equal(user => user.Password).WithMessage("Confirm password does not match the password.");
        }
    }
}
