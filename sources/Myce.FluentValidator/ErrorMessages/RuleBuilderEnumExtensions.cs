using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class InvalidEnumValueError : ErrorMessage
   {
      public InvalidEnumValueError(string fieldName, string typeName)
         : base("InvalidEnumValueError", $"'{fieldName}' has an invalid value for {typeName}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("typeName", typeName);
      }
   }

   public class MustNotBeDefaultValueError : ErrorMessage
   {
      public MustNotBeDefaultValueError(string fieldName)
         : base("MustNotBeDefaultValueError", $"'{fieldName}' must not be the default value.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class NotInEnumError : ErrorMessage
   {
      public NotInEnumError(string fieldName, string typeName)
         : base("NotInEnumError", $"'{fieldName}' cannot be a defined value of {typeName}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("typeName", typeName);
      }
   }
}
