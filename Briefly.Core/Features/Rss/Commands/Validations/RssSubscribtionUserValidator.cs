namespace Briefly.Core.Features.Rss.Commands.Validations
{
    public class RssSubscribtionUserValidator : AbstractValidator<RssUserSubscribeCommand>
    {
        public RssSubscribtionUserValidator()
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
