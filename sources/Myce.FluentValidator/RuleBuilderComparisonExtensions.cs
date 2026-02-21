using Myce.Response.Messages;
using System;
using System.Linq.Expressions;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for RuleBuilder for equality and inequality comparisons.
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
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            if (attrValue is null && value is null) return true;
            return attrValue is not null && attrValue.Equals(value);
         }, new ErrorMessage($"'{attributeName}' must be equal to {value}."));
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
         return ruleBuilder.EqualityCompare(comparisonProperty, true, "be equal to");
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
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            if (attrValue is null && value is null) return false;
            return attrValue is null || !attrValue.Equals(value);
         }, new ErrorMessage($"'{attributeName}' must not be equal to {value}."));
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
         return ruleBuilder.EqualityCompare(comparisonProperty, false, "not be equal to");
      }

      /// <summary>
      /// Core logic for equality/inequality comparisons between two properties.
      /// </summary>
      private static RuleBuilder<T, TAttribute> EqualityCompare<T, TAttribute>(
         this RuleBuilder<T, TAttribute> ruleBuilder,
         Expression<Func<T, TAttribute>> comparisonProperty,
         bool expected,
         string label)
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
         }, new ErrorMessage($"'{attributeName}' must {label} '{comparisonName}'."));
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