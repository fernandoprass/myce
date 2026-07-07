using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class EnumErrorMessages
{
   public const string InvalidEnumValueError = nameof(InvalidEnumValueError);
   public const string MustNotBeDefaultValueError = nameof(MustNotBeDefaultValueError);
   public const string NotInEnumError = nameof(NotInEnumError);

   public static ErrorMessage InvalidEnumValue(
      string fieldName,
      string typeName)
      => ValidationError.Create(
         InvalidEnumValueError,
         ("fieldName", fieldName),
         ("typeName", typeName));

   public static ErrorMessage MustNotBeDefaultValue(string fieldName)
      => ValidationError.Create(
         MustNotBeDefaultValueError,
         ("fieldName", fieldName));

   public static ErrorMessage NotInEnum(
      string fieldName,
      string typeName)
      => ValidationError.Create(
         NotInEnumError,
         ("fieldName", fieldName),
         ("typeName", typeName));
}
