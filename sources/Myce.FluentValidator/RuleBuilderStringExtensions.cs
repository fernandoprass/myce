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
         var attributeName = rb.GetAttributeName();
         return rb.Contains(substring, StringComparison.OrdinalIgnoreCase, new MustContainSubstringError(attributeName, substring));
      }

      /// <summary>
      ///  Validates that the string contains a specific substring using OrdinalIgnoreCase.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="substring">The substring to search for.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> Contains<T>(this RuleBuilder<T, string> rb, string substring, Message message) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.Contains(substring, StringComparison.OrdinalIgnoreCase, message);
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
         var attributeName = rb.GetAttributeName();
         return rb.Contains(substring, stringComparison, new MustContainSubstringError(attributeName, substring));
      }

      /// <summary>
      /// Validates that the string contains a specific substring.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="substring">The substring to search for.</param>
      /// <param name="stringComparison">The string comparison option to use when checking for the substring.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> Contains<T>(this RuleBuilder<T, string> rb, string substring, StringComparison stringComparison, Message message) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return value != null && value.Contains(substring, stringComparison);
         }, message);
      }

      /// <summary>
      /// Validates if a string contains only numeric characters.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      public static RuleBuilder<T, string> ContainsOnlyNumber<T>(this RuleBuilder<T, string> rb) where T : class
      {
         return rb.ContainsOnlyNumber(new ShouldContainOnlyNumberError(rb.GetAttributeName()));
      }

      /// <summary>
      /// Validates if a string contains only numeric characters with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> ContainsOnlyNumber<T>(this RuleBuilder<T, string> rb, Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.All(char.IsNumber);
         }, message);
      }

      /// <summary>
      /// Determines whether a string has an exact character length.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharacters<T>(this RuleBuilder<T, string> rb, int length) where T : class
      {
         return rb.ExactNumberOfCharacters(length, new NotExactNumberOfCharactersError(rb.GetAttributeName(), length));
      }

      /// <summary>
      /// Determines whether a string has an exact character length with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharacters<T>(this RuleBuilder<T, string> rb, int length, Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.Length == length;
         }, message);
      }

      /// <summary>
      /// Determines whether a string has an exact length if a given condition is true.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="condition">The condition to verify.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharactersIf<T>(this RuleBuilder<T, string> rb, int length, bool condition) where T : class
      {
         return condition ? rb.ExactNumberOfCharacters(length) : rb;
      }

      /// <summary>
      /// Determines whether a string has an exact length if a given condition is true with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="condition">The condition to verify.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public static RuleBuilder<T, string> ExactNumberOfCharactersIf<T>(this RuleBuilder<T, string> rb, int length, bool condition, Message message) where T : class
      {
         return condition ? rb.ExactNumberOfCharacters(length, message) : rb;
      }

      /// <summary>
      /// Determines whether a string is a valid date.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      public static RuleBuilder<T, string> IsValidDate<T>(this RuleBuilder<T, string> rb) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.IsValidDate(new InvalidDateError(attributeName));
      }

      /// <summary>
      /// Determines whether a string is a valid date with a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> IsValidDate<T>(this RuleBuilder<T, string> ruleBuilder, Message message) where T : class
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
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> IsValidEmailAddress<T>(this RuleBuilder<T, string> ruleBuilder, Message message) where T : class
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
      /// <param name="pattern">The Regex pattern.</param>
      public static RuleBuilder<T, string> Matches<T>(this RuleBuilder<T, string> rb, string pattern) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.Matches(pattern, new InvalidEmailError(attributeName));
      }

      /// <summary> 
      /// Validates that the string matches a specific regular expression pattern. 
      /// </summary>
      /// <param name="pattern">The Regex pattern.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> Matches<T>(this RuleBuilder<T, string> rb, string pattern, Message message) where T : class
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
         var attributeName = rb.GetAttributeName();
         return rb.IsAlpha(new ShouldContainOnlyLettersError(attributeName));
      }

      /// <summary> Validates that the string contains only alphabetic characters. </summary>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> IsAlpha<T>(this RuleBuilder<T, string> rb, Message message) where T : class
      {
         return rb.Matches(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$", message);
      }

      /// <summary> Validates that the string contains only alphanumeric characters. </summary>
      public static RuleBuilder<T, string> IsAlphaNumeric<T>(this RuleBuilder<T, string> rb) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.IsAlphaNumeric(new ShouldContainOnlyLettersAndNumbersError(attributeName));
      }

      /// <summary> Validates that the string contains only alphanumeric characters. </summary>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> IsAlphaNumeric<T>(this RuleBuilder<T, string> rb, Message message) where T : class
      {
         return rb.Matches(@"^[a-zA-Z0-9]+$", message);
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
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, string> MaxLength<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength, Message message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.Length <= maxLength;
         }, message);
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
      public static RuleBuilder<T, string> MinLength<T>(this RuleBuilder<T, string> ruleBuilder, int minLength, Message message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            return string.IsNullOrEmpty(value) || value.Length >= minLength;
         }, message);
      }
   }
}