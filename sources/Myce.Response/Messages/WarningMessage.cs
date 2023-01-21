namespace Myce.Response.Messages
{
   public class WarningMessage : Message
   {
      public WarningMessage() : base(MessageType.Warning) { }

      public WarningMessage(string text) : base(MessageType.Warning, string.Empty, text) { }

      public WarningMessage(string code, string text) : base(MessageType.Warning, code, text) { }

      public WarningMessage(string code, string text, IEnumerable<Variable> variables) : base(MessageType.Warning, code, text, variables) { }
   }
}
