using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

internal static class ValidationError
{
   internal static ErrorMessage Create(
      string code,
      params (string Name, string Value)[] arguments)
   {
      var message = new BuiltInValidationMessage(code);

      foreach (var argument in arguments)
         message.AddVariable(argument.Name, argument.Value);

      return message;
   }

   internal sealed class BuiltInValidationMessage : ErrorMessage
   {
      internal BuiltInValidationMessage(string code)
         : base(code, code) { }
   }
}
