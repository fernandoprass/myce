using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class ComparisonErrorMessages
{
   public const string MustBeEqualError = nameof(MustBeEqualError);
   public const string MustNotBeEqualError = nameof(MustNotBeEqualError);
   public const string MustBeEqualToFieldError = nameof(MustBeEqualToFieldError);
   public const string MustNotBeEqualToFieldError = nameof(MustNotBeEqualToFieldError);

   public static ErrorMessage MustBeEqual(string fieldName, string value)
      => ValidationError.Create(
         MustBeEqualError,
         ("fieldName", fieldName),
         ("value", value));

   public static ErrorMessage MustNotBeEqual(string fieldName, string value)
      => ValidationError.Create(
         MustNotBeEqualError,
         ("fieldName", fieldName),
         ("value", value));

   public static ErrorMessage MustBeEqualToField(
      string fieldName,
      string comparisonName)
      => ValidationError.Create(
         MustBeEqualToFieldError,
         ("fieldName", fieldName),
         ("comparisonName", comparisonName));

   public static ErrorMessage MustNotBeEqualToField(
      string fieldName,
      string comparisonName)
      => ValidationError.Create(
         MustNotBeEqualToFieldError,
         ("fieldName", fieldName),
         ("comparisonName", comparisonName));
}
