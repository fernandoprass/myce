using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class CollectionErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string Condition = "condition";
      private const string Max = "max";

      public const string ContainsInvalidValueError = nameof(ContainsInvalidValueError);
      public const string IsEmptyError = nameof(IsEmptyError);
      public const string IsNotEmptyError = nameof(IsNotEmptyError);
      public const string InvalidNumberOfItemsError = nameof(InvalidNumberOfItemsError);
      public const string MaxNumberOfItemsError = nameof(MaxNumberOfItemsError);
      public const string ContainsDuplicateItemsError = nameof(ContainsDuplicateItemsError);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            ContainsInvalidValueError, new()
            {
               { "en-US", "'{fieldName}' contains an invalid value." },
               { "pt-BR", "'{fieldName}' contém um valor inválido." }
            }
         },
         {
            IsEmptyError, new()
            {
               { "en-US", "'{fieldName}' is not empty." },
               { "pt-BR", "'{fieldName}' não está vazio(a)." }
            }
         },
         {
            IsNotEmptyError, new()
            {
               { "en-US", "'{fieldName}' is empty." },
               { "pt-BR", "'{fieldName}' está vazio(a)." }
            }
         },
         {
            InvalidNumberOfItemsError, new()
            {
               { "en-US", "'{fieldName}' contains an invalid number of items ({condition})." },
               { "pt-BR", "'{fieldName}' contém uma quantidade inválida de itens ({condition})." }
            }
         },
         {
            MaxNumberOfItemsError, new()
            {
               { "en-US", "'{fieldName}' must have at most {max} items." },
               { "pt-BR", "'{fieldName}' deve ter no máximo {max} itens." }
            }
         },
         {
            ContainsDuplicateItemsError, new()
            {
               { "en-US", "'{fieldName}' contains duplicate items." },
               { "pt-BR", "'{fieldName}' contém itens duplicados." }
            }
         }
      };

      public static ErrorMessage ContainsInvalidValue(string fieldName)
      {
         var error = new ErrorMessage(ContainsInvalidValueError, _localTranslations[ContainsInvalidValueError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsEmpty(string fieldName)
      {
         var error = new ErrorMessage(IsEmptyError, _localTranslations[IsEmptyError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage IsNotEmpty(string fieldName)
      {
         var error = new ErrorMessage(IsNotEmptyError, _localTranslations[IsNotEmptyError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage InvalidNumberOfItems(string fieldName, int condition)
      {
         var error = new ErrorMessage(InvalidNumberOfItemsError, _localTranslations[InvalidNumberOfItemsError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Condition, condition.ToString());
         return error;
      }

      public static ErrorMessage MaxNumberOfItems(string fieldName, int max)
      {
         var error = new ErrorMessage(MaxNumberOfItemsError, _localTranslations[MaxNumberOfItemsError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(Max, max.ToString());
         return error;
      }

      public static ErrorMessage ContainsDuplicateItems(string fieldName)
      {
         var error = new ErrorMessage(ContainsDuplicateItemsError, _localTranslations[ContainsDuplicateItemsError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}