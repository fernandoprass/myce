using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class EnumErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string TypeName = "typeName";

      public const string InvalidEnumValueErrorCode = nameof(InvalidEnumValueErrorCode);
      public const string MustNotBeDefaultValueErrorCode = nameof(MustNotBeDefaultValueErrorCode);
      public const string NotInEnumErrorCode = nameof(NotInEnumErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            InvalidEnumValueErrorCode, new()
            {
               { "en-US", "'{fieldName}' has an invalid value for {typeName}." },
               { "pt-BR", "'{fieldName}' possui um valor inválido para {typeName}." }
            }
         },
         {
            MustNotBeDefaultValueErrorCode, new()
            {
               { "en-US", "'{fieldName}' must not be the default value." },
               { "pt-BR", "'{fieldName}' não deve ser o valor padrão." }
            }
         },
         {
            NotInEnumErrorCode, new()
            {
               { "en-US", "'{fieldName}' cannot be a defined value of {typeName}." },
               { "pt-BR", "'{fieldName}' não pode ser um valor definido de {typeName}." }
            }
         }
      };

      public static ErrorMessage InvalidEnumValue(string fieldName, string typeName)
      {
         var error = new ErrorMessage(InvalidEnumValueErrorCode, _localTranslations[InvalidEnumValueErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(TypeName, typeName);
         return error;
      }

      public static ErrorMessage MustNotBeDefaultValue(string fieldName)
      {
         var error = new ErrorMessage(MustNotBeDefaultValueErrorCode, _localTranslations[MustNotBeDefaultValueErrorCode]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage NotInEnum(string fieldName, string typeName)
      {
         var error = new ErrorMessage(NotInEnumErrorCode, _localTranslations[NotInEnumErrorCode]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(TypeName, typeName);
         return error;
      }
   }
}