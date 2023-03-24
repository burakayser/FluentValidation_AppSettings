using FluentValidation;

namespace FluentValidation_AppSettings.Infrastructure.Options
{
    public class ExampleValidator : AbstractValidator<ExampleOptions>
    {
        public ExampleValidator()
        {
            RuleFor(x => x.LogLevel).IsEnumName(typeof(LogLevel));

            RuleFor(x => x.Retries).InclusiveBetween(1, 10);
        }
    }
}
