using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;
using System.Linq.Expressions;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for comparison-based validation rules, such as equality 
   /// and inequality checks against fixed values or other properties.
   /// </summary>
   public static class RuleBuilderComparisonExtensions
   {
      /// <summary>
      /// Validates equality to a fixed value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      public static RuleBuilder<T, TAttribute> IsEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, TAttribute value)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsEqualTo(value, new MustBeEqualError(attributeName, value?.ToString()));
      }

      /// <summary>
      /// Validates equality to a fixed value using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, TAttribute value, ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            if (attrValue is null && value is null) return true;

            return attrValue is not null && attrValue.Equals(value);
         }, message);
      }

      /// <summary>
      /// Validates if the property value is equal to another property value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="comparisonProperty">Expression representing the property to compare with.</param>
      public static RuleBuilder<T, TAttribute> IsEqualTo<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Expression<Func<T, TAttribute>> comparisonProperty)
         where T : class
      {
         return ruleBuilder.EqualityCompare(comparisonProperty, true, (attributeName, comparisonName) => new ComparisonError(attributeName, "be equal to", comparisonName));
      }

      /// <summary>
      /// Validates if the property value is equal to another property value using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="comparisonProperty">Expression representing the property to compare with.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsEqualTo<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Expression<Func<T, TAttribute>> comparisonProperty,
         ErrorMessage message)
         where T : class
      {
         return ruleBuilder.EqualityCompare(comparisonProperty, true, (_, _) => message);
      }

      /// <summary>
      /// Validates if the property value is not equal to a fixed value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      public static RuleBuilder<T, TAttribute> IsNotEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, TAttribute value)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsNotEqualTo(value, new MustNotBeEqualError(attributeName, value?.ToString()));
      }

      /// <summary>
      /// Validates if the property value is not equal to a fixed value using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsNotEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, TAttribute value, ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            if (attrValue is null && value is null) return false;
            return attrValue is null || !attrValue.Equals(value);
         }, message);
      }

      /// <summary>
      /// Validates if the property value is not equal to another property value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="comparisonProperty">Expression representing the property to compare with.</param>
      public static RuleBuilder<T, TAttribute> IsNotEqualTo<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Expression<Func<T, TAttribute>> comparisonProperty)
         where T : class
      {
         return ruleBuilder.EqualityCompare(comparisonProperty, false, (attributeName, comparisonName) => new ComparisonError(attributeName, "not be equal to", comparisonName));
      }

      /// <summary>
      /// Validates if the property value is not equal to another property value using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="comparisonProperty">Expression representing the property to compare with.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsNotEqualTo<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Expression<Func<T, TAttribute>> comparisonProperty,
         ErrorMessage message)
         where T : class
      {
         return ruleBuilder.EqualityCompare(comparisonProperty, false, (_, _) => message);
      }

      /// <summary>
      /// Core logic for equality/inequality comparisons between two properties.
      /// </summary>
      private static RuleBuilder<T, TAttribute> EqualityCompare<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Expression<Func<T, TAttribute>> comparisonProperty,
         bool expected,
         Func<string, string, ErrorMessage> errorFactory)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         var (comparisonFunc, comparisonName) = GetComparisonInfo(comparisonProperty);

         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            var compValue = comparisonFunc(instance);
            bool areEqual = (attrValue is null && compValue is null) || (attrValue is not null && attrValue.Equals(compValue));
            return areEqual == expected;
         }, errorFactory(attributeName, comparisonName));
      }

      /// <summary>
      /// Compiles the comparison property expression and extracts its name.
      /// </summary>
      private static (Func<T, TAttribute> func, string name) GetComparisonInfo<T, TAttribute>(Expression<Func<T, TAttribute>> expression)
      {
         var name = (expression.Body is MemberExpression me) ? me.Member.Name : "other property";
         return (expression.Compile(), name);
      }
   }
}