using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class StateErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";

      public const string IsFalseErrorCode = nameof(IsFalseErrorCode);
      public const string IsTrueErrorCode = nameof(IsTrueErrorCode);
      public const string IsNullErrorCode = nameof(IsNullErrorCode);
      public const string IsNotNullErrorCode = nameof(IsNotNullErrorCode);
      public const string IsRequiredErrorCode = nameof(IsRequiredErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            IsFalseErrorCode, new()
            {
               { "en-US", "'{fieldName}' is false." },
               { "pt-BR", "'{fieldName}' é falso(a)." }
            }
         },
         {
            IsTrueErrorCode, new()
            {
               { "en-US", "'{fieldName}' is true." },
               { "pt-BR", "'{fieldName}' é verdadeiro(a)." }
            }
         },
         {
            IsNullErrorCode, new()
            {
               { "en-US", "'{fieldName}' is null." },
               { "pt-BR", "'{fieldName}' é nulo(a)." }
            }
         },
         {
            IsNotNullErrorCode, new()
            {
               { "en-US", "'{fieldName}' is not null." },
               { "pt-BR", "'{fieldName}' não é nulo(a)." }
            }
         },
         {
            IsRequiredErrorCode, new()
            {
               { "en-US", "'{fieldName}' is required." },
               { "pt-BR", "'{fieldName}' é obrigatório(a)." }
            }
         }
      };

      public static ErrorMessage IsFalse(string fieldName)
      {
         var error = new ErrorMessage(IsFalseErrorCode, _localTranslations[IsFalseErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsTrue(string fieldName)
      {
         var error = new ErrorMessage(IsTrueErrorCode, _localTranslations[IsTrueErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsNull(string fieldName)
      {
         var error = new ErrorMessage(IsNullErrorCode, _localTranslations[IsNullErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsNotNull(string fieldName)
      {
         var error = new ErrorMessage(IsNotNullErrorCode, _localTranslations[IsNotNullErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsRequired(string fieldName)
      {
         var error = new ErrorMessage(IsRequiredErrorCode, _localTranslations[IsRequiredErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}