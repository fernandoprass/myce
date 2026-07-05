namespace Myce.Response.Messages
{
   public class Variable {
      private const string _defaultLanguageKey = "en-US";
      public string Language { get; private set; } = _defaultLanguageKey;
      public string Name { get; set; } = string.Empty;
      public string Value { get; set; } = string.Empty;

      public Variable() { }

      public Variable(string name, string value) : this(_defaultLanguageKey, name, value) { }

      public Variable(string language, string name, string value)
      {
         Language = language;
         Name = name;
         Value = value;
      }

      public override string ToString() {
         return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}, {nameof(Language)}: {Language}";
      }
   }
}
