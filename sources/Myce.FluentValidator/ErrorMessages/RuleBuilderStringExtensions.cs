using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class StringErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string MinLength = "minLength";
      private const string MaxLength = "maxLength";
      private const string Length = "length";
      private const string Substring = "substring";

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

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         { FewerCharactersThanExpectedError, new() {
            { "en-US", "'{fieldName}' has fewer characters than expected ({minLength})." },
            { "pt-BR", "'{fieldName}' possui menos caracteres que o esperado ({minLength})." }
         }},
         { InvalidDateError, new() {
            { "en-US", "'{fieldName}' is not a valid date." },
            { "pt-BR", "'{fieldName}' não é uma data válida." }
         }},
         { InvalidEmailError, new() {
            { "en-US", "'{fieldName}' is not a valid email." },
            { "pt-BR", "'{fieldName}' não é um e-mail válido." }
         }},
         { InvalidFormatError, new() {
            { "en-US", "'{fieldName}' does not match the expected format." },
            { "pt-BR", "'{fieldName}' não corresponde ao formato esperado." }
         }},
         { MoreCharactersThanExpectedError, new() {
            { "en-US", "'{fieldName}' has more characters than expected ({maxLength})." },
            { "pt-BR", "'{fieldName}' possui mais caracteres que o esperado ({maxLength})." }
         }},
         { NotExactNumberOfCharactersError, new() {
            { "en-US", "'{fieldName}' does not have the expected number of characters ({length})." },
            { "pt-BR", "'{fieldName}' não possui o número esperado de caracteres ({length})." }
         }},
         { ShouldContainOnlyNumberError, new() {
            { "en-US", "'{fieldName}' should contain only numbers." },
            { "pt-BR", "'{fieldName}' deve conter apenas números." }
         }},
         { MustContainSubstringError, new() {
            { "en-US", "'{fieldName}' must contain '{substring}'." },
            { "pt-BR", "'{fieldName}' deve conter '{substring}'." }
         }},
         { ShouldContainOnlyLettersError, new() {
            { "en-US", "'{fieldName}' must contain only letters." },
            { "pt-BR", "'{fieldName}' deve conter apenas letras." }
         }},
         { ShouldContainOnlyLettersAndNumbersError, new() {
            { "en-US", "'{fieldName}' must contain only letters and numbers." },
            { "pt-BR", "'{fieldName}' deve conter apenas letras e números." }
         }}
      };

      public static ErrorMessage FewerCharactersThanExpected(string fieldName, int minLength)
         => CreateWithVariable(FewerCharactersThanExpectedError, fieldName, MinLength, minLength.ToString());

      public static ErrorMessage InvalidDate(string fieldName)
         => Create(InvalidDateError, fieldName);

      public static ErrorMessage InvalidEmail(string fieldName)
         => Create(InvalidEmailError, fieldName);

      public static ErrorMessage InvalidFormat(string fieldName)
         => Create(InvalidFormatError, fieldName);

      public static ErrorMessage MoreCharactersThanExpected(string fieldName, int maxLength)
         => CreateWithVariable(MoreCharactersThanExpectedError, fieldName, MaxLength, maxLength.ToString());

      public static ErrorMessage NotExactNumberOfCharacters(string fieldName, int length)
         => CreateWithVariable(NotExactNumberOfCharactersError, fieldName, Length, length.ToString());

      public static ErrorMessage ShouldContainOnlyNumber(string fieldName)
         => Create(ShouldContainOnlyNumberError, fieldName);

      public static ErrorMessage MustContainSubstring(string fieldName, string substring)
         => CreateWithVariable(MustContainSubstringError, fieldName, Substring, substring);

      public static ErrorMessage ShouldContainOnlyLetters(string fieldName)
         => Create(ShouldContainOnlyLettersError, fieldName);

      public static ErrorMessage ShouldContainOnlyLettersAndNumbers(string fieldName)
         => Create(ShouldContainOnlyLettersAndNumbersError, fieldName);

      private static ErrorMessage CreateWithVariable(string code, string fieldName, string variableName, string value)
      {
         var error = Create(code, fieldName);
         error.AddVariable(variableName, value);
         return error;
      }

      private static ErrorMessage Create(string code, string fieldName)
      {
         var error = new ErrorMessage(code, _localTranslations[code]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}
