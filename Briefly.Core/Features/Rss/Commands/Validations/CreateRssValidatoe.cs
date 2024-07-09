namespace Briefly.Core.Features.Rss.Commands.Validations
{
    public class CreateRssValidatoe : AbstractValidator<CreateRssCommand>
    {
        public CreateRssValidatoe()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(rss => rss.RssUrl)
                .NotEmpty().WithMessage("rss is required.");
        }
    }
}
