
namespace Briefly.Core.Features.Rss.Commands.Validations
{
    public class EditRssValidator : AbstractValidator<UpdateRssCommand>
    {
        public EditRssValidator()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(rss => rss.Id)
               .NotEmpty().WithMessage("Id is required.");

            RuleFor(rss => rss.Title)
                .NotEmpty().WithMessage("Title is required.").MaximumLength(100);

            RuleFor(rss => rss.Description)
                .NotEmpty().WithMessage("Description is required.").MaximumLength(300);
            
        }
    }
}
