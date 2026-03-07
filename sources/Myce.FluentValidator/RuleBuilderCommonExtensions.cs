using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for common validation rules that can be applied to any property type, 
   /// such as null checks, required fields, and boolean validations.
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
      /// Validates the property using a pre-calculated boolean value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="condition">The result of a custom validation performed outside the builder.</param>
      /// <param name="message">The error message if the condition is false.</param>
      public static RuleBuilder<T, TAttribute> Custom<T, TAttribute>(
          this RuleBuilder<T, TAttribute> ruleBuilder,
          bool condition,
          ErrorMessage message)
          where T : class
      {
         return ruleBuilder.AddRule(_ => condition, message);
      }

      /// <summary>
      /// Validates if the property value is not null.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsNotNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName(); 
         return ruleBuilder.IsNotNull(new IsNullError(attributeName));
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
         return ruleBuilder.IsNull(new IsNotNullError(attributeName));
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
         return ruleBuilder.IsRequired(new IsRequiredError(ruleBuilder.GetAttributeName()));
      }

      /// <summary>
      /// Determines whether a value was filled with a custom message.
      /// </summary>
      /// <param name="message">The error message if the IsRequired rule fails.</param>
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
      /// <param name="condition">The condition that determines if the property is required.</param>
      public static RuleBuilder<T, TAttribute> IsRequiredIf<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, bool condition)
         where T : class
      {
         return condition ? ruleBuilder.IsRequired() : ruleBuilder;
      }

      /// <summary>
      /// Determines whether the property is required if a condition is true with a custom message.
      /// </summary>
      /// <param name="message">The error message if the IsRequiredIf rule fails.</param>
      public static RuleBuilder<T, TAttribute> IsRequiredIf<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, bool condition, ErrorMessage message)
         where T : class
      {
         return condition ? ruleBuilder.IsRequired(message) : ruleBuilder;
      }

      /// <summary>
      /// Validates that the boolean attribute is true.
      /// </summary>
      public static RuleBuilder<T, bool> IsTrue<T>(this RuleBuilder<T, bool> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsTrue(new IsFalseError(attributeName));
      }

      /// <summary>
      /// Validates that the boolean attribute is true.
      /// </summary>
      /// <param name="message">The error message if the IsTrue rule fails.</param>
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
      public static RuleBuilder<T, bool> IsFalse<T>(this RuleBuilder<T, bool> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsFalse(new IsTrueError(attributeName));
      }

      /// <summary>
      /// Validates that the boolean attribute is false.
      /// </summary>
      /// <param name="message">The error message if the IsFalse rule fails.</param>
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