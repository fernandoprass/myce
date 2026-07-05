using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class EnumErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";
      private const string TypeName = "typeName";

      public const string InvalidEnumValueError = nameof(InvalidEnumValueError);
      public const string MustNotBeDefaultValueError = nameof(MustNotBeDefaultValueError);
      public const string NotInEnumError = nameof(NotInEnumError);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            InvalidEnumValueError, new()
            {
               { "en-US", "'{fieldName}' has an invalid value for '{typeName}'." },
               { "pt-BR", "'{fieldName}' possui um valor inválido para '{typeName}'." }
            }
         },
         {
            MustNotBeDefaultValueError, new()
            {
               { "en-US", "'{fieldName}' must not be the default value." },
               { "pt-BR", "'{fieldName}' não deve ser o valor padrão." }
            }
         },
         {
            NotInEnumError, new()
            {
               { "en-US", "'{fieldName}' cannot be a defined value of '{typeName}'." },
               { "pt-BR", "'{fieldName}' não pode ser um valor definido de '{typeName}'." }
            }
         }
      };

      public static ErrorMessage InvalidEnumValue(string fieldName, string typeName)
      {
         var error = new ErrorMessage(InvalidEnumValueError, _localTranslations[InvalidEnumValueError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(TypeName, typeName);
         return error;
      }

      public static ErrorMessage MustNotBeDefaultValue(string fieldName)
      {
         var error = new ErrorMessage(MustNotBeDefaultValueError, _localTranslations[MustNotBeDefaultValueError]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }

      public static ErrorMessage NotInEnum(string fieldName, string typeName)
      {
         var error = new ErrorMessage(NotInEnumError, _localTranslations[NotInEnumError]);
         error.AddVariable(FieldName, fieldName);
         error.AddVariable(TypeName, typeName);
         return error;
      }
   }
}