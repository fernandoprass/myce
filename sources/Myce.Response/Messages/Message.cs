using System.Globalization;
using Myce.Response.Messages.Internationalization;

namespace Myce.Response.Messages
{
   public abstract class Message
   {
      internal const string DefaultLanguage = "en-US";

      private readonly MessageCatalog _catalog;
      private CultureInfo _culture = CultureInfo.GetCultureInfo(DefaultLanguage);

      public string Language => _culture.Name;

      public MessageType Type { get; protected set; }

      public string Code { get; set; } = string.Empty;

      public string Text { get; private set; } = string.Empty;

      public IReadOnlyCollection<Variable> Variables
         => ResolveLocalization(_culture).Variables;

      public Message(MessageType type)
      {
         Type = type;
         _catalog = new MessageCatalog();
      }

      public Message(MessageType type, string text)
         : this(type, string.Empty, text, DefaultLanguage) { }

      public Message(MessageType type, string code, string text)
         : this(type, code, text, DefaultLanguage) { }

      public Message(
         MessageType type,
         string code,
         string text,
         string language)
         : this(type, code, text, CultureName.Get(language)) { }

      public Message(
         MessageType type,
         string code,
         string text,
         CultureInfo culture)
      {
         Type = type;
         Code = code ?? string.Empty;
         Text = text ?? string.Empty;
         _culture = CultureName.Get(culture);
         _catalog = new MessageCatalog();

         if (!string.IsNullOrWhiteSpace(Text))
            _catalog.AddTemplate(_culture, Text);
      }

      public Message(
         MessageType type,
         string code,
         string text,
         Variable variable)
         : this(type, code, text, DefaultLanguage)
      {
         if (variable == null)
            throw new ArgumentNullException(nameof(variable));
         _catalog.AddVariable(
            CultureName.Get(variable.Language),
            variable.Name,
            variable.Value);
      }

      public Message(
         MessageType type,
         string code,
         string text,
         IEnumerable<Variable> variables)
         : this(type, code, text, DefaultLanguage)
      {
         if (variables == null)
            throw new ArgumentNullException(nameof(variables));

         foreach (var variable in variables)
         {
            _catalog.AddVariable(
               CultureName.Get(variable.Language),
               variable.Name,
               variable.Value);
         }
      }

      public Message(
         MessageType type,
         string code,
         Dictionary<string, string> translations)
         : this(
            type,
            code,
            translations?.Keys.FirstOrDefault() ?? DefaultLanguage,
            translations!) { }

      public Message(
         MessageType type,
         string code,
         string language,
         Dictionary<string, string> translations)
         : this(type, code, CultureName.Get(language), translations) { }

      public Message(
         MessageType type,
         string code,
         CultureInfo culture,
         Dictionary<string, string> translations)
      {
         if (translations == null || translations.Count == 0)
            throw new ArgumentNullException(nameof(translations));

         Type = type;
         Code = code ?? string.Empty;
         _culture = CultureName.Get(culture);
         _catalog = new MessageCatalog();

         foreach (var translation in translations)
         {
            _catalog.AddTemplate(
               CultureName.Get(translation.Key),
               translation.Value);
         }

         ApplyLocalization(ResolveLocalization(_culture));
      }

      internal Message(
         MessageType type,
         string code,
         MessageCatalog catalog,
         CultureInfo culture)
      {
         Type = type;
         Code = code;
         _catalog = catalog;
         _culture = CultureName.Get(culture);

         ApplyLocalization(ResolveLocalization(_culture));
      }

      public Message AddTranslation(string language, string text)
         => AddTranslation(CultureName.Get(language), text);

      public Message AddTranslation(CultureInfo culture, string text)
      {
         culture = CultureName.Get(culture);
         _catalog.AddTemplate(culture, text ?? string.Empty);

         if (culture.Name.Equals(
            _culture.Name,
            StringComparison.OrdinalIgnoreCase))
         {
            ApplyLocalization(ResolveLocalization(_culture));
         }

         return this;
      }

      public Message AddVariable(string name, string value)
         => AddVariable(_culture, name, value);

      public Message AddVariable(
         string language,
         string name,
         string value)
         => AddVariable(CultureName.Get(language), name, value);

      public Message AddVariable(
         CultureInfo culture,
         string name,
         string value)
      {
         culture = CultureName.Get(culture);

         if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
               "Variable name cannot be null or empty.",
               nameof(name));

         _catalog.AddVariable(
            culture,
            name,
            value ?? string.Empty);

         return this;
      }

      public Message AddVariableTranslation(
         string name,
         string language,
         string value)
         => AddVariableTranslation(name, CultureName.Get(language), value);

      public Message AddVariableTranslation(
         string name,
         CultureInfo culture,
         string value)
      {
         return AddVariable(culture, name, value);
      }

      public Message WithLanguage(string language)
         => WithLanguage(CultureName.Get(language));

      public Message WithLanguage(CultureInfo culture)
      {
         culture = CultureName.Get(culture);
         var catalog = _catalog.Copy();
         var localization = MessageLocalizer.Localize(
            catalog,
            culture,
            _culture,
            Text);

         return CreateLocalizedCopy(catalog, localization.Culture);
      }

      public void TranslateTo(string language)
         => TranslateTo(CultureName.Get(language));

      public void TranslateTo(CultureInfo culture)
      {
         ApplyLocalization(ResolveLocalization(CultureName.Get(culture)));
      }

      public string Show()
         => MessageLocalizer.Format(ResolveLocalization(_culture));

      public string Show(string language)
         => Show(CultureName.Get(language));

      public string Show(CultureInfo culture)
      {
         return MessageLocalizer.Format(
            ResolveLocalization(CultureName.Get(culture)));
      }

      public override string ToString()
         => $"{nameof(Language)}: {Language}, {nameof(Code)}: {Code}, {nameof(Text)}: {Text}";

      private MessageLocalization ResolveLocalization(CultureInfo culture)
         => MessageLocalizer.Localize(
            _catalog,
            culture,
            _culture,
            Text);

      private void ApplyLocalization(MessageLocalization localization)
      {
         _culture = localization.Culture;
         Text = localization.Template;
      }

      private Message CreateLocalizedCopy(
         MessageCatalog catalog,
         CultureInfo culture)
      {
         switch (Type)
         {
            case MessageType.Error:
               return new ErrorMessage(Code, catalog, culture);
            case MessageType.Warning:
               return new WarningMessage(Code, catalog, culture);
            default:
               return new InformationMessage(Code, catalog, culture);
         }
      }
   }
}
