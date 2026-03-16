using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for string-specific validation rules, such as checks for substrings, 
   /// character types, length constraints, and format validations like email and date formats.
   /// </summary>
   public static partial class RuleBuilderStringExtensions
   {
      /// <summary>
      /// Validates that the string contains a specific substring using OrdinalIgnoreCase.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="substring">The substring to search for.</param>
      public static RuleBuilder<T, string> Contains<T>(this RuleBuilder<T, string> rb, string substring) where T : class
      {
         var name = rb.GetAttributeName();
         return rb.Contains(substring, StringComparison.OrdinalIgnoreCase);
      }

      /// <summary>
      /// Validates that the string contains a specific substring.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="substring">The substring to search for.</param>
      /// <param name="stringComparison">The string comparison option to use when checking for the substring.</param>
      public static RuleBuilder<T, string> Contains<T>(this RuleBuilder<T, string> rb, string substring, StringComparison stringComparison) where T : class
      {
         var name = rb.GetAttributeName();
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return value != null && value.Contains(substring, stringComparison);
         }, new MustContainSubstringError(name, substring));
      }

      /// <summary>
      /// Validates if a string contains only numeric characters.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      public static RuleBuilder<T, string> ContainsOnlyNumber<T>(this RuleBuilder<T, string> ruleBuilder) where T : class
      {
         return ruleBuilder.ContainsOnlyNumber(new ShouldContainOnlyNumberError(ruleBuilder.GetAttributeName()));
      }

      /// <summary>
      /// Validates if a string contains only numeric characters with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public static RuleBuilder<T, string> ContainsOnlyNumber<T>(this RuleBuilder<T, string> ruleBuilder, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.All(char.IsNumber);
         }, message);
      }

      /// <summary>
      /// Determines whether a string has an exact character length.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharacters<T>(this RuleBuilder<T, string> ruleBuilder, int length) where T : class
      {
         return ruleBuilder.ExactNumberOfCharacters(length, new NotExactNumberOfCharactersError(ruleBuilder.GetAttributeName(), length));
      }

      /// <summary>
      /// Determines whether a string has an exact character length with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharacters<T>(this RuleBuilder<T, string> ruleBuilder, int length, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.Length == length;
         }, message);
      }

      /// <summary>
      /// Determines whether a string has an exact length if a given condition is true.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="condition">The condition to verify.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharactersIf<T>(this RuleBuilder<T, string> ruleBuilder, int length, bool condition) where T : class
      {
         return condition ? ruleBuilder.ExactNumberOfCharacters(length) : ruleBuilder;
      }

      /// <summary>
      /// Determines whether a string has an exact length if a given condition is true with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="condition">The condition to verify.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharactersIf<T>(this RuleBuilder<T, string> ruleBuilder, int length, bool condition, ErrorMessage message) where T : class
      {
         return condition ? ruleBuilder.ExactNumberOfCharacters(length, message) : ruleBuilder;
      }

      /// <summary>
      /// Determines whether a string is a valid date.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      public static RuleBuilder<T, string> IsValidDate<T>(this RuleBuilder<T, string> ruleBuilder) where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsValidDate(new InvalidDateError(attributeName));
      }

      /// <summary>
      /// Determines whether a string is a valid date with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="message">The custom error message.</param>
      public static RuleBuilder<T, string> IsValidDate<T>(this RuleBuilder<T, string> ruleBuilder, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) ? false : DateTime.TryParse(value, out _);
         }, message);
      }

      /// <summary>
      /// Validates if the property value is a valid email address.
      /// </summary>
      public static RuleBuilder<T, string> IsValidEmailAddress<T>(this RuleBuilder<T, string> ruleBuilder) where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsValidEmailAddress(new InvalidEmailError(attributeName));
      }

      /// <summary>
      /// Validates if the property value is a valid email address with a custom message.
      /// </summary>
      /// <param name="message">The custom error message.</param>
      public static RuleBuilder<T, string> IsValidEmailAddress<T>(this RuleBuilder<T, string> ruleBuilder, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var email = ruleBuilder.GetAttributeValue(instance);
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");
         }, message);
      }

      /// <summary> 
      /// Validates that the string matches a specific regular expression pattern. 
      /// </summary>
      /// <param name="pattern">The Regex patterb</param>
      /// <param name="message">The custom error message.</param>
      public static RuleBuilder<T, string> Matches<T>(this RuleBuilder<T, string> rb, string pattern, ErrorMessage message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return value != null && Regex.IsMatch(value, pattern);
         }, message);
      }

      /// <summary> Validates that the string contains only alphabetic characters. </summary>
      public static RuleBuilder<T, string> IsAlpha<T>(this RuleBuilder<T, string> rb) where T : class
      {
         //todo create new Error Message for this case
         var name = rb.GetAttributeName();
         return rb.Matches(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$",
             new ShouldContainOnlyLettersError(name));
      }

      /// <summary> Validates that the string contains only alphanumeric characters. </summary>
      public static RuleBuilder<T, string> IsAlphaNumeric<T>(this RuleBuilder<T, string> rb) where T : class
      {
         //todo create new Error Message for this case
         var name = rb.GetAttributeName();
         return rb.Matches(@"^[a-zA-Z0-9]+$",
             new ShouldContainOnlyLettersAndNumbersError(name));
      }

      /// <summary>
      /// Validates the maximum character length.
      /// </summary>
      /// <param name="maxLength">The maximum lenght allowed. </param>
      public static RuleBuilder<T, string> MaxLength<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength) where T : class
      {
         return ruleBuilder.MaxLength(maxLength, new MoreCharactersThanExpectedError(ruleBuilder.GetAttributeName(), maxLength));
      }

      /// <summary>
      /// Validates the maximum character length with a custom message.
      /// </summary>
      /// <param name="maxLength">The maximum lenght allowed. </param>
      /// <param name="message">The custom error message.</param>
      public static RuleBuilder<T, string> MaxLength<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.Length <= maxLength;
         }, message);
      }

      /// <summary>
      /// Validates the maximum character length if a condition is true.
      /// </summary>
      public static RuleBuilder<T, string> MaxLengthIf<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength, bool condition) where T : class
      {
         return condition ? ruleBuilder.MaxLength(maxLength) : ruleBuilder;
      }

      /// <summary>
      /// Validates the maximum length if a condition is true with a custom message.
      /// </summary>
      public static RuleBuilder<T, string> MaxLengthIf<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength, bool condition, ErrorMessage message) where T : class
      {
         return condition ? ruleBuilder.MaxLength(maxLength, message) : ruleBuilder;
      }

      /// <summary>
      /// Validates the minimum character length.
      /// </summary>
      public static RuleBuilder<T, string> MinLength<T>(this RuleBuilder<T, string> ruleBuilder, int minLength) where T : class
      {
         return ruleBuilder.MinLength(minLength, new FewerCharactersThanExpectedError(ruleBuilder.GetAttributeName(), minLength));
      }

      /// <summary>
      /// Validates the minimum character length with a custom message.
      /// </summary>
      public static RuleBuilder<T, string> MinLength<T>(this RuleBuilder<T, string> ruleBuilder, int minLength, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.Length >= minLength;
         }, message);
      }

      /// <summary>
      /// Validates the minimum character length if a condition is true.
      /// </summary>
      public static RuleBuilder<T, string> MinLengthIf<T>(this RuleBuilder<T, string> ruleBuilder, int minLength, bool condition) where T : class
      {
         return condition ? ruleBuilder.MinLength(minLength) : ruleBuilder;
      }

      /// <summary>
      /// Validates the minimum length if a condition is true with a custom message.
      /// </summary>
      public static RuleBuilder<T, string> MinLengthIf<T>(this RuleBuilder<T, string> ruleBuilder, int minLength, bool condition, ErrorMessage message) where T : class
      {
         return condition ? ruleBuilder.MinLength(minLength, message) : ruleBuilder;
      }
   }
}