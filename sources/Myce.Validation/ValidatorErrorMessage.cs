using Myce.Response.Messages;

namespace Myce.Validation.ErrorMessages
{
   public class ErrorShouldContainOnlyNumber : ErrorMessage
   {
      public ErrorShouldContainOnlyNumber(string fieldName) 
         : base("ERROR_SHOULD_CONTAIN_ONLY_NUMBERS", "'{fieldName}' should contain only number.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ErrorNotExactNumberOfCharacteres : ErrorMessage
   {
      public ErrorNotExactNumberOfCharacteres(string value, int lenght) 
         : base("ERROR_NOT_EXACT_NUMBER_OF_CHARACTERES", "The string '{value}' does not have the expected number of characters ({lenght})")
      {
         AddVariable("value", value);
         AddVariable("lenght", lenght.ToString());
      }
   }

   public class ErrorIsRequired : ErrorMessage
   {
      public ErrorIsRequired(string fieldName)
         : base("ERROR_NOT_FILLED", "T'{fieldName}' is required")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class ErrorInvalidDate : ErrorMessage
   {
      public ErrorInvalidDate(string date)
         : base("ERROR_INVALID_DATE", "The date '{date}' is not valid")
      {
         AddVariable("date", date);
      }
   }

   public class ErrorInvalidEmail : ErrorMessage
   {
      public ErrorInvalidEmail(string email)
         : base("ERROR_INVALID_EMAIL", "The email '{email}' is not valid")
      {
         AddVariable("email", email);
      }
   }

   public class ErrorMoreCharactersThanExpected : ErrorMessage
   {
      public ErrorMoreCharactersThanExpected(string value, int maxLenght)
         : base("ERROR_MORE_CHARACTERES_THAN_EXPECTED", "The string '{value}' has more characters than expected ({max.lenght})")
      {
         AddVariable("value", value);
         AddVariable("max.lenght", maxLenght.ToString());
      }
   }

   public class ErrorFewerCharactersThanExpected : ErrorMessage
   {
      public ErrorFewerCharactersThanExpected(string value, int minLenght)
         : base("ERROR_FEWER_CHARACTERES_THAN_EXPECTED", "The string '{value}' has fewer characters than expected ({min.lenght})")
      {
         AddVariable("value", value);
         AddVariable("min.lenght", minLenght.ToString());
      }
   }
}
