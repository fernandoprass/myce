using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class StateErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";

      public const string IsFalseError = nameof(IsFalseError);
      public const string IsTrueError = nameof(IsTrueError);
      public const string IsNullError = nameof(IsNullError);
      public const string IsNotNullError = nameof(IsNotNullError);
      public const string IsRequiredError = nameof(IsRequiredError);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            IsFalseError, new()
            {
               { "en-US", "'{fieldName}' is false." },
               { "pt-BR", "'{fieldName}' é falso(a)." }
            }
         },
         {
            IsTrueError, new()
            {
               { "en-US", "'{fieldName}' is true." },
               { "pt-BR", "'{fieldName}' é verdadeiro(a)." }
            }
         },
         {
            IsNullError, new()
            {
               { "en-US", "'{fieldName}' is null." },
               { "pt-BR", "'{fieldName}' é nulo(a)." }
            }
         },
         {
            IsNotNullError, new()
            {
               { "en-US", "'{fieldName}' is not null." },
               { "pt-BR", "'{fieldName}' não é nulo(a)." }
            }
         },
         {
            IsRequiredError, new()
            {
               { "en-US", "'{fieldName}' is required." },
               { "pt-BR", "'{fieldName}' é obrigatório(a)." }
            }
         }
      };

      public static ErrorMessage IsFalse(string fieldName)
      {
         var error = new ErrorMessage(IsFalseError, _localTranslations[IsFalseError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsTrue(string fieldName)
      {
         var error = new ErrorMessage(IsTrueError, _localTranslations[IsTrueError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsNull(string fieldName)
      {
         var error = new ErrorMessage(IsNullError, _localTranslations[IsNullError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsNotNull(string fieldName)
      {
         var error = new ErrorMessage(IsNotNullError, _localTranslations[IsNotNullError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsRequired(string fieldName)
      {
         var error = new ErrorMessage(IsRequiredError, _localTranslations[IsRequiredError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}