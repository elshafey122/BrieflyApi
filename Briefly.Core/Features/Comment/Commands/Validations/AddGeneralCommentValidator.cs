
namespace Briefly.Core.Features.Rss.Commands.Validations
{
    public class AddGeneralCommentValidator : AbstractValidator<AddGeneralCommentCommand>
    {
        public AddGeneralCommentValidator()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(s => s.text)
               .NotEmpty().WithMessage("Text must not be empty is required.");
        }
    }
}
