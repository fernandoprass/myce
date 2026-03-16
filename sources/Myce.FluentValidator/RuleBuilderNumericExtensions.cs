using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for RuleBuilder with numeric attributes.
   /// Unified generic version supporting any numeric type (int, double, decimal, long, byte, etc.).
   /// </summary>
   public static partial class RuleBuilderNumericExtensions
   {
      #region GenericNumericExtensions

      /// <summary>
      /// Validates that the numeric value is between a minimum and maximum (inclusive).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="min">The minimum allowed value.</param>
      /// <param name="max">The maximum allowed value.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute min, TAttribute max)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(min, (attr, _) => attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0,
                              new IsBetweenError(rb.GetAttributeName(), min.ToString(), max.ToString()));

      /// <summary>
      /// Validates that the nullable numeric value is between a minimum and maximum (inclusive).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="min">The minimum allowed value.</param>
      /// <param name="max">The maximum allowed value.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute min, TAttribute max)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(min, (attr, _) => attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0,
                                      new IsBetweenError(rb.GetAttributeName(), min.ToString(), max.ToString()));

      /// <summary>
      /// Validates that the numeric value is greater than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, new IsGreaterThanError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the nullable numeric value is greater than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, new IsGreaterThanError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the numeric value is greater than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, new IsGreaterThanOrEqualToError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the nullable numeric value is greater than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, new IsGreaterThanOrEqualToError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the numeric value is less than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, new IsLessThanError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the nullable numeric value is less than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, new IsLessThanError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the numeric value is less than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, new IsLessThanOrEqualToError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the nullable numeric value is less than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, new IsLessThanOrEqualToError(rb.GetAttributeName(), value.ToString()));

      /// <summary>
      /// Validates that the numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, new IsPositiveError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the nullable numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, new IsPositiveError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, new IsNegativeError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the nullable numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, new IsNegativeError(rb.GetAttributeName()));

      #endregion

      #region PrivateHelpersForCodeReuse

      /// <summary>
      /// Internal helper to add rules for non-nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute> AddNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         ErrorMessage message)
         where T : class
         where TAttribute : struct, IComparable<TAttribute>
      {
         var attributeName = rb.GetAttributeName();
         return rb.AddRule(instance => compare(rb.GetAttributeValue(instance), value), message);
      }

      /// <summary>
      /// Internal helper to add rules for nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute?> AddNullableNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute?> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         ErrorMessage message)
         where T : class
         where TAttribute : struct, IComparable<TAttribute>
      {
         var attributeName = rb.GetAttributeName();
         return rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && compare(attr.Value, value);
         }, message);
      }

      #endregion
   }
}