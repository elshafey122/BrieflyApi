
namespace Briefly.Core.Features.CommentsArticle.Commands.Validations
{
    public class AddLocalCommentValidator : AbstractValidator<AddLocalCommentCommand>
    {
        public AddLocalCommentValidator()
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
