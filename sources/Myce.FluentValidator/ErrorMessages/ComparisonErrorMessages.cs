using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class ComparisonErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string Value = "value";
      private const string ComparisonName = "comparisonName";

      public const string MustBeEqualErrorCode = nameof(MustBeEqualErrorCode);
      public const string MustNotBeEqualErrorCode = nameof(MustNotBeEqualErrorCode);
      public const string MustBeEqualToFieldErrorCode = nameof(MustBeEqualToFieldErrorCode);
      public const string MustNotBeEqualToFieldErrorCode = nameof(MustNotBeEqualToFieldErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            MustBeEqualErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be equal to {value}." },
               { "pt-BR", "'{fieldName}' deve ser igual a {value}." }
            }
         },
         {
            MustNotBeEqualErrorCode, new()
            {
               { "en-US", "'{fieldName}' must not be equal to {value}." },
               { "pt-BR", "'{fieldName}' não deve ser igual a {value}." }
            }
         },
         {
            MustBeEqualToFieldErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be equal to '{comparisonName}'." },
               { "pt-BR", "'{fieldName}' deve ser igual a '{comparisonName}'." }
            }
         },
         {
            MustNotBeEqualToFieldErrorCode, new()
            {
               { "en-US", "'{fieldName}' must not be equal to '{comparisonName}'." },
               { "pt-BR", "'{fieldName}' não deve ser igual a '{comparisonName}'." }
            }
         }
      };

      public static ErrorMessage MustBeEqual(string fieldName, string value)
      {
         var error = new ErrorMessage(MustBeEqualErrorCode, _localTranslations[MustBeEqualErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Value, value);
         return error;
      }

      public static ErrorMessage MustNotBeEqual(string fieldName, string value)
      {
         var error = new ErrorMessage(MustNotBeEqualErrorCode, _localTranslations[MustNotBeEqualErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Value, value);
         return error;
      }

      public static ErrorMessage MustBeEqualToField(string fieldName, string comparisonName)
      {
         var error = new ErrorMessage(MustBeEqualToFieldErrorCode, _localTranslations[MustBeEqualToFieldErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(ComparisonName, comparisonName);
         return error;
      }

      public static ErrorMessage MustNotBeEqualToField(string fieldName, string comparisonName)
      {
         var error = new ErrorMessage(MustNotBeEqualToFieldErrorCode, _localTranslations[MustNotBeEqualToFieldErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(ComparisonName, comparisonName);
         return error;
      }
   }
}
