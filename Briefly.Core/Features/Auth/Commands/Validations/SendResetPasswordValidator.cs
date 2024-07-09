namespace Briefly.Core.Features.Auth.Commands.Validations
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        public SendResetPasswordValidator()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address.");
        }
    }
}
