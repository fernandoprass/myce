using System.Globalization;
using System.Text;

namespace Myce.Response.Messages.Internationalization;

internal static class MessageLocalizer
{
   internal static MessageLocalization Localize(
      MessageCatalog catalog,
      CultureInfo requestedCulture,
      CultureInfo currentCulture,
      string fallbackText)
   {
      var template = ResolveTemplate(
         catalog,
         requestedCulture,
         currentCulture,
         fallbackText);

      var variables = catalog.Variables
         .Select(variable => variable.Name)
         .Where(name => !string.IsNullOrWhiteSpace(name))
         .Distinct(StringComparer.OrdinalIgnoreCase)
         .Select(name => ResolveVariable(
            catalog,
            name,
            template.Culture,
            currentCulture))
         .Where(variable => variable != null)
         .Cast<Variable>()
         .ToList()
         .AsReadOnly();

      return new MessageLocalization(
         template.Culture,
         template.Template,
         variables);
   }

   internal static string Format(MessageLocalization localization)
   {
      if (string.IsNullOrWhiteSpace(localization.Template))
         return string.Empty;

      var builder = new StringBuilder(localization.Template);

      foreach (var variable in localization.Variables)
      {
         builder.Replace("{" + variable.Name + "}", variable.Value ?? string.Empty);
         builder.Replace("[" + variable.Name + "]", variable.Value ?? string.Empty);
      }

      return builder.ToString();
   }

   private static (string Template, CultureInfo Culture) ResolveTemplate(
      MessageCatalog catalog,
      CultureInfo requestedCulture,
      CultureInfo currentCulture,
      string fallbackText)
   {
      var candidates = new[]
      {
         requestedCulture,
         currentCulture,
         CultureName.Get(Message.DefaultLanguage)
      };

      foreach (var candidate in candidates
         .GroupBy(candidate => candidate.Name, StringComparer.OrdinalIgnoreCase)
         .Select(group => group.First()))
      {
         if (catalog.TryGetTemplate(
            candidate,
            out var template,
            out var resolvedCulture))
         {
            return (template, resolvedCulture);
         }
      }

      var first = catalog.Templates.FirstOrDefault();
      return !string.IsNullOrWhiteSpace(first.Key)
         ? (first.Value, CultureName.Get(first.Key))
         : (fallbackText, currentCulture);
   }

   private static Variable? ResolveVariable(
      MessageCatalog catalog,
      string name,
      CultureInfo resolvedCulture,
      CultureInfo currentCulture)
   {
      var candidates = new[]
      {
         resolvedCulture,
         currentCulture,
         CultureName.Get(Message.DefaultLanguage)
      };

      foreach (var candidate in candidates
         .GroupBy(candidate => candidate.Name, StringComparer.OrdinalIgnoreCase)
         .Select(group => group.First()))
      {
         if (catalog.TryGetVariable(candidate, name, out var variable))
            return variable;
      }

      return catalog.Variables.FirstOrDefault(item =>
         item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
   }
}
