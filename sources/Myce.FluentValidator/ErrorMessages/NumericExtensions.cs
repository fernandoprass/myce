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

      public const string IsBetweenErrorCode = nameof(IsBetweenErrorCode);
      public const string IsGreaterThanErrorCode = nameof(IsGreaterThanErrorCode);
      public const string IsGreaterThanOrEqualToErrorCode = nameof(IsGreaterThanOrEqualToErrorCode);
      public const string IsLessThanErrorCode = nameof(IsLessThanErrorCode);
      public const string IsLessThanOrEqualToErrorCode = nameof(IsLessThanOrEqualToErrorCode);
      public const string IsPositiveErrorCode = nameof(IsPositiveErrorCode);
      public const string IsNegativeErrorCode = nameof(IsNegativeErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         { IsBetweenErrorCode, new() {
            { "en-US", "'{fieldName}' must be between {min} and {max}." },
            { "pt-BR", "'{fieldName}' deve estar entre {min} e {max}." }
         }},
         { IsGreaterThanErrorCode, new() {
            { "en-US", "'{fieldName}' must be greater than {value}." },
            { "pt-BR", "'{fieldName}' deve ser maior que {value}." }
         }},
         { IsGreaterThanOrEqualToErrorCode, new() {
            { "en-US", "'{fieldName}' must be greater than or equal to {value}." },
            { "pt-BR", "'{fieldName}' deve ser maior ou igual a {value}." }
         }},
         { IsLessThanErrorCode, new() {
            { "en-US", "'{fieldName}' must be less than {value}." },
            { "pt-BR", "'{fieldName}' deve ser menor que {value}." }
         }},
         { IsLessThanOrEqualToErrorCode, new() {
            { "en-US", "'{fieldName}' must be less than or equal to {value}." },
            { "pt-BR", "'{fieldName}' deve ser menor ou igual a {value}." }
         }},
         { IsPositiveErrorCode, new() {
            { "en-US", "'{fieldName}' must be positive." },
            { "pt-BR", "'{fieldName}' deve ser positivo." }
         }},
         { IsNegativeErrorCode, new() {
            { "en-US", "'{fieldName}' must be negative." },
            { "pt-BR", "'{fieldName}' deve ser negativo." }
         }}
      };

      public static ErrorMessage IsBetween(string fieldName, string min, string max)
      {
         var error = Create(IsBetweenErrorCode, fieldName);
         error.AddVariable(Min, min);
         error.AddVariable(Max, max);
         return error;
      }

      public static ErrorMessage IsGreaterThan(string fieldName, string value)
         => CreateWithValue(IsGreaterThanErrorCode, fieldName, value);

      public static ErrorMessage IsGreaterThanOrEqualTo(string fieldName, string value)
         => CreateWithValue(IsGreaterThanOrEqualToErrorCode, fieldName, value);

      public static ErrorMessage IsLessThan(string fieldName, string value)
         => CreateWithValue(IsLessThanErrorCode, fieldName, value);

      public static ErrorMessage IsLessThanOrEqualTo(string fieldName, string value)
         => CreateWithValue(IsLessThanOrEqualToErrorCode, fieldName, value);

      public static ErrorMessage IsPositive(string fieldName)
         => Create(IsPositiveErrorCode, fieldName);

      public static ErrorMessage IsNegative(string fieldName)
         => Create(IsNegativeErrorCode, fieldName);

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
