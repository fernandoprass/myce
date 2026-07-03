namespace Myce.Response.Messages
{
   public class ErrorMessage : Message
   {
      public ErrorMessage() : base(MessageType.Error) { }

      public ErrorMessage(string text) : base(MessageType.Error, text) { }
      
      public ErrorMessage(string code, string text) : base(MessageType.Error, code, text) { }
      
      public ErrorMessage(string code, string text, Variable variable) : base(MessageType.Error, code, text, variable) { }

      public ErrorMessage(string code, string text, IEnumerable<Variable> variables) : base(MessageType.Error, code, text, variables) { }

      public ErrorMessage(string code, Dictionary<string, string> translations) : base(MessageType.Error, code, translations) { }
   }
}
