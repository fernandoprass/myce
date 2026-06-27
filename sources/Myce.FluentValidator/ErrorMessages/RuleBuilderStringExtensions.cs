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

      public const string FewerCharactersThanExpectedErrorCode = nameof(FewerCharactersThanExpectedErrorCode);
      public const string InvalidDateErrorCode = nameof(InvalidDateErrorCode);
      public const string InvalidEmailErrorCode = nameof(InvalidEmailErrorCode);
      public const string InvalidFormatErrorCode = nameof(InvalidFormatErrorCode);
      public const string MoreCharactersThanExpectedErrorCode = nameof(MoreCharactersThanExpectedErrorCode);
      public const string NotExactNumberOfCharactersErrorCode = nameof(NotExactNumberOfCharactersErrorCode);
      public const string ShouldContainOnlyNumberErrorCode = nameof(ShouldContainOnlyNumberErrorCode);
      public const string MustContainSubstringErrorCode = nameof(MustContainSubstringErrorCode);
      public const string ShouldContainOnlyLettersErrorCode = nameof(ShouldContainOnlyLettersErrorCode);
      public const string ShouldContainOnlyLettersAndNumbersErrorCode = nameof(ShouldContainOnlyLettersAndNumbersErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         { FewerCharactersThanExpectedErrorCode, new() {
            { "en-US", "'{fieldName}' has fewer characters than expected ({minLength})." },
            { "pt-BR", "'{fieldName}' possui menos caracteres que o esperado ({minLength})." }
         }},
         { InvalidDateErrorCode, new() {
            { "en-US", "'{fieldName}' is not a valid date." },
            { "pt-BR", "'{fieldName}' não é uma data válida." }
         }},
         { InvalidEmailErrorCode, new() {
            { "en-US", "'{fieldName}' is not a valid email." },
            { "pt-BR", "'{fieldName}' não é um e-mail válido." }
         }},
         { InvalidFormatErrorCode, new() {
            { "en-US", "'{fieldName}' does not match the expected format." },
            { "pt-BR", "'{fieldName}' não corresponde ao formato esperado." }
         }},
         { MoreCharactersThanExpectedErrorCode, new() {
            { "en-US", "'{fieldName}' has more characters than expected ({maxLength})." },
            { "pt-BR", "'{fieldName}' possui mais caracteres que o esperado ({maxLength})." }
         }},
         { NotExactNumberOfCharactersErrorCode, new() {
            { "en-US", "'{fieldName}' does not have the expected number of characters ({length})." },
            { "pt-BR", "'{fieldName}' não possui o número esperado de caracteres ({length})." }
         }},
         { ShouldContainOnlyNumberErrorCode, new() {
            { "en-US", "'{fieldName}' should contain only numbers." },
            { "pt-BR", "'{fieldName}' deve conter apenas números." }
         }},
         { MustContainSubstringErrorCode, new() {
            { "en-US", "'{fieldName}' must contain '{substring}'." },
            { "pt-BR", "'{fieldName}' deve conter '{substring}'." }
         }},
         { ShouldContainOnlyLettersErrorCode, new() {
            { "en-US", "'{fieldName}' must contain only letters." },
            { "pt-BR", "'{fieldName}' deve conter apenas letras." }
         }},
         { ShouldContainOnlyLettersAndNumbersErrorCode, new() {
            { "en-US", "'{fieldName}' must contain only letters and numbers." },
            { "pt-BR", "'{fieldName}' deve conter apenas letras e números." }
         }}
      };

      public static ErrorMessage FewerCharactersThanExpected(string fieldName, int minLength)
         => CreateWithVariable(FewerCharactersThanExpectedErrorCode, fieldName, MinLength, minLength.ToString());

      public static ErrorMessage InvalidDate(string fieldName)
         => Create(InvalidDateErrorCode, fieldName);

      public static ErrorMessage InvalidEmail(string fieldName)
         => Create(InvalidEmailErrorCode, fieldName);

      public static ErrorMessage InvalidFormat(string fieldName)
         => Create(InvalidFormatErrorCode, fieldName);

      public static ErrorMessage MoreCharactersThanExpected(string fieldName, int maxLength)
         => CreateWithVariable(MoreCharactersThanExpectedErrorCode, fieldName, MaxLength, maxLength.ToString());

      public static ErrorMessage NotExactNumberOfCharacters(string fieldName, int length)
         => CreateWithVariable(NotExactNumberOfCharactersErrorCode, fieldName, Length, length.ToString());

      public static ErrorMessage ShouldContainOnlyNumber(string fieldName)
         => Create(ShouldContainOnlyNumberErrorCode, fieldName);

      public static ErrorMessage MustContainSubstring(string fieldName, string substring)
         => CreateWithVariable(MustContainSubstringErrorCode, fieldName, Substring, substring);

      public static ErrorMessage ShouldContainOnlyLetters(string fieldName)
         => Create(ShouldContainOnlyLettersErrorCode, fieldName);

      public static ErrorMessage ShouldContainOnlyLettersAndNumbers(string fieldName)
         => Create(ShouldContainOnlyLettersAndNumbersErrorCode, fieldName);

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
