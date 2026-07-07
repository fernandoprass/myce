using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;

namespace Myce.FluentValidator;

public sealed class ValidationMessageProvider : IValidationMessageProvider
{
   private const string DefaultCultureName = "en-US";

   private readonly Dictionary<string, Dictionary<string, string>> _templates;

   public static ValidationMessageProvider Default { get; } = CreateDefault();

   public ValidationMessageProvider()
   {
      _templates = new Dictionary<string, Dictionary<string, string>>(
         StringComparer.OrdinalIgnoreCase);
   }

   private ValidationMessageProvider(
      Dictionary<string, Dictionary<string, string>> templates)
   {
      _templates = templates;
   }

   public ValidationMessageProvider WithTranslation(
      string code,
      CultureInfo culture,
      string template)
   {
      if (string.IsNullOrWhiteSpace(code))
         throw new ArgumentException("Message code cannot be empty.", nameof(code));
      if (culture == null)
         throw new ArgumentNullException(nameof(culture));

      var copy = CopyTemplates();
      Add(copy, code, culture, template);
      return new ValidationMessageProvider(copy);
   }

   public bool TryCreate(
      string code,
      CultureInfo culture,
      IEnumerable<Variable> arguments,
      out ErrorMessage message)
   {
      if (!_templates.TryGetValue(code, out var translations))
      {
         message = null!;
         return false;
      }

      culture = CultureInfo.GetCultureInfo(culture.Name);
      string resolvedCulture = translations.ContainsKey(culture.Name)
         ? culture.Name
         : translations.ContainsKey(DefaultCultureName)
            ? DefaultCultureName
            : translations.Keys.First();

      message = new ErrorMessage(
         code,
         translations[resolvedCulture],
         CultureInfo.GetCultureInfo(resolvedCulture));

      foreach (var argument in arguments)
      {
         message.AddVariable(
            CultureInfo.GetCultureInfo(resolvedCulture),
            argument.Name,
            argument.Value);
      }

      return true;
   }

   private Dictionary<string, Dictionary<string, string>> CopyTemplates()
      => _templates.ToDictionary(
         item => item.Key,
         item => new Dictionary<string, string>(
            item.Value,
            StringComparer.OrdinalIgnoreCase),
         StringComparer.OrdinalIgnoreCase);

