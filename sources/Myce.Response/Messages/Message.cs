using System.Text;
using System.Collections.Generic;

namespace Myce.Response.Messages
{
   public abstract class Message
   {
      private static readonly string _defaultLanguage = "en-US";

      /// <summary>
      /// List of variables to be used in the message text. Each variable consists of a name and a value, and can be referenced
      /// </summary>
      private readonly List<Variable> _variables = [];

      /// <summary>
      /// Holds the multilingual templates for this specific message instance: [Language] -> [Template Text]
      /// </summary>
      private readonly Dictionary<string, string> _translatedTexts = new(StringComparer.OrdinalIgnoreCase);

      /// <summary>
      /// Holds localizable values for specific variables: [VariableName] -> [Language] -> [Localized Value]
      /// </summary>
      private readonly Dictionary<string, Dictionary<string, string>> _translatedVariables = new(StringComparer.OrdinalIgnoreCase);

      /// <summary>
      /// Default language key used as fallback if requested translation is missing. If not informed, uses en-US.
      /// </summary>
      public string Language { get; set; } = _defaultLanguage;

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
      public string Text { get; set; } = string.Empty;

      /// <summary>
      /// List of variables associated with the message. Each variable provides additional context or data for the message, and can be referenced in the Text property using placeholders.
      /// </summary>
      public IReadOnlyCollection<Variable> Variables => _variables.AsReadOnly();

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message..</param>
      public Message(MessageType type)
      {
         Type = type;
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text)
      {
         Type = type;
         Code = code ?? string.Empty;
         Text = text ?? string.Empty;
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      public Message(MessageType type, string text) : this(type, string.Empty, text) { }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, text, and a collection
      /// of variables.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      /// <param name="variables">Variables to associate with the message. Each variable provides 
      /// additional context or data for the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text, Variable variable) : this(type, code, text)
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
      public Message(MessageType type, string code, string text, IEnumerable<Variable> variables) : this(type, code, text)
      {
         _variables.AddRange(variables);
      }

      /// <summary>
      /// Initializes a new instance of the Message class supporting multiple languages.
      /// </summary>
      /// <param name="type">The type of the message.</param>
      /// <param name="code">The unique message code identifier.</param>
      /// <param name="translations">The dictionary containing language keys and message templates (e.g., Key: "en-US", Value: "Inform Date of birth").</param>
      public Message(MessageType type, string code, Dictionary<string, string> translations)
      {
         Type = type;
         Code = code ?? string.Empty;
         if (translations != null)
         {
            foreach (var kvp in translations)
            {
               _translatedTexts[kvp.Key] = kvp.Value;
            }
         }
         Text = GetTextTranslated();
      }

      /// <summary>
      /// Returns a string that represents the current object, including the code and text values.
      /// </summary>
      /// <returns>A string containing the code and text of the object in the format "Code: {Code}, Text: {Text}".</returns>
      public override string ToString()
      {
         return $"{nameof(Code)}: {Code}, {nameof(Text)}: {Text}";
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
      }

      /// <summary>
      /// Add new variable to the massage
      /// </summary>
      /// <param name="name">The variable name</param>
      /// <param name="value">The variable value</param>
      public void AddVariable(string name, string value)
      {
         var variable = new Variable { Name = name, Value = value };
         _variables.Add(variable);
      }

      /// <summary>
      /// Adds or updates a single localized translation value for a specific variable.
      /// </summary>
      /// <param name="variable">The placeholder name of the variable (e.g., "fieldName").</param>
      /// <param name="language">The language culture code (e.g., "en-US", "pt-BR").</param>
      /// <param name="value">The localized value for the variable.</param>
      public void AddVariableTranslation(string variable, string language, string value)
      {
         if (string.IsNullOrEmpty(variable) || string.IsNullOrEmpty(language)) return;

         if (!_translatedVariables.TryGetValue(variable, out var translations))
         {
            translations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _translatedVariables[variable] = translations;
         }

         translations[language] = value ?? string.Empty;

         // If this is the first translation being added for this variable, 
         // sync it with the legacy collection to preserve backward compatibility.
         if (!_variables.Any(v => string.Equals(v.Name, variable, StringComparison.OrdinalIgnoreCase)))
         {
            AddVariable(variable, value ?? string.Empty);
         }
      }

      /// <summary>
      /// Adds a variable whose value changes depending on the requested language (e.g., localized field names).
      /// </summary>
      /// <param name="name">The placeholder name used in the template (e.g., "fieldName").</param>
      /// <param name="translations">A dictionary mapping language culture codes to their respective localized variable values (e.g., Key: "en-US", Value: "Birth Date").</param>
      public void AddVariableTranslation(string name, Dictionary<string, string> translations)
      {
         if (string.IsNullOrEmpty(name) || translations == null) return;

         _translatedVariables[name] = translations;

         // Populates legacy collection with the first available translation to protect backward compatibility
         var firstValue = translations.Values.FirstOrDefault();
         if (firstValue != null)
         {
            AddVariable(name, firstValue);
         }
      }

      /// <summary>
      /// Show the Text value. If any variable is used, the parse is done
      /// There are two ways to use variables
      /// 2. using {}
      /// 3. using []
      /// </summary>
      public string Show()
      {
         if (_translatedTexts.Any())
         {
            string targetLanguage = _translatedTexts.ContainsKey(Language)
               ? Language
               : _translatedTexts.Keys.FirstOrDefault() ?? string.Empty;

            return Show(targetLanguage);
         }

         if (string.IsNullOrWhiteSpace(Text))
            return string.Empty;

         if (_variables == null || !_variables.Any())
            return Text;

         var builder = new StringBuilder(Text);

         foreach (var variable in _variables)
         {
            if (string.IsNullOrEmpty(variable.Name)) continue;

            string valueToReplace = variable.Value ?? string.Empty;

            builder.Replace("{" + variable.Name + "}", valueToReplace);
            builder.Replace("[" + variable.Name + "]", valueToReplace);
         }

         return builder.ToString();
      }

      /// <summary>
      /// Resolves and returns the fully translated and formatted message text for the specified language.
      /// </summary>
      /// <param name="language">The target language culture code (e.g., "en-US", "pt-BR").</param>
      /// <returns>The formatted string message.</returns>
      public string Show(string language)
      {
         // 1. Resolve Template with Fallback Strategy
         string template;
         if (_translatedTexts.TryGetValue(language, out var exactTemplate))
         {
            template = exactTemplate;
         }
         else if (_translatedTexts.TryGetValue(Language, out var fallbackTemplate))
         {
            template = fallbackTemplate;
         }
         else
         {
            template = _translatedTexts.Values.FirstOrDefault() ?? (!string.IsNullOrWhiteSpace(Text) ? Text : $"[{Code}]");
         }

         if (string.IsNullOrWhiteSpace(template))
            return string.Empty;

         var builder = new StringBuilder(template);

         // 2. Process Standard/Fixed variables first
         foreach (var variable in _variables)
         {
            if (string.IsNullOrEmpty(variable.Name)) continue;

            // If this variable has a localized version, skip it here to let the localized processor handle it
            if (_translatedVariables.ContainsKey(variable.Name)) continue;

            string valueToReplace = variable.Value ?? string.Empty;
            builder.Replace("{" + variable.Name + "}", valueToReplace);
            builder.Replace("[" + variable.Name + "]", valueToReplace);
         }

         // 3. Process Multilingual variables with language resolution fallbacks
         foreach (var kvp in _translatedVariables)
         {
            string varName = kvp.Key;
            if (string.IsNullOrEmpty(varName)) continue;

            var varTranslations = kvp.Value;

            if (!varTranslations.TryGetValue(language, value: out string localizedValue))
            {
               if (!varTranslations.TryGetValue(Language, out localizedValue))
               {
                  localizedValue = varTranslations.Values.FirstOrDefault() ?? string.Empty;
               }
            }

            builder.Replace("{" + varName + "}", localizedValue);
            builder.Replace("[" + varName + "]", localizedValue);
         }

         return builder.ToString();
      }

      /// <summary>
      /// Get the translation for the Text. If language is not informed, get from default language (en-US). If there is no translation for en-US, returns empty string. 
      /// </summary>
      /// <returns></returns>
      private string GetTextTranslated()
      {
         return _translatedTexts.TryGetValue(Language, out var txt) ? txt :
                _translatedTexts.TryGetValue(_defaultLanguage, out var defTxt) ? defTxt :
                string.Empty;
      }
   }
}
