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
      public FluentValidatorMessage(Message message, bool wasRuleBroken)
      {
         Message = message;
         WasRuleBroken = wasRuleBroken;
      }
   }
}
