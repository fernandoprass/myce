using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class IsBetweenError : ErrorMessage
   {
      public IsBetweenError(string fieldName, string min, string max)
         : base("IsBetweenError", $"'{fieldName}' must be between {min} and {max}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("min", min);
         AddVariable("max", max);
      }
   }

   public class IsGreaterThanError : ErrorMessage
   {
      public IsGreaterThanError(string fieldName, string value)
         : base("IsGreaterThanError", $"'{fieldName}' must be greater than {value}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("value", value);
      }
   }

   public class IsGreaterThanOrEqualToError : ErrorMessage
   {
      public IsGreaterThanOrEqualToError(string fieldName, string value)
         : base("IsGreaterThanOrEqualToError", $"'{fieldName}' must be greater than or equal to {value}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("value", value);
      }
   }

   public class IsLessThanError : ErrorMessage
   {
      public IsLessThanError(string fieldName, string value)
         : base("IsLessThanError", $"'{fieldName}' must be less than {value}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("value", value);
      }
   }

   public class IsLessThanOrEqualToError : ErrorMessage
   {
      public IsLessThanOrEqualToError(string fieldName, string value)
         : base("IsLessThanOrEqualToError", $"'{fieldName}' must be less than or equal to {value}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("value", value);
      }
   }

   public class IsPositiveError : ErrorMessage
   {
      public IsPositiveError(string fieldName)
         : base("IsPositiveError", $"'{fieldName}' must be positive.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsNegativeError : ErrorMessage
   {
      public IsNegativeError(string fieldName)
         : base("IsNegativeError", $"'{fieldName}' must be negative.")
      {
         AddVariable("fieldName", fieldName);
      }
   }
}
