using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for <see cref="RuleBuilder{T, TProperty}"/> providing specialized validation for <see cref="DateTime"/> properties.
   /// </summary>
   public static partial class RuleBuilderDateExtensions
   {
      #region Relative Dates (Today, Yesterday, Tomorrow)

      /// <summary>
      /// Validates that the property value represents today's date, ignoring the time component.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsToday<T>(this RuleBuilder<T, DateTime> rb) where T : class
         => rb.IsToday(new IsTodayError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the property value represents today's date, ignoring the time component, using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsToday<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
         => rb.AddRule(instance => rb.GetAttributeValue(instance).Date == DateTime.Today, message);

      /// <summary>
      /// Validates that the nullable property value is either null or represents today's date (ignoring time).
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime?}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime?> IsToday<T>(this RuleBuilder<T, DateTime?> rb) where T : class
         => rb.IsToday(new IsTodayError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the nullable property value is either null or represents today's date (ignoring time) using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime?}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime?> IsToday<T>(this RuleBuilder<T, DateTime?> rb, ErrorMessage message) where T : class
         => rb.AddRule(instance => {
            var val = rb.GetAttributeValue(instance);
            return !val.HasValue || val.Value.Date == DateTime.Today;
         }, message);

      /// <summary>
      /// Validates that the property value represents yesterday's date, ignoring the time component.
      /// </summary>
      public static RuleBuilder<T, DateTime> IsYesterday<T>(this RuleBuilder<T, DateTime> rb) where T : class
         => rb.IsYesterday(new IsYesterdayError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the property value represents yesterday's date, ignoring the time component, using a custom error message.
      /// </summary>
      public static RuleBuilder<T, DateTime> IsYesterday<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
         => rb.AddRule(instance => rb.GetAttributeValue(instance).Date == DateTime.Today.AddDays(-1), message);

      /// <summary>
      /// Validates that the property value represents tomorrow's date, ignoring the time component.
      /// </summary>
      public static RuleBuilder<T, DateTime> IsTomorrow<T>(this RuleBuilder<T, DateTime> rb) where T : class
         => rb.IsTomorrow(new IsTomorrowError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the property value represents tomorrow's date, ignoring the time component, using a custom error message.
      /// </summary>
      public static RuleBuilder<T, DateTime> IsTomorrow<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
         => rb.AddRule(instance => rb.GetAttributeValue(instance).Date == DateTime.Today.AddDays(1), message);

      #endregion

      #region Logical Range Checks (Past, Future)

      /// <summary>
      /// Validates that the property value is a date and time chronologically later than the current moment.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsInTheFuture<T>(this RuleBuilder<T, DateTime> rb) where T : class
         => rb.IsInTheFuture(new IsInTheFutureError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the property value is a date and time chronologically later than the current moment using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsInTheFuture<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
         => rb.AddRule(instance => rb.GetAttributeValue(instance) > DateTime.Now, message);

      /// <summary>
      /// Validates that the property value is a date and time chronologically earlier than the current moment.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsInThePast<T>(this RuleBuilder<T, DateTime> rb) where T : class
         => rb.IsInThePast(new IsInThePastError(rb.GetAttributeName()));

      /// <summary>
      /// Validates that the property value is a date and time chronologically earlier than the current moment using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsInThePast<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
         => rb.AddRule(instance => rb.GetAttributeValue(instance) < DateTime.Now, message);

      #endregion

      #region Calendar Based Checks (Weekend, Weekday)

      /// <summary>
      /// Validates that the property value falls on a weekend (Saturday or Sunday).
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsWeekend<T>(this RuleBuilder<T, DateTime> rb) where T : class
      {
         return rb.IsWeekend(new IsWeekendError(rb.GetAttributeName()));
      }

      /// <summary>
      /// Validates that the property value falls on a weekend (Saturday or Sunday) using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsWeekend<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
      {
         return rb.AddRule(instance => {
            var date = rb.GetAttributeValue(instance);
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
         }, message);
      }

      /// <summary>
      /// Validates that the property value falls on a weekday (Monday through Friday).
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsWeekday<T>(this RuleBuilder<T, DateTime> rb) where T : class
      {
         return rb.IsWeekday(new IsWeekdayError(rb.GetAttributeName()));
      }

      /// <summary>
      /// Validates that the property value falls on a weekday (Monday through Friday) using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      /// <returns>The same <see cref="RuleBuilder{T, DateTime}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, DateTime> IsWeekday<T>(this RuleBuilder<T, DateTime> rb, ErrorMessage message) where T : class
      {
         return rb.AddRule(instance => {
            var date = rb.GetAttributeValue(instance);
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
         }, message);
      }

      #endregion
   }
}