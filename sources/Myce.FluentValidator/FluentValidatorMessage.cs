using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System.Globalization;

namespace Myce.FluentValidator;

internal sealed class FluentValidatorMessage(
   Message message,
   CultureInfo culture,
   IValidationMessageProvider messageProvider,
   bool wasRuleBroken)
{
   public Message Message { get; } =
         message is ValidationError.BuiltInValidationMessage &&
         messageProvider.TryCreate(
            message.Code,
            culture,
            message.Variables,
            out var localized)
            ? localized
            : message.WithLanguage(culture);
   public bool WasRuleBroken { get; set; } = wasRuleBroken;
}
