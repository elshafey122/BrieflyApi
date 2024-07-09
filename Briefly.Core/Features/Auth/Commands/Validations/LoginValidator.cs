namespace Briefly.Core.Features.Auth.Commands.Validations
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("email is required");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("password is required");
        }
    }
}
