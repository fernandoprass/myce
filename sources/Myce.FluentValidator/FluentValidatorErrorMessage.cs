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

   public class FewerCharactersThanExpectedError : ErrorMessage
   {
      public FewerCharactersThanExpectedError(string fieldName, int minLength)
         : base("FewerCharactersThanExpectedError", "'{fieldName}' has fewer characters than expected ({min.length}).")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("min.length", minLength.ToString());
      }
   }

   public class InvalidDateError : ErrorMessage
   {
      public InvalidDateError(string fieldName)
         : base("InvalidEmailError", "'{fieldName}' is not a valid date.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class InvalidEmailError : ErrorMessage
   {
      public InvalidEmailError(string fieldName)
         : base("InvalidEmailError", "'{fieldName}' is not a valid email.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

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

   public class MoreCharactersThanExpectedError : ErrorMessage
   {
      public MoreCharactersThanExpectedError(string fieldName, int maxLength)
         : base("MoreCharactersThanExpectedError", "'{fieldName}' has more characters than expected ({maxLength}).")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("maxLength", maxLength.ToString());
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


   public class NotExactNumberOfCharactersError : ErrorMessage
   {
      public NotExactNumberOfCharactersError(string fieldName, int length)
         : base("NotExactNumberOfCharactersError", "'{fieldName}' does not have the expected number of characters ({length}).")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("length", length.ToString());
      }
   }

   public class ShouldContainOnlyNumberError : ErrorMessage
   {
      public ShouldContainOnlyNumberError(string fieldName)
         : base("ShouldContainOnlyNumbersError", "'{fieldName}' should contain only number.")
      {
         AddVariable("fieldName", fieldName);
      }
   }
}
