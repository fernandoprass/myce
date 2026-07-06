using System.Text;

namespace Myce.Response.Messages
{
   public abstract class Message
   {      
      private const string DEFAULT_LANGUAGE = "en-US";

      private string _language { get; set; } = DEFAULT_LANGUAGE;

      /// <summary>
      /// List of variables to be used in the message text. Each variable consists of a name and a value, and can be referenced
      /// </summary>
      private List<Variable> _variables = [];

      /// <summary>
      /// Holds the multilingual templates for this specific message instance: [Language] -> [Template Text]
      /// </summary>
      private Dictionary<string, string> _translatedTexts = new(StringComparer.OrdinalIgnoreCase);

      /// <summary>
      /// The Default language for messages. Uses TranslateTo() to update.
      /// </summary>
      public string Language { 
         get {return _language; } 
      }

      /// <summary>
      /// Message type that determines the category or severity of the message. This property is set during object initialization and 
      /// cannot be changed afterwards.
      /// </summary>
      public MessageType Type { get; protected set; }

      /// <summary>
      /// Code that uniquely identifies the message. This property can be used to categorize messages or to provide a reference for 
      /// specific types of messages. It is initialized to an empty string and can be set during object initialization or later as needed.
      /// </summary>
      public string Code { get; set; } = string.Empty;

      /// <summary>
      /// The text content of the message. This property contains the main information or description of the message. It is initialized 
      /// to an empty string and can be set during object initialization or later as needed. The text can include placeholders for variables, 
      /// which will be replaced with their corresponding values when the Show() method is called.
      /// </summary>
      public string Text { get; private set; } = string.Empty;

      /// <summary>
      /// List of variables associated with the message. Each variable provides additional context or data for the message, and can be referenced in the Text property using placeholders.
      /// </summary>
      public IReadOnlyCollection<Variable> Variables => GetEffectiveVariables();

      /// <summary>
      /// Translates the message to the specified language. If the new language doesn't exist, the default language will be used. 
      /// This method updates both the Language and Text properties of the message instance.
      /// </summary>
      /// <param name="language">The new language</param>
      public void TranslateTo(string language)
      {
         var resolved = ResolveTemplate(language);
         _language = resolved.Language;
         Text = resolved.Template;
      }

