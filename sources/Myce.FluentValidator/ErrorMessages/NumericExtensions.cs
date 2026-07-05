using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class NumericErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string Value = "value";
      private const string Min = "min";
      private const string Max = "max";

      public const string IsBetweenError = nameof(IsBetweenError);
      public const string IsGreaterThanError = nameof(IsGreaterThanError);
      public const string IsGreaterThanOrEqualToError = nameof(IsGreaterThanOrEqualToError);
      public const string IsLessThanError = nameof(IsLessThanError);
      public const string IsLessThanOrEqualToError = nameof(IsLessThanOrEqualToError);
      public const string IsPositiveError = nameof(IsPositiveError);
      public const string IsNegativeError = nameof(IsNegativeError   );

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         { IsBetweenError, new() {
            { "en-US", "'{fieldName}' must be between {min} and {max}." },
            { "pt-BR", "'{fieldName}' deve estar entre {min} e {max}." }
         }},
         { IsGreaterThanError, new() {
            { "en-US", "'{fieldName}' must be greater than {value}." },
            { "pt-BR", "'{fieldName}' deve ser maior que {value}." }
         }},
         { IsGreaterThanOrEqualToError, new() {
            { "en-US", "'{fieldName}' must be greater than or equal to {value}." },
            { "pt-BR", "'{fieldName}' deve ser maior ou igual a {value}." }
         }},
         { IsLessThanError, new() {
            { "en-US", "'{fieldName}' must be less than {value}." },
            { "pt-BR", "'{fieldName}' deve ser menor que {value}." }
         }},
         { IsLessThanOrEqualToError, new() {
            { "en-US", "'{fieldName}' must be less than or equal to {value}." },
            { "pt-BR", "'{fieldName}' deve ser menor ou igual a {value}." }
         }},
         { IsPositiveError, new() {
            { "en-US", "'{fieldName}' must be positive." },
            { "pt-BR", "'{fieldName}' deve ser positivo." }
         }},
         { IsNegativeError, new() {
            { "en-US", "'{fieldName}' must be negative." },
            { "pt-BR", "'{fieldName}' deve ser negativo." }
         }}
      };

      public static ErrorMessage IsBetween(string fieldName, string min, string max)
      {
         var error = Create(IsBetweenError, fieldName);
         error.AddVariable(Min, min);
         error.AddVariable(Max, max);
         return error;
      }

      public static ErrorMessage IsGreaterThan(string fieldName, string value)
         => CreateWithValue(IsGreaterThanError, fieldName, value);

      public static ErrorMessage IsGreaterThanOrEqualTo(string fieldName, string value)
         => CreateWithValue(IsGreaterThanOrEqualToError, fieldName, value);

      public static ErrorMessage IsLessThan(string fieldName, string value)
         => CreateWithValue(IsLessThanError, fieldName, value);

      public static ErrorMessage IsLessThanOrEqualTo(string fieldName, string value)
         => CreateWithValue(IsLessThanOrEqualToError, fieldName, value);

      public static ErrorMessage IsPositive(string fieldName)
         => Create(IsPositiveError, fieldName);

      public static ErrorMessage IsNegative(string fieldName)
         => Create(IsNegativeError, fieldName);

      private static ErrorMessage CreateWithValue(string code, string fieldName, string value)
      {
         var error = Create(code, fieldName);
         error.AddVariable(Value, value);
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
