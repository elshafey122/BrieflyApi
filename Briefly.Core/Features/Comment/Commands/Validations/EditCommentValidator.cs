
namespace Briefly.Core.Features.CommentsArticle.Commands.Validations
{
    public class EditCommentValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentValidator()
        {
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(s => s.updatedtext)
               .NotEmpty().WithMessage("Text must not be empty is required.");
        }
    }
}