      /// <summary>
      /// Creates an independent copy translated to the requested language.
      /// The current message is not modified.
      /// </summary>
      public Message WithLanguage(string language)
      {
         var clone = (Message)MemberwiseClone();
         clone._translatedTexts = new Dictionary<string, string>(
            _translatedTexts,
            StringComparer.OrdinalIgnoreCase);
         clone._variables = _variables
            .Select(variable => new Variable(
               variable.Language,
               variable.Name,
               variable.Value))
            .ToList();
         clone.TranslateTo(language);
         return clone;
      }
      
      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message..</param>
      public Message(MessageType type)
      {
         Type = type;
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      public Message(MessageType type, string text) : this(type, string.Empty, text, DEFAULT_LANGUAGE) { }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text) : this(type, code, text, DEFAULT_LANGUAGE) { }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      /// <param name="language">The language culture code (e.g., "en-US", "pt-BR").</param>
      public Message(MessageType type, string code, string text, string language)
      {
         Type = type;
         Code = code ?? string.Empty;
         Text = text ?? string.Empty;
         _language = language;
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, text, and a collection
      /// of variables.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      /// <param name="variables">Variables to associate with the message. Each variable provides 
      /// additional context or data for the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text, Variable variable) : this(type, code, text, DEFAULT_LANGUAGE)
      {
         _variables.Add(variable);
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, text, and a collection
      /// of variables.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      /// <param name="variables">A collection of variables to associate with the message. Each variable provides 
      /// additional context or data for the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text, IEnumerable<Variable> variables) : this(type, code, text, DEFAULT_LANGUAGE)
      {
         _variables.AddRange(variables);
      }

      /// <summary>
      /// Initializes a new instance of the Message class supporting multiple languages.
      /// </summary>
      /// <param name="type">The type of the message.</param>
      /// <param name="code">The unique message code identifier.</param>
      /// <param name="translations">The dictionary containing language keys and message templates (e.g., Key: "en-US", Value: "Inform Date of birth"). 
      /// The first language present in the dictionary will be assigned as the default language for the message.
      /// </param>
      public Message(MessageType type, string code, Dictionary<string, string> translations)
         : this(type, code, translations?.Keys.FirstOrDefault() ?? DEFAULT_LANGUAGE, translations!)
      {}

      /// <summary>
      /// Initializes a new instance of the Message class supporting multiple languages.
      /// </summary>
      /// <param name="type">The type of the message.</param>
      /// <param name="code">The unique message code identifier.</param>
      /// <param name="translations">The dictionary containing language keys and message templates (e.g., Key: "en-US", Value: "Inform Date of birth").</param>
      /// <param name="language">The language culture code (e.g., "en-US", "pt-BR").</param>
      public Message(MessageType type, string code, string language, Dictionary<string, string> translations)
      {
         if (translations == null || translations.Count == 0)
         {
            throw new ArgumentNullException(nameof(translations));
         }

         _translatedTexts = new Dictionary<string, string>(
            translations,
            StringComparer.OrdinalIgnoreCase);

         Type = type;
         Code = code ?? string.Empty;
         _language = language ?? DEFAULT_LANGUAGE;
         Text = ResolveTemplate(_language).Template;
      }

      /// <summary>
      /// Adds or updates a single text template translation for a specific language.
      /// </summary>
      /// <param name="language">The language culture code (e.g., "en-US", "pt-BR").</param>
      /// <param name="text">The localized text template.</param>
      public void AddTextTranslation(string language, string text)
      {
         if (string.IsNullOrEmpty(language)) return;
         _translatedTexts[language] = text ?? string.Empty;

         if (language.Equals(_language, StringComparison.OrdinalIgnoreCase))
         {
            Text = text;
         }
      }

      /// <summary>
      /// Add new variable to the massage
      /// </summary>
      /// <param name="name">The placeholder name of the variable (e.g., "fieldName").</param>
      /// <param name="value">The variable value</param>
      public void AddVariable(string name, string value) => AddVariable(_language, name, value);

      /// <summary>
      /// Add new variable to the massage
      /// </summary>
      /// <param name="language">The language culture code (e.g., "en-US", "pt-BR").</param>
      /// <param name="name">The placeholder name of the variable (e.g., "fieldName").</param>
      /// <param name="value">The variable value</param>
      public void AddVariable(string language, string name, string value)
      {
         if (string.IsNullOrEmpty(language) || string.IsNullOrEmpty(name)) return;

         var existingVariable = _variables.FirstOrDefault(v =>
            v.Language.Equals(language, StringComparison.OrdinalIgnoreCase) &&
            v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

         if (existingVariable != null)
         {
            existingVariable.Value = value ?? string.Empty;
         }
         else
         {
            var variable = new Variable(language, name, value ?? string.Empty);
            _variables.Add(variable);
         }
      }

      /// <summary>
      /// Resolves and returns the formatted message text.
      /// If any variable is used, the parse is done. There are two ways to use variables:
      /// 1. using {} => 'Inform a value for {variableName}.'
      /// 2. using [] => 'Inform a value for [variableName].'
      /// </summary>
      public string Show()
      {
         return Show(_language);
      }

      /// <summary>
      /// Resolves and returns the fully translated and formatted message text for the specified language.
      /// If any variable is used, the parse is done. There are two ways to use variables:
      /// 1. using {} => 'Inform a value for {variableName}.'
      /// 2. using [] => 'Inform a value for [variableName].'
      /// </summary>
      /// <param name="language">The target language culture code (e.g., "en-US", "pt-BR").</param>
      /// <returns>The formatted string message.</returns>
      public string Show(string language)
      {
         var resolved = ResolveTemplate(language);
         if (string.IsNullOrWhiteSpace(resolved.Template))
            return string.Empty;

         var builder = new StringBuilder(resolved.Template);

         foreach (var variableName in _variables
            .Select(variable => variable.Name)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.OrdinalIgnoreCase))
         {
            var variable = ResolveVariable(variableName, resolved.Language);
            if (variable == null) continue;

            string valueToReplace = variable.Value ?? string.Empty;
            builder.Replace("{" + variableName + "}", valueToReplace);
            builder.Replace("[" + variableName + "]", valueToReplace);
         }

         return builder.ToString();
      }

      /// <summary>
      /// Returns a string that represents the current object, including the code and text values.
      /// </summary>
      /// <returns>A string containing the code and text of the object in the format "Code: {Code}, Text: {Text}".</returns>
      public override string ToString()
      {
         return $"{nameof(Language)}: {Language}, {nameof(Code)}: {Code}, {nameof(Text)}: {Text}";
      }

      #region Private Methods

      /// <summary>
      /// Get the translation language for the variables. If there is no translation for the informed language, 
      /// get the translation for the Message language, if it is also doesn´t exists, uses default language (en-US).
      /// </summary>
      /// <param name="language">The target language culture code (e.g., "en-US", "pt-BR").</param>
      /// <returns></returns>
      private (string Template, string Language) ResolveTemplate(string language)
      {
         if (!string.IsNullOrWhiteSpace(language) &&
             TryGetTranslation(language, out var requestedTemplate, out var requestedLanguage))
         {
            return (requestedTemplate, requestedLanguage);
         }

         if (TryGetTranslation(_language, out var currentTemplate, out var currentLanguage))
         {
            return (currentTemplate, currentLanguage);
         }

         if (TryGetTranslation(DEFAULT_LANGUAGE, out var defaultTemplate, out var defaultLanguage))
         {
            return (defaultTemplate, defaultLanguage);
         }

         var firstTranslation = _translatedTexts.FirstOrDefault();
         if (!string.IsNullOrWhiteSpace(firstTranslation.Key))
         {
            return (firstTranslation.Value, firstTranslation.Key);
         }

         return (Text, _language);
      }

      private Variable? ResolveVariable(string name, string language)
      {
         var candidates = new[] { language, _language, DEFAULT_LANGUAGE }
            .Where(candidate => !string.IsNullOrWhiteSpace(candidate))
            .Distinct(StringComparer.OrdinalIgnoreCase);

         foreach (var candidate in candidates)
         {
            var variable = _variables.FirstOrDefault(item =>
               item.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
               item.Language.Equals(candidate, StringComparison.OrdinalIgnoreCase));

            if (variable != null) return variable;
         }

         return _variables.FirstOrDefault(item =>
            item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
      }

      private bool TryGetTranslation(
         string language,
         out string template,
         out string resolvedLanguage)
      {
         foreach (var translation in _translatedTexts)
         {
            if (translation.Key.Equals(language, StringComparison.OrdinalIgnoreCase))
            {
               template = translation.Value;
               resolvedLanguage = translation.Key;
               return true;
            }
         }

         template = string.Empty;
         resolvedLanguage = string.Empty;
         return false;
      }

      private IReadOnlyCollection<Variable> GetEffectiveVariables()
      {
         return _variables
                  .Select(variable => variable.Name)
                  .Where(name => !string.IsNullOrWhiteSpace(name))
                  .Distinct(StringComparer.OrdinalIgnoreCase)
                  .Select(name => ResolveVariable(name, _language))
                  .Where(variable => variable != null)
                  .Cast<Variable>()
                  .ToList()
                  .AsReadOnly();
      }
      #endregion
   }
}
