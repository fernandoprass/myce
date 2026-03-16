using Myce.Response.Messages;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Class representing the result of a validation rule execution, containing 
   /// the error message and a flag indicating whether an error was found.
   /// </summary>
   internal class FluentValidatorError
   {
      public ErrorMessage Message { get; set; } = null!;
      public bool ErrorFound { get; set; }
      public FluentValidatorError(ErrorMessage message, bool errorFound)
      {
         Message = message;
         ErrorFound = errorFound;
      }
   }
}
