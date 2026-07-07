using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class StringErrorMessages
{
   public const string FewerCharactersThanExpectedError = nameof(FewerCharactersThanExpectedError);
   public const string InvalidDateError = nameof(InvalidDateError);
   public const string InvalidEmailError = nameof(InvalidEmailError);
   public const string InvalidFormatError = nameof(InvalidFormatError);
   public const string MoreCharactersThanExpectedError = nameof(MoreCharactersThanExpectedError);
   public const string NotExactNumberOfCharactersError = nameof(NotExactNumberOfCharactersError);
   public const string ShouldContainOnlyNumberError = nameof(ShouldContainOnlyNumberError);
   public const string MustContainSubstringError = nameof(MustContainSubstringError);
   public const string ShouldContainOnlyLettersError = nameof(ShouldContainOnlyLettersError);
   public const string ShouldContainOnlyLettersAndNumbersError = nameof(ShouldContainOnlyLettersAndNumbersError);

   public static ErrorMessage FewerCharactersThanExpected(
      string fieldName,
      int minLength)
      => WithValue(
         FewerCharactersThanExpectedError,
         fieldName,
         "minLength",
         minLength.ToString());

   public static ErrorMessage InvalidDate(string fieldName)
      => Create(InvalidDateError, fieldName);
   public static ErrorMessage InvalidEmail(string fieldName)
      => Create(InvalidEmailError, fieldName);
   public static ErrorMessage InvalidFormat(string fieldName)
      => Create(InvalidFormatError, fieldName);

   public static ErrorMessage MoreCharactersThanExpected(
      string fieldName,
      int maxLength)
      => WithValue(
         MoreCharactersThanExpectedError,
         fieldName,
         "maxLength",
         maxLength.ToString());

   public static ErrorMessage NotExactNumberOfCharacters(
      string fieldName,
      int length)
      => WithValue(
         NotExactNumberOfCharactersError,
         fieldName,
         "length",
         length.ToString());

   public static ErrorMessage ShouldContainOnlyNumber(string fieldName)
      => Create(ShouldContainOnlyNumberError, fieldName);

   public static ErrorMessage MustContainSubstring(
      string fieldName,
      string substring)
      => WithValue(
         MustContainSubstringError,
         fieldName,
         "substring",
         substring);

   public static ErrorMessage ShouldContainOnlyLetters(string fieldName)
      => Create(ShouldContainOnlyLettersError, fieldName);

   public static ErrorMessage ShouldContainOnlyLettersAndNumbers(
      string fieldName)
      => Create(ShouldContainOnlyLettersAndNumbersError, fieldName);

   private static ErrorMessage WithValue(
      string code,
      string fieldName,
      string variableName,
      string value)
      => ValidationError.Create(
         code,
         ("fieldName", fieldName),
         (variableName, value));

   private static ErrorMessage Create(string code, string fieldName)
      => ValidationError.Create(code, ("fieldName", fieldName));
}
