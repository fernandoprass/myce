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
                              NumericErrorMessages.IsBetween(rb.GetAttributeName(), rb.FormatValue(min), rb.FormatValue(max)));

      /// <summary>
      /// Validates that the numeric value is between minimum and maximum values extracted dynamically from other fields (inclusive).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="minExpression">A function expression to extract the minimum comparison value from the instance.</param>
      /// <param name="maxExpression">A function expression to extract the maximum comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> minExpression, Func<T, TAttribute> maxExpression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            var min = minExpression(instance);
            var max = maxExpression(instance);
            return attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0;
         }, NumericErrorMessages.IsBetween(rb.GetAttributeName(), "min field", "max field"));

      /// <summary>
      /// Validates that the numeric value is between a minimum and maximum (inclusive).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="min">The minimum allowed value.</param>
      /// <param name="max">The maximum allowed value.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute min, TAttribute max, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(min, (attr, _) => attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0, message);

      /// <summary>
      /// Validates that the numeric value is between minimum and maximum values extracted dynamically from other fields (inclusive) using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="minExpression">A function expression to extract the minimum comparison value from the instance.</param>
      /// <param name="maxExpression">A function expression to extract the maximum comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> minExpression, Func<T, TAttribute> maxExpression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.CompareTo(minExpression(instance)) >= 0 && attr.CompareTo(maxExpression(instance)) <= 0;
         }, message);

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
                                      NumericErrorMessages.IsBetween(rb.GetAttributeName(), rb.FormatValue(min), rb.FormatValue(max)));

      /// <summary>
      /// Validates that the nullable numeric value is between minimum and maximum values extracted dynamically from other fields (inclusive).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="minExpression">A function expression to extract the minimum comparison value from the instance.</param>
      /// <param name="maxExpression">A function expression to extract the maximum comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> minExpression, Func<T, TAttribute> maxExpression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(minExpression(instance)) >= 0 && attr.Value.CompareTo(maxExpression(instance)) <= 0;
         }, NumericErrorMessages.IsBetween(rb.GetAttributeName(), "min field", "max field"));

      /// <summary>
      /// Validates that the nullable numeric value is between a minimum and maximum (inclusive).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="min">The minimum allowed value.</param>
      /// <param name="max">The maximum allowed value.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute min, TAttribute max, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(min, (attr, _) => attr.CompareTo(min) >= 0 && attr.CompareTo(max) <= 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is between minimum and maximum values extracted dynamically from other fields (inclusive) using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="minExpression">A function expression to extract the minimum comparison value from the instance.</param>
      /// <param name="maxExpression">A function expression to extract the maximum comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsBetween<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> minExpression, Func<T, TAttribute> maxExpression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(minExpression(instance)) >= 0 && attr.Value.CompareTo(maxExpression(instance)) <= 0;
         }, message);

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, NumericErrorMessages.IsGreaterThan(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the numeric value is greater than the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) > 0,
                       NumericErrorMessages.IsGreaterThan(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the numeric value is greater than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, message);

      /// <summary>
      /// Validates that the numeric value is greater than the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) > 0, message);

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, NumericErrorMessages.IsGreaterThan(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the nullable numeric value is greater than the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) > 0;
         }, NumericErrorMessages.IsGreaterThan(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the nullable numeric value is greater than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) > 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is greater than the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) > 0;
         }, message);

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, NumericErrorMessages.IsGreaterThanOrEqualTo(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the numeric value is greater than or equal to the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) >= 0,
                       NumericErrorMessages.IsGreaterThanOrEqualTo(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the numeric value is greater than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, message);

      /// <summary>
      /// Validates that the numeric value is greater than or equal to the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) >= 0, message);

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, NumericErrorMessages.IsGreaterThanOrEqualTo(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the nullable numeric value is greater than or equal to the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) >= 0;
         }, NumericErrorMessages.IsGreaterThanOrEqualTo(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the nullable numeric value is greater than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param> 
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) >= 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is greater than or equal to the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsGreaterThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) >= 0;
         }, message);

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, NumericErrorMessages.IsLessThan(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the numeric value is less than the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) < 0,
                       NumericErrorMessages.IsLessThan(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the numeric value is less than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, message);

      /// <summary>
      /// Validates that the numeric value is less than the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) < 0, message);

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, NumericErrorMessages.IsLessThan(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the nullable numeric value is less than the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) < 0;
         }, NumericErrorMessages.IsLessThan(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the nullable numeric value is less than a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param> 
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) < 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is less than the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThan<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) < 0;
         }, message);

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
         => rb.AddNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, NumericErrorMessages.IsLessThanOrEqualTo(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the numeric value is less than or equal to the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) <= 0,
                       NumericErrorMessages.IsLessThanOrEqualTo(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the numeric value is less than or equal to the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance => rb.GetAttributeValue(instance).CompareTo(expression(instance)) <= 0, message);

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
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, NumericErrorMessages.IsLessThanOrEqualTo(rb.GetAttributeName(), rb.FormatValue(value)));

      /// <summary>
      /// Validates that the nullable numeric value is less than or equal to the value of another property.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) <= 0;
         }, NumericErrorMessages.IsLessThanOrEqualTo(rb.GetAttributeName(), "the other field"));

      /// <summary>
      /// Validates that the nullable numeric value is less than or equal to a specified value.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="value">The value to compare against.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, TAttribute value, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(value, (attr, val) => attr.CompareTo(val) <= 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is less than or equal to the value of another property using a custom message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expression">A function expression to extract the comparison value from the instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsLessThanOrEqualTo<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Func<T, TAttribute> expression, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddRule(instance =>
         {
            var attr = rb.GetAttributeValue(instance);
            return attr.HasValue && attr.Value.CompareTo(expression(instance)) <= 0;
         }, message);

      /// <summary>
      /// Validates that the numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, NumericErrorMessages.IsPositive(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, NumericErrorMessages.IsPositive(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the nullable numeric value is positive (greater than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsPositive<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) > 0, message);

      /// <summary>
      /// Validates that the numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, NumericErrorMessages.IsNegative(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, message);

      /// <summary>
      /// Validates that the nullable numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, NumericErrorMessages.IsNegative(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the nullable numeric value is negative (less than zero).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The numeric struct type implementing IComparable.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom message to use if validation fails.</param>
      /// <returns>The rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute?> IsNegative<T, TAttribute>(this RuleBuilder<T, TAttribute?> rb, Message message)
         where T : class where TAttribute : struct, IComparable<TAttribute>
         => rb.AddNullableNumericRule(default, (attr, val) => attr.CompareTo(val) < 0, message);
      #endregion

      #region PrivateHelpersForCodeReuse

      /// <summary>
      /// Internal helper to add rules for non-nullable numeric types.
      /// </summary>
      private static RuleBuilder<T, TAttribute> AddNumericRule<T, TAttribute>(
         this RuleBuilder<T, TAttribute> rb,
         TAttribute value,
         Func<TAttribute, TAttribute, bool> compare,
         Message message)
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
         Message message)
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
