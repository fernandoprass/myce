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
      //todo create validation for enum types (IsInEnum, IsNotInEnum) 
      //bool isValidType = Enum.IsDefined(typeof(CustomerType), request.Type);

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
         => rb.AddNumericRule(min, (attr, _) => attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0, $"between {min} and {max}");

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
         => rb.AddNullableNumericRule(min, (attr, _) => attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0, $"between {min} and {max}");

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, $"greater than {value}");

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, $"greater than {value}");

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, $"greater than or equal to {value}");

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, $"greater than or equal to {value}");

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, $"less than {value}");

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, $"less than {value}");

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, $"less than or equal to {value}");

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, $"less than or equal to {value}");

      /// <summary>
      /// Validates that the numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, "positive");

      /// <summary>
      /// Validates that the nullable numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, "positive");

      /// <summary>
      /// Validates that the numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, "negative");

      /// <summary>
      /// Validates that the nullable numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, "negative");

      #endregion

      #region PrivateHelpersForCodeReuse

      /// <summary>
      /// Internal helper to add rules for non-nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute> AddNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         string messageSuffix)
         where T : class
         where TAttribute : struct, IComparable<TAttribute>
      {
         var name = rb.GetAttributeName();
         return rb.AddRule(instance => compare(rb.GetAttributeValue(instance), value),
            new ErrorMessage($"'{name}' must be {messageSuffix}."));
      }

      /// <summary>
      /// Internal helper to add rules for nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute?> AddNullableNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute?> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         string messageSuffix)
         where T : class
         where TAttribute : struct, IComparable<TAttribute>
      {
         var name = rb.GetAttributeName();
         return rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && compare(attr.Value, value);
         }, new ErrorMessage($"'{name}' must be {messageSuffix}."));
      }

      #endregion
   }
}