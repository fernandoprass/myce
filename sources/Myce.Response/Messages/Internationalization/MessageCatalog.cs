using System.Globalization;

namespace Myce.Response.Messages.Internationalization;

internal sealed class MessageCatalog
{
   private readonly Dictionary<string, string> _templates;
   private readonly Dictionary<string, Dictionary<string, Variable>> _variables;

   internal IEnumerable<KeyValuePair<string, string>> Templates => _templates;
   internal IEnumerable<Variable> Variables
      => _variables.Values.SelectMany(variables => variables.Values);

   internal MessageCatalog()
   {
      _templates = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      _variables = new Dictionary<string, Dictionary<string, Variable>>(
         StringComparer.OrdinalIgnoreCase);
   }

   internal MessageCatalog(
      IEnumerable<KeyValuePair<string, string>> templates,
      IEnumerable<Variable>? variables = null)
      : this()
   {
      foreach (var template in templates)
      {
         _templates[template.Key] = template.Value ?? string.Empty;
      }

      if (variables != null)
      {
         foreach (var variable in variables)
         {
            AddVariable(
               CultureName.Get(variable.Language),
               variable.Name,
               variable.Value);
         }
      }
   }

   internal void AddTemplate(CultureInfo culture, string text)
      => _templates[culture.Name] = text ?? string.Empty;

   internal void AddVariable(CultureInfo culture, string name, string value)
   {
      if (!_variables.TryGetValue(culture.Name, out var languageVariables))
      {
         languageVariables = new Dictionary<string, Variable>(
            StringComparer.OrdinalIgnoreCase);
         _variables[culture.Name] = languageVariables;
      }

      if (languageVariables.TryGetValue(name, out var existing))
         existing.Value = value ?? string.Empty;
      else
         languageVariables[name] =
            new Variable(culture.Name, name, value ?? string.Empty);
   }

   internal bool TryGetVariable(
      CultureInfo culture,
      string name,
      out Variable? variable)
   {
      if (_variables.TryGetValue(culture.Name, out var languageVariables) &&
          languageVariables.TryGetValue(name, out var value))
      {
         variable = value;
         return true;
      }

      variable = null;
      return false;
   }

   internal bool TryGetTemplate(
      CultureInfo culture,
      out string template,
      out CultureInfo resolvedCulture)
   {
      if (_templates.TryGetValue(culture.Name, out var value))
      {
         template = value;
         resolvedCulture = CultureName.Get(_templates.Keys.First(key =>
            key.Equals(culture.Name, StringComparison.OrdinalIgnoreCase)));
         return true;
      }

      template = string.Empty;
      resolvedCulture = null!;
      return false;
   }

   internal MessageCatalog Copy()
      => new(
         _templates,
         Variables.Select(variable => new Variable(
            variable.Language,
            variable.Name,
            variable.Value)));
}
