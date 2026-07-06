using System.Globalization;

namespace Myce.Response.Messages.Internationalization;

internal sealed class MessageLocalization
{
   internal CultureInfo Culture { get; }
   internal string Template { get; }
   internal IReadOnlyCollection<Variable> Variables { get; }

   internal MessageLocalization(
      CultureInfo culture,
      string template,
      IReadOnlyCollection<Variable> variables)
   {
      Culture = culture;
      Template = template;
      Variables = variables;
   }
}
