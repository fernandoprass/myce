using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class CollectionErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string Condition = "condition";
      private const string Max = "max";

      public const string ContainsInvalidValueErrorCode = nameof(ContainsInvalidValueErrorCode);
      public const string IsEmptyErrorCode = nameof(IsEmptyErrorCode);
      public const string IsNotEmptyErrorCode = nameof(IsNotEmptyErrorCode);
      public const string InvalidNumberOfItemsErrorCode = nameof(InvalidNumberOfItemsErrorCode);
      public const string MaxNumberOfItemsErrorCode = nameof(MaxNumberOfItemsErrorCode);
      public const string ContainsDuplicateItemsErrorCode = nameof(ContainsDuplicateItemsErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            ContainsInvalidValueErrorCode, new()
            {
               { "en-US", "'{fieldName}' contains an invalid value." },
               { "pt-BR", "'{fieldName}' contém um valor inválido." }
            }
         },
         {
            IsEmptyErrorCode, new()
            {
               { "en-US", "'{fieldName}' is not empty." },
               { "pt-BR", "'{fieldName}' não está vazio(a)." }
            }
         },
         {
            IsNotEmptyErrorCode, new()
            {
               { "en-US", "'{fieldName}' is empty." },
               { "pt-BR", "'{fieldName}' está vazio(a)." }
            }
         },
         {
            InvalidNumberOfItemsErrorCode, new()
            {
               { "en-US", "'{fieldName}' contains an invalid number of items ({condition})." },
               { "pt-BR", "'{fieldName}' contém uma quantidade inválida de itens ({condition})." }
            }
         },
         {
            MaxNumberOfItemsErrorCode, new()
            {
               { "en-US", "'{fieldName}' must have at most {max} items." },
               { "pt-BR", "'{fieldName}' deve ter no máximo {max} itens." }
            }
         },
         {
            ContainsDuplicateItemsErrorCode, new()
            {
               { "en-US", "'{fieldName}' contains duplicate items." },
               { "pt-BR", "'{fieldName}' contém itens duplicados." }
            }
         }
      };

      public static ErrorMessage ContainsInvalidValue(string fieldName)
      {
         var error = new ErrorMessage(ContainsInvalidValueErrorCode, _localTranslations[ContainsInvalidValueErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsEmpty(string fieldName)
      {
         var error = new ErrorMessage(IsEmptyErrorCode, _localTranslations[IsEmptyErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsNotEmpty(string fieldName)
      {
         var error = new ErrorMessage(IsNotEmptyErrorCode, _localTranslations[IsNotEmptyErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage InvalidNumberOfItems(string fieldName, int condition)
      {
         var error = new ErrorMessage(InvalidNumberOfItemsErrorCode, _localTranslations[InvalidNumberOfItemsErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Condition, condition.ToString());
         return error;
      }

      public static ErrorMessage MaxNumberOfItems(string fieldName, int max)
      {
         var error = new ErrorMessage(MaxNumberOfItemsErrorCode, _localTranslations[MaxNumberOfItemsErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Max, max.ToString());
         return error;
      }

      public static ErrorMessage ContainsDuplicateItems(string fieldName)
      {
         var error = new ErrorMessage(ContainsDuplicateItemsErrorCode, _localTranslations[ContainsDuplicateItemsErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}