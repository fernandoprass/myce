namespace Myce.Response.Messages
{
   public class WarningMessage : Message
   {
      public WarningMessage() : base(MessageType.Warning) { }

      public WarningMessage(string text) : base(MessageType.Warning, string.Empty, text) { }

      public WarningMessage(string code, string text) : base(MessageType.Warning, code, text) { }

      public WarningMessage(string code, string text, CultureInfo culture) : base(MessageType.Warning, code, text, culture) { }

      public WarningMessage(string code, string text, Variable variable) : base(MessageType.Warning, code, text, variable) { }

      public WarningMessage(string code, string text, IEnumerable<Variable> variables) : base(MessageType.Warning, code, text, variables) { }

      public WarningMessage(string code, Dictionary<string, string> translations) : base(MessageType.Warning, code, translations) { }

      public WarningMessage(string code, CultureInfo culture, Dictionary<string, string> translations) : base(MessageType.Warning, code, culture, translations) { }

      internal WarningMessage(
         string code,
         Internationalization.MessageCatalog catalog,
         CultureInfo culture)
         : base(MessageType.Warning, code, catalog, culture) { }
   }
}
