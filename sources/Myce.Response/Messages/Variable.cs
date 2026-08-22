using System.Text.Json.Serialization;

namespace Myce.Response.Messages
{
   public class Variable {
      private const string _defaultLanguageKey = "en-US";

      [JsonIgnore]
      public string Language { get; private set; } = _defaultLanguageKey;
      public string Name { get; set; } = string.Empty;
      public string Value { get; set; } = string.Empty;

      public Variable() { }

      public Variable(string name, string value) : this(_defaultLanguageKey, name, value) { }

      public Variable(string language, string name, string value)
         : this(Internationalization.CultureName.Get(language), name, value) { }

      public Variable(CultureInfo culture, string name, string value)
      {
         Language = culture?.Name ?? throw new ArgumentNullException(nameof(culture));
         Name = name;
         Value = value;
      }

      public override string ToString() {
         return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}, {nameof(Language)}: {Language}";
      }
   }
}
