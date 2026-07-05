using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class ComparisonErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string Value = "value";
      private const string ComparisonName = "comparisonName";

      public const string MustBeEqualError = nameof(MustBeEqualError);
      public const string MustNotBeEqualError = nameof(MustNotBeEqualError);
      public const string MustBeEqualToFieldError = nameof(MustBeEqualToFieldError);
      public const string MustNotBeEqualToFieldError = nameof(MustNotBeEqualToFieldError);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            MustBeEqualError, new()
            {
               { "en-US", "'{fieldName}' must be equal to '{value}'." },
               { "pt-BR", "'{fieldName}' deve ser igual a '{value}'." }
            }
         },
         {
            MustNotBeEqualError, new()
            {
               { "en-US", "'{fieldName}' must not be equal to '{value}'." },
               { "pt-BR", "'{fieldName}' não deve ser igual a '{value}'." }
            }
         },
         {
            MustBeEqualToFieldError, new()
            {
               { "en-US", "'{fieldName}' must be equal to '{comparisonName}'." },
               { "pt-BR", "'{fieldName}' deve ser igual a '{comparisonName}'." }
            }
         },
         {
            MustNotBeEqualToFieldError, new()
            {
               { "en-US", "'{fieldName}' must not be equal to '{comparisonName}'." },
               { "pt-BR", "'{fieldName}' não deve ser igual a '{comparisonName}'." }
            }
         }
      };

      public static ErrorMessage MustBeEqual(string fieldName, string value)
      {
         var error = new ErrorMessage(MustBeEqualError, _localTranslations[MustBeEqualError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Value, value);
         return error;
      }

      public static ErrorMessage MustNotBeEqual(string fieldName, string value)
      {
         var error = new ErrorMessage(MustNotBeEqualError, _localTranslations[MustNotBeEqualError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Value, value);
         return error;
      }

      public static ErrorMessage MustBeEqualToField(string fieldName, string comparisonName)
      {
         var error = new ErrorMessage(MustBeEqualToFieldError, _localTranslations[MustBeEqualToFieldError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(ComparisonName, comparisonName);
         return error;
      }

      public static ErrorMessage MustNotBeEqualToField(string fieldName, string comparisonName)
      {
         var error = new ErrorMessage(MustNotBeEqualToFieldError, _localTranslations[MustNotBeEqualToFieldError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(ComparisonName, comparisonName);
         return error;
      }
   }
}
