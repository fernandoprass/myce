using Myce.Response.Messages;
using System;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for RuleBuilder with numeric attributes (int, double, decimal).
   /// </summary>
   public static partial class RuleBuilderNumericExtensions
   {
      #region IntExtensions

      /// <summary> Validates that the integer value is greater than a specified value. </summary>
      public static RuleBuilder<T, int> IsGreaterThan<T>(this RuleBuilder<T, int> rb, int value) where T : class
         => rb.AddNumericRule(value, (a, v) => a > v, "greater than");

      /// <summary> Validates that the nullable integer value is greater than a specified value. </summary>
      public static RuleBuilder<T, int?> IsGreaterThan<T>(this RuleBuilder<T, int?> rb, int value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a > v, "greater than");

      /// <summary> Validates that the integer value is greater than or equal to a specified value. </summary>
      public static RuleBuilder<T, int> IsGreaterThanOrEqualTo<T>(this RuleBuilder<T, int> rb, int value) where T : class
         => rb.AddNumericRule(value, (a, v) => a >= v, "greater than or equal to");

      /// <summary> Validates that the nullable integer value is greater than or equal to a specified value. </summary>
      public static RuleBuilder<T, int?> IsGreaterThanOrEqualTo<T>(this RuleBuilder<T, int?> rb, int value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a >= v, "greater than or equal to");

      /// <summary> Validates that the integer value is less than a specified value. </summary>
      public static RuleBuilder<T, int> IsLessThan<T>(this RuleBuilder<T, int> rb, int value) where T : class
         => rb.AddNumericRule(value, (a, v) => a < v, "less than");

      /// <summary> Validates that the nullable integer value is less than a specified value. </summary>
      public static RuleBuilder<T, int?> IsLessThan<T>(this RuleBuilder<T, int?> rb, int value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a < v, "less than");

      /// <summary> Validates that the integer value is less than or equal to a specified value. </summary>
      public static RuleBuilder<T, int> IsLessThanOrEqualTo<T>(this RuleBuilder<T, int> rb, int value) where T : class
         => rb.AddNumericRule(value, (a, v) => a <= v, "less than or equal to");

      /// <summary> Validates that the nullable integer value is less than or equal to a specified value. </summary>
      public static RuleBuilder<T, int?> IsLessThanOrEqualTo<T>(this RuleBuilder<T, int?> rb, int value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a <= v, "less than or equal to");

      #endregion

      #region DoubleExtensions

      /// <summary> Validates that the double value is greater than a specified value. </summary>
      public static RuleBuilder<T, double> IsGreaterThan<T>(this RuleBuilder<T, double> rb, double value) where T : class
         => rb.AddNumericRule(value, (a, v) => a > v, "greater than");

      /// <summary> Validates that the nullable double value is greater than a specified value. </summary>
      public static RuleBuilder<T, double?> IsGreaterThan<T>(this RuleBuilder<T, double?> rb, double value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a > v, "greater than");

      /// <summary> Validates that the double value is greater than or equal to a specified value. </summary>
      public static RuleBuilder<T, double> IsGreaterThanOrEqualTo<T>(this RuleBuilder<T, double> rb, double value) where T : class
         => rb.AddNumericRule(value, (a, v) => a >= v, "greater than or equal to");

      /// <summary> Validates that the nullable double value is greater than or equal to a specified value. </summary>
      public static RuleBuilder<T, double?> IsGreaterThanOrEqualTo<T>(this RuleBuilder<T, double?> rb, double value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a >= v, "greater than or equal to");

      /// <summary> Validates that the double value is less than a specified value. </summary>
      public static RuleBuilder<T, double> IsLessThan<T>(this RuleBuilder<T, double> rb, double value) where T : class
         => rb.AddNumericRule(value, (a, v) => a < v, "less than");

      /// <summary> Validates that the nullable double value is less than a specified value. </summary>
      public static RuleBuilder<T, double?> IsLessThan<T>(this RuleBuilder<T, double?> rb, double value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a < v, "less than");

      /// <summary> Validates that the double value is less than or equal to a specified value. </summary>
      public static RuleBuilder<T, double> IsLessThanOrEqualTo<T>(this RuleBuilder<T, double> rb, double value) where T : class
         => rb.AddNumericRule(value, (a, v) => a <= v, "less than or equal to");

      /// <summary> Validates that the nullable double value is less than or equal to a specified value. </summary>
      public static RuleBuilder<T, double?> IsLessThanOrEqualTo<T>(this RuleBuilder<T, double?> rb, double value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a <= v, "less than or equal to");

      #endregion

      #region DecimalExtensions

      /// <summary> Validates that the decimal value is greater than a specified value. </summary>
      public static RuleBuilder<T, decimal> IsGreaterThan<T>(this RuleBuilder<T, decimal> rb, decimal value) where T : class
         => rb.AddNumericRule(value, (a, v) => a > v, "greater than");

      /// <summary> Validates that the nullable decimal value is greater than a specified value. </summary>
      public static RuleBuilder<T, decimal?> IsGreaterThan<T>(this RuleBuilder<T, decimal?> rb, decimal value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a > v, "greater than");

      /// <summary> Validates that the decimal value is greater than or equal to a specified value. </summary>
      public static RuleBuilder<T, decimal> IsGreaterThanOrEqualTo<T>(this RuleBuilder<T, decimal> rb, decimal value) where T : class
         => rb.AddNumericRule(value, (a, v) => a >= v, "greater than or equal to");

      /// <summary> Validates that the nullable decimal value is greater than or equal to a specified value. </summary>
      public static RuleBuilder<T, decimal?> IsGreaterThanOrEqualTo<T>(this RuleBuilder<T, decimal?> rb, decimal value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a >= v, "greater than or equal to");

      /// <summary> Validates that the decimal value is less than a specified value. </summary>
      public static RuleBuilder<T, decimal> IsLessThan<T>(this RuleBuilder<T, decimal> rb, decimal value) where T : class
         => rb.AddNumericRule(value, (a, v) => a < v, "less than");

      /// <summary> Validates that the nullable decimal value is less than a specified value. </summary>
      public static RuleBuilder<T, decimal?> IsLessThan<T>(this RuleBuilder<T, decimal?> rb, decimal value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a < v, "less than");

      /// <summary> Validates that the decimal value is less than or equal to a specified value. </summary>
      public static RuleBuilder<T, decimal> IsLessThanOrEqualTo<T>(this RuleBuilder<T, decimal> rb, decimal value) where T : class
         => rb.AddNumericRule(value, (a, v) => a <= v, "less than or equal to");

      /// <summary> Validates that the nullable decimal value is less than or equal to a specified value. </summary>
      public static RuleBuilder<T, decimal?> IsLessThanOrEqualTo<T>(this RuleBuilder<T, decimal?> rb, decimal value) where T : class
         => rb.AddNullableNumericRule(value, (a, v) => a <= v, "less than or equal to");

      #endregion

      #region PrivateHelpersForCodeReuse

      /// <summary>
      /// Internal helper to add rules for non-nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute> AddNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         string label)
         where T : class
         where TAttribute : struct, IComparable
      {
         var name = rb.GetAttributeName();
         return rb.AddRule(instance => compare((TAttribute)rb.GetAttributeValue(instance), value),
            new ErrorMessage($"'{name}' must be {label} {value}."));
      }

      /// <summary>
      /// Internal helper to add rules for nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute?> AddNullableNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute?> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         string label)
         where T : class
         where TAttribute : struct, IComparable
      {
         var name = rb.GetAttributeName();
         return rb.AddRule(instance =>
         {
            var attr = (TAttribute?)rb.GetAttributeValue(instance);
            return attr.HasValue && compare(attr.Value, value);
         }, new ErrorMessage($"'{name}' must be {label} {value}."));
      }

      #endregion
   }
}