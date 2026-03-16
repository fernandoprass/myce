using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
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

   public class MoreCharactersThanExpectedError : ErrorMessage
   {
      public MoreCharactersThanExpectedError(string fieldName, int maxLength)
         : base("MoreCharactersThanExpectedError", "'{fieldName}' has more characters than expected ({maxLength}).")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("maxLength", maxLength.ToString());
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

   public class MustContainSubstringError : ErrorMessage
   {
      public MustContainSubstringError(string fieldName, string substring)
         : base("MustContainSubstringError", $"'{fieldName}' must contain '{substring}'.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("substring", substring);
      }
   }

   public class ShouldContainOnlyLettersError : ErrorMessage
   {
      public ShouldContainOnlyLettersError(string fieldName)
         : base("ShouldContainOnlyLettersError", $"'{fieldName}' must contain only letters.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ShouldContainOnlyLettersAndNumbersError : ErrorMessage
   {
      public ShouldContainOnlyLettersAndNumbersError(string fieldName)
         : base("ShouldContainOnlyLettersAndNumbersError", $"'{fieldName}' must contain only letters and number.")
      {
         AddVariable("fieldName", fieldName);
      }
   }
}
