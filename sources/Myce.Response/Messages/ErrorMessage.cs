namespace Myce.Response.Messages
{
   public class ErrorMessage : Message
   {
      public ErrorMessage() : base(MessageType.Error) { }

      public ErrorMessage(string text) : base(MessageType.Error, string.Empty, text) { }

      public ErrorMessage(string code, string text) : base(MessageType.Error, code, text) { }

      public ErrorMessage(string code, string text, IEnumerable<Variable> variables) : base(MessageType.Error, code, text, variables) { }
   }
}
