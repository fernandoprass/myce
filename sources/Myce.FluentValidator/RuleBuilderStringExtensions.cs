using Myce.Extensions;
using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Myce.FluentValidator
{
   public static partial class RuleBuilderStringExtensions
   {
      /// <summary>
      /// Determines whether a string is present within a sequence of allowed values.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="values">The sequence of allowed values.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public static RuleBuilder<T, string> Contains<T>(this RuleBuilder<T, string> ruleBuilder, string[] values, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var val = ruleBuilder.GetAttributeValue(instance);
            return val is not null && values.Contains(val);
         }, message);
      }

      /// <summary>
      /// Validates if a string contains only numeric characters.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      public static RuleBuilder<T, string> ContainsOnlyNumber<T>(this RuleBuilder<T, string> ruleBuilder) where T : class
      {
         return ruleBuilder.ContainsOnlyNumber(new ErrorShouldContainOnlyNumber(ruleBuilder.GetAttributeName()));
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
            var val = ruleBuilder.GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(val) || val.All(char.IsNumber);
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
         return ruleBuilder.ExactNumberOfCharacters(length, new ErrorNotExactNumberOfCharacters(ruleBuilder.GetAttributeName(), length));
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
            var value = ruleBuilder.GetAttributeValue(instance)?.ToString();
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
         return ruleBuilder.IsValidDate(new ErrorInvalidDate(attributeName));
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
            var value = ruleBuilder.GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) ? false : DateTime.TryParse(value, out _);
         }, message);
      }

      /// <summary>
      /// Validates if the property value is a valid email address.
      /// </summary>
      public static RuleBuilder<T, string> IsValidEmailAddress<T>(this RuleBuilder<T, string> ruleBuilder) where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsValidEmailAddress(new ErrorInvalidEmail(attributeName));
      }

      /// <summary>
      /// Validates if the property value is a valid email address with a custom message.
      /// </summary>
      public static RuleBuilder<T, string> IsValidEmailAddress<T>(this RuleBuilder<T, string> ruleBuilder, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var email = ruleBuilder.GetAttributeValue(instance)?.ToString();
            if (string.IsNullOrEmpty(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$");
         }, message);
      }

      /// <summary>
      /// Validates the maximum character length.
      /// </summary>
      public static RuleBuilder<T, string> MaxLength<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength) where T : class
      {
         return ruleBuilder.MaxLength(maxLength, new ErrorMoreCharactersThanExpected(ruleBuilder.GetAttributeName(), maxLength));
      }

      /// <summary>
      /// Validates the maximum character length with a custom message.
      /// </summary>
      public static RuleBuilder<T, string> MaxLength<T>(this RuleBuilder<T, string> ruleBuilder, int maxLength, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance)?.ToString();
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
         return ruleBuilder.MinLength(minLength, new ErrorFewerCharactersThanExpected(ruleBuilder.GetAttributeName(), minLength));
      }

      /// <summary>
      /// Validates the minimum character length with a custom message.
      /// </summary>
      public static RuleBuilder<T, string> MinLength<T>(this RuleBuilder<T, string> ruleBuilder, int minLength, ErrorMessage message) where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance)?.ToString();
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
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