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
      private readonly List<Variable> _variables = [];

      /// <summary>
      /// Holds the multilingual templates for this specific message instance: [Language] -> [Template Text]
      /// </summary>
      private readonly Dictionary<string, string> _translatedTexts = new(StringComparer.OrdinalIgnoreCase);

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
      public string Text { get; set; } = string.Empty;

      /// <summary>
      /// List of variables associated with the message. Each variable provides additional context or data for the message, and can be referenced in the Text property using placeholders.
      /// </summary>
      public IReadOnlyCollection<Variable> Variables => _variables.AsReadOnly();

      /// <summary>
      /// Translates the message to the specified language. If the new language doesn't exist, the default language will be used. 
      /// This method updates both the Language and Text properties of the message instance.
      /// </summary>
      /// <param name="language">The new language</param>
      public void TranslateTo(string language)
      {
         _language = language;
         Text = GetTextTranslated(language);
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

         _translatedTexts = translations;

         Type = type;
         Code = code ?? string.Empty;
         _language = language ?? DEFAULT_LANGUAGE;
         Text = GetTextTranslated(_language);
      }

      /// <summary>
      /// Returns a string that represents the current object, including the code and text values.
      /// </summary>
      /// <returns>A string containing the code and text of the object in the format "Code: {Code}, Text: {Text}".</returns>
      public override string ToString()
      {
         return $"{nameof(Language)}: {Language}, {nameof(Code)}: {Code}, {nameof(Text)}: {Text}";
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

         if (language == _language)
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
         // 1. Resolve Template with Fallback Strategy
         string template;
         if (_translatedTexts.TryGetValue(language, out var exactTemplate))
         {
            template = exactTemplate;
         }
         else if (_translatedTexts.TryGetValue(Language, out var fallbackTemplate))
         {
            template = fallbackTemplate;
            language = Language; // Update language to the fallback language for variable replacement
         }
         else
         {
            template = _translatedTexts.Values.FirstOrDefault() ?? (!string.IsNullOrWhiteSpace(Text) ? Text : $"[{Code}]");
            language = _translatedTexts.Keys.FirstOrDefault() ?? Language;
         }

         if (string.IsNullOrWhiteSpace(template))
            return string.Empty;

         var builder = new StringBuilder(template);

         // 2. Process Standard/Fixed variables first
         string variablesLanguage = GetVariablesLanguage(language);

         foreach (var variable in _variables.Where(v => v.Language == variablesLanguage))
         {
            string valueToReplace = variable.Value ?? string.Empty;
            builder.Replace("{" + variable.Name + "}", valueToReplace);
            builder.Replace("[" + variable.Name + "]", valueToReplace);
         }

         return builder.ToString();
      }

      /// <summary>
      /// Get the translation language for the variables. If there is no translation for the informed language, 
      /// get the translation for the Message language, if it is also doesn´t exists, uses default language (en-US).
      /// </summary>
      /// <param name="language"></param>
      /// <returns></returns>
      private string GetVariablesLanguage(string language)
      {
         return _variables.Any(v => v.Language == language) ? language :
                _variables.Any(v => v.Language == _language) ? _language : DEFAULT_LANGUAGE;
      }

      /// <summary>
      /// Get the translation for the Text. If language is not informed, get from default language (en-US). 
      /// If there is no translation for en-US, returns the current Text. 
      /// </summary>
      /// <param name="language">New language</param>
      /// <returns></returns>
      private string GetTextTranslated(string language)
      {
         return _translatedTexts.TryGetValue(language, out var txt) ? txt :
                _translatedTexts.TryGetValue(DEFAULT_LANGUAGE, out var defTxt) ? defTxt :
                Text;
      }
   }
}
