namespace Myce.Response.Messages
{
   public class InformationMessage : Message
   {
      public InformationMessage() : base(MessageType.Information) { }

      public InformationMessage(string text) : base(MessageType.Information, string.Empty, text) { }

      public InformationMessage(string code, string text) : base(MessageType.Information, code, text) { }

      public InformationMessage(string code, string text, IEnumerable<Variable> variables) : base(MessageType.Information, code, text, variables) { }
   }
}
