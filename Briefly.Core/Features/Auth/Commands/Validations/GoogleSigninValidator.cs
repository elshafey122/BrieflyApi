namespace Briefly.Core.Features.Auth.Commands.Validations
{
    public class GoogleSigninValidator : AbstractValidator<GoogleSigninCommand>
    {
        public GoogleSigninValidator()
        {
            ApplyRoles();
        }
        public void ApplyRoles()
        {
            RuleFor(x => x.IdToken).NotEmpty().WithMessage("Idtoken is required");
        }
    }
}
