using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class NumericErrorMessages
{
   public const string IsBetweenError = nameof(IsBetweenError);
   public const string IsGreaterThanError = nameof(IsGreaterThanError);
   public const string IsGreaterThanOrEqualToError = nameof(IsGreaterThanOrEqualToError);
   public const string IsLessThanError = nameof(IsLessThanError);
   public const string IsLessThanOrEqualToError = nameof(IsLessThanOrEqualToError);
   public const string IsPositiveError = nameof(IsPositiveError);
   public const string IsNegativeError = nameof(IsNegativeError);

   public static ErrorMessage IsBetween(
      string fieldName,
      string min,
      string max)
      => ValidationError.Create(
         IsBetweenError,
         ("fieldName", fieldName),
         ("min", min),
         ("max", max));

   public static ErrorMessage IsGreaterThan(string fieldName, string value)
      => WithValue(IsGreaterThanError, fieldName, value);

   public static ErrorMessage IsGreaterThanOrEqualTo(
      string fieldName,
      string value)
      => WithValue(IsGreaterThanOrEqualToError, fieldName, value);

   public static ErrorMessage IsLessThan(string fieldName, string value)
      => WithValue(IsLessThanError, fieldName, value);

   public static ErrorMessage IsLessThanOrEqualTo(
      string fieldName,
      string value)
      => WithValue(IsLessThanOrEqualToError, fieldName, value);

   public static ErrorMessage IsPositive(string fieldName)
      => ValidationError.Create(IsPositiveError, ("fieldName", fieldName));

   public static ErrorMessage IsNegative(string fieldName)
      => ValidationError.Create(IsNegativeError, ("fieldName", fieldName));

   private static ErrorMessage WithValue(
      string code,
      string fieldName,
      string value)
      => ValidationError.Create(
         code,
         ("fieldName", fieldName),
         ("value", value));
}
