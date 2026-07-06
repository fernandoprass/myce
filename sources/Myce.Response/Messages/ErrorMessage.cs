namespace Myce.Response.Messages
{
   public class ErrorMessage : Message
   {
      public ErrorMessage() : base(MessageType.Error) { }

      public ErrorMessage(string text) : base(MessageType.Error, text) { }
      
      public ErrorMessage(string code, string text) : base(MessageType.Error, code, text) { }

      public ErrorMessage(string code, string text, CultureInfo culture) : base(MessageType.Error, code, text, culture) { }
      
      public ErrorMessage(string code, string text, Variable variable) : base(MessageType.Error, code, text, variable) { }

      public ErrorMessage(string code, string text, IEnumerable<Variable> variables) : base(MessageType.Error, code, text, variables) { }

      public ErrorMessage(string code, Dictionary<string, string> translations) : base(MessageType.Error, code, translations) { }

      public ErrorMessage(string code, CultureInfo culture, Dictionary<string, string> translations) : base(MessageType.Error, code, culture, translations) { }

      internal ErrorMessage(
         string code,
         Internationalization.MessageCatalog catalog,
         CultureInfo culture)
         : base(MessageType.Error, code, catalog, culture) { }
   }
}
