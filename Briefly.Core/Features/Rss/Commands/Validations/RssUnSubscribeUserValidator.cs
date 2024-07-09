namespace Briefly.Core.Features.Rss.Commands.Validations
{
    public class RssUnSubscribeUserValidator : AbstractValidator<RssUserUnSubscribeCommand>
    {
        public RssUnSubscribeUserValidator()
        {
            ApplyValidationRules();
        }
        private void ApplyValidationRules()
        {
            RuleFor(rss => rss.RssId)
                .NotEmpty().WithMessage("rss is required.");
        }
    }
}
