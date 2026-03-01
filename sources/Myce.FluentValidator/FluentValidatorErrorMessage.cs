using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class ErrorShouldContainOnlyNumber : ErrorMessage
   {
      public ErrorShouldContainOnlyNumber(string fieldName) 
         : base("ShouldContainOnlyNumbersError", "'{fieldName}' should contain only number.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ErrorNotExactNumberOfCharacters : ErrorMessage
   {
      public ErrorNotExactNumberOfCharacters(string fieldName, int length) 
         : base("NotExactNumberOfCharactersError", "The field '{fieldName}' does not have the expected number of characters ({length})")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("length", length.ToString());
      }
   }

   public class ErrorIsRequired : ErrorMessage
   {
      public ErrorIsRequired(string fieldName)
         : base("NotFilledError", "The field '{fieldName}' is required")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ErrorInvalidDate : ErrorMessage
   {
      public ErrorInvalidDate(string fieldName)
         : base("InvalidEmailError", "The field '{fieldName}' is not a valid date")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ErrorInvalidEmail : ErrorMessage
   {
      public ErrorInvalidEmail(string fieldName)
         : base("InvalidEmailError", "The field '{fieldName}' is not a valid email")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ErrorMoreCharactersThanExpected : ErrorMessage
   {
      public ErrorMoreCharactersThanExpected(string fieldName, int maxLength)
         : base("MoreCharactersThanExpectedError", "The field '{fieldName}' has more characters than expected ({max.length})")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("max.length", maxLength.ToString());
      }
   }

   public class ErrorFewerCharactersThanExpected : ErrorMessage
   {
      public ErrorFewerCharactersThanExpected(string fieldName, int minLength)
         : base("FewerCharactersThanExpectedError", "The field '{fieldName}' has fewer characters than expected ({min.length})")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("min.length", minLength.ToString());
      }
   }
}
