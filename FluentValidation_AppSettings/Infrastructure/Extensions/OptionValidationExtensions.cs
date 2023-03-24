using FluentValidation;
using FluentValidation_AppSettings.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FluentValidation_AppSettings.Infrastructure.Extensions
{
    public static class OptionValidationExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(p => new FluentValidationOptions<TOptions>(optionsBuilder.Name, p.GetService<IValidator<TOptions>>()));
            return optionsBuilder;
        }
    }


    public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly IValidator<TOptions> _validator;

        public FluentValidationOptions(string name, IValidator<TOptions> validator) : this(name)
        {
            _validator = validator;
        }

        public FluentValidationOptions(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string Name { get; }

        public ValidateOptionsResult Validate(string name, TOptions options)
        {
            // Null name is used to configure all named options.
            if (Name != null && Name != name)
            {
                // Ignored if not validating this instance.
                return ValidateOptionsResult.Skip;
            }

            // Ensure options are provided to validate against
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var validationResult = _validator.Validate(options);

            if (validationResult.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            var errors = validationResult.Errors.Select(error => $"Option validation failed for '{error.PropertyName}' with error : {error.ErrorMessage}");

            return ValidateOptionsResult.Fail(errors);
        }
    }
}
