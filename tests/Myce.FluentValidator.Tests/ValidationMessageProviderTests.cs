using System.Globalization;
using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests;

public class ValidationMessageProviderTests
{
   [Fact]
   public void WithTranslation_ShouldCreateIndependentProvider()
   {
      var french = CultureInfo.GetCultureInfo("fr-FR");

      var provider = ValidationMessageProvider.Default.WithTranslation(
         StateErrorMessages.IsRequiredError,
         french,
         "'{fieldName}' est obligatoire.");

      Assert.True(provider.TryCreate(
         StateErrorMessages.IsRequiredError,
         french,
         new[] { new Variable("fieldName", "Nom") },
         out var message));
      Assert.Equal("'Nom' est obligatoire.", message.Show());

      Assert.True(ValidationMessageProvider.Default.TryCreate(
         StateErrorMessages.IsRequiredError,
         french,
         new[] { new Variable("fieldName", "Name") },
         out var fallback));
      Assert.Equal("'Name' is required.", fallback.Show());
   }

   [Fact]
   public void TryCreate_WithUnknownCode_ShouldReturnFalse()
   {
      bool created = ValidationMessageProvider.Default.TryCreate(
         "UNKNOWN",
         CultureInfo.GetCultureInfo("en-US"),
         Array.Empty<Variable>(),
         out _);

      Assert.False(created);
   }

   [Fact]
   public void EveryBuiltInCode_ShouldResolveForBuiltInCultures()
   {
      string[] codes =
      [
         CollectionErrorMessages.ContainsInvalidValueError,
         CollectionErrorMessages.IsEmptyError,
         CollectionErrorMessages.IsNotEmptyError,
         CollectionErrorMessages.InvalidNumberOfItemsError,
         CollectionErrorMessages.MaxNumberOfItemsError,
         CollectionErrorMessages.ContainsDuplicateItemsError,
         ComparisonErrorMessages.MustBeEqualError,
         ComparisonErrorMessages.MustNotBeEqualError,
         ComparisonErrorMessages.MustBeEqualToFieldError,
         ComparisonErrorMessages.MustNotBeEqualToFieldError,
         DateErrorMessages.IsTodayError,
         DateErrorMessages.IsYesterdayError,
         DateErrorMessages.IsTomorrowError,
         DateErrorMessages.IsInTheFutureError,
         DateErrorMessages.IsInThePastError,
         DateErrorMessages.IsWeekendError,
         DateErrorMessages.IsWeekdayError,
         EnumErrorMessages.InvalidEnumValueError,
         EnumErrorMessages.MustNotBeDefaultValueError,
         EnumErrorMessages.NotInEnumError,
         NumericErrorMessages.IsBetweenError,
         NumericErrorMessages.IsGreaterThanError,
         NumericErrorMessages.IsGreaterThanOrEqualToError,
         NumericErrorMessages.IsLessThanError,
         NumericErrorMessages.IsLessThanOrEqualToError,
         NumericErrorMessages.IsPositiveError,
         NumericErrorMessages.IsNegativeError,
         StateErrorMessages.IsFalseError,
         StateErrorMessages.IsTrueError,
         StateErrorMessages.IsNullError,
         StateErrorMessages.IsNotNullError,
         StateErrorMessages.IsRequiredError,
         StringErrorMessages.FewerCharactersThanExpectedError,
         StringErrorMessages.InvalidDateError,
         StringErrorMessages.InvalidEmailError,
         StringErrorMessages.InvalidFormatError,
         StringErrorMessages.MoreCharactersThanExpectedError,
         StringErrorMessages.NotExactNumberOfCharactersError,
         StringErrorMessages.ShouldContainOnlyNumberError,
         StringErrorMessages.MustContainSubstringError,
         StringErrorMessages.ShouldContainOnlyLettersError,
         StringErrorMessages.ShouldContainOnlyLettersAndNumbersError
      ];

      foreach (string code in codes)
      {
         Assert.True(ValidationMessageProvider.Default.TryCreate(
            code,
            CultureInfo.GetCultureInfo("en-US"),
            Array.Empty<Variable>(),
            out _));
         Assert.True(ValidationMessageProvider.Default.TryCreate(
            code,
            CultureInfo.GetCultureInfo("pt-BR"),
            Array.Empty<Variable>(),
            out _));
      }
   }
}
