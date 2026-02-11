using Myce.Response.Messages;

namespace Myce.FluentValidator
{
   internal class FluentValidatorError
   {
      public ErrorMessage Message { get; set; }
      public bool ErrorFound { get; set; }
      public FluentValidatorError(ErrorMessage message, bool errorFound)
      {
         Message = message;
         ErrorFound = errorFound;
      }
   }
}
