using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for RuleBuilder for requirement and nullability validations.
   /// </summary>
   public static class RuleBuilderCommonExtensions
   {
      /// <summary>
      /// Validates the attribute using a custom logic function.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="rule">The custom boolean rule function to execute.</param>
      /// <param name="message">The error message if the custom rule fails.</param>
      public static RuleBuilder<T, TAttribute> Custom<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Func<TAttribute, bool> rule,
         ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = (TAttribute)ruleBuilder.GetAttributeValueAsObject(instance)!;

            return rule(value);
         }, message);
      }

      /// <summary>
      /// Validates if the property value is not null.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsNotNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName(); 
         return ruleBuilder.IsNotNull(new ErrorMessage($"'{attributeName}' is null."));
      }

      /// <summary>
      /// Validates if the property value is not null.
      /// </summary>
      /// <param name="message">The error message if the custom rule fails.</param>
      public static RuleBuilder<T, TAttribute> IsNotNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, ErrorMessage message)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValueAsObject(instance);
            return attrValue is not null;
         }, message);
      }

      /// <summary>
      /// Validates if the property value is null.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsNull(new ErrorMessage($"'{attributeName}' is not null."));
      }

      /// <summary>
      /// Validates if the property value is null.
      /// </summary>
      /// <param name="message">The error message if the custom rule fails.</param>
      public static RuleBuilder<T, TAttribute> IsNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, ErrorMessage message)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValueAsObject(instance);
            return attrValue is null;
         }, message);
      }

      /// <summary>
      /// Determines whether a value was filled (not null and not empty/whitespace if string).
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequired<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         return ruleBuilder.IsRequired(new ErrorIsRequired(ruleBuilder.GetAttributeName()));
      }

      /// <summary>
      /// Determines whether a value was filled with a custom message.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequired<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValueAsObject(instance);
            if (value is null) return false;

            // Check for empty strings or whitespace
            if (value is string s) return !string.IsNullOrWhiteSpace(s);

            return true;
         }, message);
      }

      /// <summary>
      /// Determines whether the property is required if a given condition is true.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequiredIf<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, bool condition)
         where T : class
      {
         return condition ? ruleBuilder.IsRequired() : ruleBuilder;
      }

      /// <summary>
      /// Determines whether the property is required if a condition is true with a custom message.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequiredIf<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, bool condition, ErrorMessage message)
         where T : class
      {
         return condition ? ruleBuilder.IsRequired(message) : ruleBuilder;
      }

      /// <summary>
      /// Validates that the boolean attribute is true.
      /// </summary>
      public static RuleBuilder<T, bool> IsTrue<T>(this RuleBuilder<T, bool> ruleBuilder, ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            return ruleBuilder.GetAttributeValue(instance);
         }, message);
      }

      /// <summary>
      /// Validates that the boolean attribute is false.
      /// </summary>
      public static RuleBuilder<T, bool> IsFalse<T>(this RuleBuilder<T, bool> ruleBuilder, ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            return !ruleBuilder.GetAttributeValue(instance);
         }, message);
      }
   }
}