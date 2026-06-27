using Myce.Response.Messages;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Class representing the result of a validation rule execution, containing 
   /// the message and a flag indicating whether an error was found.
   /// </summary>
   internal class FluentValidatorMessage
   {
      public Message Message { get; set; } = null!;
      public bool WasRuleBroken { get; set; }
      public FluentValidatorMessage(Message message, MessageLanguage language, bool wasRuleBroken)
      {
         message.Language = language.ToString();
         Message = message;
         WasRuleBroken = wasRuleBroken;
      }
   }
}
