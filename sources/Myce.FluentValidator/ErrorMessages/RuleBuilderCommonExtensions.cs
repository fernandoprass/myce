using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class IsFalseError : ErrorMessage
   {
      public IsFalseError(string fieldName)
         : base("IsFalseError", "'{fieldName}' is false.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsTrueError : ErrorMessage
   {
      public IsTrueError(string fieldName)
         : base("IsTrueError", "'{fieldName}' is true.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsNullError : ErrorMessage
   {
      public IsNullError(string fieldName)
         : base("IsNullError", "'{fieldName}' is null.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsNotNullError : ErrorMessage
   {
      public IsNotNullError(string fieldName)
         : base("IsNotNullError", "'{fieldName}' is not null.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsRequiredError : ErrorMessage
   {
      public IsRequiredError(string fieldName)
         : base("IsRequiredError", "'{fieldName}' is required.")
      {
         AddVariable("fieldName", fieldName);
      }
   }
}