   private static ValidationMessageProvider CreateDefault()
   {
      var provider = new ValidationMessageProvider();
      var en = CultureInfo.GetCultureInfo("en-US");
      var pt = CultureInfo.GetCultureInfo("pt-BR");

      void Both(string code, string english, string portuguese)
      {
         Add(provider._templates, code, en, english);
         Add(provider._templates, code, pt, portuguese);
      }

      Both(CollectionErrorMessages.ContainsInvalidValueError, "'{fieldName}' contains an invalid value.", "'{fieldName}' contém um valor inválido.");
      Both(CollectionErrorMessages.IsEmptyError, "'{fieldName}' is not empty.", "'{fieldName}' não está vazio(a).");
      Both(CollectionErrorMessages.IsNotEmptyError, "'{fieldName}' is empty.", "'{fieldName}' está vazio(a).");
      Both(CollectionErrorMessages.InvalidNumberOfItemsError, "'{fieldName}' contains an invalid number of items ({condition}).", "'{fieldName}' contém uma quantidade inválida de itens ({condition}).");
      Both(CollectionErrorMessages.MaxNumberOfItemsError, "'{fieldName}' must have at most {max} items.", "'{fieldName}' deve ter no máximo {max} itens.");
      Both(CollectionErrorMessages.ContainsDuplicateItemsError, "'{fieldName}' contains duplicate items.", "'{fieldName}' contém itens duplicados.");

      Both(ComparisonErrorMessages.MustBeEqualError, "'{fieldName}' must be equal to '{value}'.", "'{fieldName}' deve ser igual a '{value}'.");
      Both(ComparisonErrorMessages.MustNotBeEqualError, "'{fieldName}' must not be equal to '{value}'.", "'{fieldName}' não deve ser igual a '{value}'.");
      Both(ComparisonErrorMessages.MustBeEqualToFieldError, "'{fieldName}' must be equal to '{comparisonName}'.", "'{fieldName}' deve ser igual a '{comparisonName}'.");
      Both(ComparisonErrorMessages.MustNotBeEqualToFieldError, "'{fieldName}' must not be equal to '{comparisonName}'.", "'{fieldName}' não deve ser igual a '{comparisonName}'.");

      Both(DateErrorMessages.IsTodayError, "'{fieldName}' must be today.", "'{fieldName}' deve ser hoje.");
      Both(DateErrorMessages.IsYesterdayError, "'{fieldName}' must be yesterday.", "'{fieldName}' deve ser ontem.");
      Both(DateErrorMessages.IsTomorrowError, "'{fieldName}' must be tomorrow.", "'{fieldName}' deve ser amanhã.");
      Both(DateErrorMessages.IsInTheFutureError, "'{fieldName}' must be a future date.", "'{fieldName}' deve ser uma data futura.");
      Both(DateErrorMessages.IsInThePastError, "'{fieldName}' must be a past date.", "'{fieldName}' deve ser uma data passada.");
      Both(DateErrorMessages.IsWeekendError, "'{fieldName}' must be a weekend date.", "'{fieldName}' deve ser uma data de fim de semana.");
      Both(DateErrorMessages.IsWeekdayError, "'{fieldName}' must be a weekday.", "'{fieldName}' deve ser um dia útil.");

      Both(EnumErrorMessages.InvalidEnumValueError, "'{fieldName}' has an invalid value for '{typeName}'.", "'{fieldName}' possui um valor inválido para '{typeName}'.");
      Both(EnumErrorMessages.MustNotBeDefaultValueError, "'{fieldName}' must not be the default value.", "'{fieldName}' não deve ser o valor padrão.");
      Both(EnumErrorMessages.NotInEnumError, "'{fieldName}' cannot be a defined value of '{typeName}'.", "'{fieldName}' não pode ser um valor definido de '{typeName}'.");

      Both(NumericErrorMessages.IsBetweenError, "'{fieldName}' must be between {min} and {max}.", "'{fieldName}' deve estar entre {min} e {max}.");
      Both(NumericErrorMessages.IsGreaterThanError, "'{fieldName}' must be greater than {value}.", "'{fieldName}' deve ser maior que {value}.");
      Both(NumericErrorMessages.IsGreaterThanOrEqualToError, "'{fieldName}' must be greater than or equal to {value}.", "'{fieldName}' deve ser maior ou igual a {value}.");
      Both(NumericErrorMessages.IsLessThanError, "'{fieldName}' must be less than {value}.", "'{fieldName}' deve ser menor que {value}.");
      Both(NumericErrorMessages.IsLessThanOrEqualToError, "'{fieldName}' must be less than or equal to {value}.", "'{fieldName}' deve ser menor ou igual a {value}.");
      Both(NumericErrorMessages.IsPositiveError, "'{fieldName}' must be positive.", "'{fieldName}' deve ser positivo.");
      Both(NumericErrorMessages.IsNegativeError, "'{fieldName}' must be negative.", "'{fieldName}' deve ser negativo.");

      Both(StateErrorMessages.IsFalseError, "'{fieldName}' is false.", "'{fieldName}' é falso(a).");
      Both(StateErrorMessages.IsTrueError, "'{fieldName}' is true.", "'{fieldName}' é verdadeiro(a).");
      Both(StateErrorMessages.IsNullError, "'{fieldName}' is null.", "'{fieldName}' é nulo(a).");
      Both(StateErrorMessages.IsNotNullError, "'{fieldName}' is not null.", "'{fieldName}' não é nulo(a).");
      Both(StateErrorMessages.IsRequiredError, "'{fieldName}' is required.", "'{fieldName}' é obrigatório(a).");

      Both(StringErrorMessages.FewerCharactersThanExpectedError, "'{fieldName}' has fewer characters than expected ({minLength}).", "'{fieldName}' possui menos caracteres que o esperado ({minLength}).");
      Both(StringErrorMessages.InvalidDateError, "'{fieldName}' is not a valid date.", "'{fieldName}' não é uma data válida.");
      Both(StringErrorMessages.InvalidEmailError, "'{fieldName}' is not a valid email.", "'{fieldName}' não é um e-mail válido.");
      Both(StringErrorMessages.InvalidFormatError, "'{fieldName}' does not match the expected format.", "'{fieldName}' não corresponde ao formato esperado.");
      Both(StringErrorMessages.MoreCharactersThanExpectedError, "'{fieldName}' has more characters than expected ({maxLength}).", "'{fieldName}' possui mais caracteres que o esperado ({maxLength}).");
      Both(StringErrorMessages.NotExactNumberOfCharactersError, "'{fieldName}' does not have the expected number of characters ({length}).", "'{fieldName}' não possui o número esperado de caracteres ({length}).");
      Both(StringErrorMessages.ShouldContainOnlyNumberError, "'{fieldName}' should contain only numbers.", "'{fieldName}' deve conter apenas números.");
      Both(StringErrorMessages.MustContainSubstringError, "'{fieldName}' must contain '{substring}'.", "'{fieldName}' deve conter '{substring}'.");
      Both(StringErrorMessages.ShouldContainOnlyLettersError, "'{fieldName}' must contain only letters.", "'{fieldName}' deve conter apenas letras.");
      Both(StringErrorMessages.ShouldContainOnlyLettersAndNumbersError, "'{fieldName}' must contain only letters and numbers.", "'{fieldName}' deve conter apenas letras e números.");

      return provider;
   }

   private static void Add(
      Dictionary<string, Dictionary<string, string>> templates,
      string code,
      CultureInfo culture,
      string template)
   {
      if (!templates.TryGetValue(code, out var translations))
      {
         translations = new Dictionary<string, string>(
            StringComparer.OrdinalIgnoreCase);
         templates[code] = translations;
      }

      translations[culture.Name] = template;
   }
}
