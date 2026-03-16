using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class ContainsInvalidValueError : ErrorMessage
   {
      public ContainsInvalidValueError(string fieldName)
         : base("ContainsInvalidValueError", "'{fieldName}' contains an invalid value.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsEmptyError : ErrorMessage
   {
      public IsEmptyError(string fieldName)
         : base("IsEmptyError", "'{fieldName}' is not empty.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsNotEmptyError : ErrorMessage
   {
      public IsNotEmptyError(string fieldName)
         : base("IsNotEmptyError", "'{fieldName}' is empty.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class MaxNumberOfItemsError : ErrorMessage
   {
      public MaxNumberOfItemsError(string fieldName, int max)
         : base("MaxNumberOfItemsError", $"'{fieldName}' must have at most {max} items.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("max", max.ToString());
      }
   }
}
