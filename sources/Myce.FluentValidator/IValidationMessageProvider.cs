using System.Collections.Generic;
using System.Globalization;
using Myce.Response.Messages;

namespace Myce.FluentValidator;

public interface IValidationMessageProvider
{
   bool TryCreate(
      string code,
      CultureInfo culture,
      IEnumerable<Variable> arguments,
      out ErrorMessage message);
}
