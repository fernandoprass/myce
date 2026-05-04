using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for <see cref="RuleBuilder{T, TAttribute}"/> providing validation for Collections and Sequences.
   /// </summary>
   public static class RuleBuilderCollectionExtensions
   {
      /// <summary>
      /// Validates that all items in the collection satisfy a specified condition using a default message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of elements in the collection.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="predicate">The condition to validate each item against.</param>
      /// <returns>The updated rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> All<T, TAttribute, TElement>(
         this RuleBuilder<T, TAttribute> rb,
         Func<TElement, bool> predicate)
         where T : class
         where TAttribute : IEnumerable<TElement>
      {
         return All(rb, predicate, new ContainsInvalidValueError(rb.GetAttributeName()));
      }

      /// <summary>
      /// Validates that all items in the collection satisfy a specified condition.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of elements in the collection.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="predicate">The condition to validate each item against.</param>
      /// <param name="message">The error message to return if the validation fails.</param>
      /// <returns>The updated rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> All<T, TAttribute, TElement>(
         this RuleBuilder<T, TAttribute> rb,
         Func<TElement, bool> predicate,
         Message message)
         where T : class
         where TAttribute : IEnumerable<TElement>
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return value == null || value.All(predicate);
         }, message);
      }

      /// <summary>
      /// Validates that any item in the collection satisfy a specified condition using a default message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of elements in the collection.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="predicate">The condition to validate each item against.</param>
      /// <returns>The updated rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> Any<T, TAttribute, TElement>(
         this RuleBuilder<T, TAttribute> rb,
         Func<TElement, bool> predicate)
         where T : class
         where TAttribute : IEnumerable<TElement>
      {
         return Any(rb, predicate, new ContainsInvalidValueError(rb.GetAttributeName()));
      }

      /// <summary>
      /// Validates that any item in the collection satisfy a specified condition.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of elements in the collection.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="predicate">The condition to validate each item against.</param>
      /// <param name="message">The error message to return if the validation fails.</param>
      /// <returns>The updated rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> Any<T, TAttribute, TElement>(
         this RuleBuilder<T, TAttribute> rb,
         Func<TElement, bool> predicate,
         Message message)
         where T : class
         where TAttribute : IEnumerable<TElement>
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return value  == null ? false : value.Any(predicate);
         }, message);
      }

      /// <summary>
      ///  Validates the number of items in the collection using a custom predicate (e.g., count => count >= 5) using a default message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="expectedCount">The expected number of items in the collection.</param>
      /// <returns>The updated rule builder instance.</returns>
      public static RuleBuilder<T, TAttribute> Count<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Func<int, bool> condition) where T : class
      {
         return Count(rb, condition, new InvalidNumberOfItemsError(rb.GetAttributeName(), 0));
      }

      /// <summary>
      /// Validates the number of items in the collection using a custom predicate (e.g., count => count >= 5).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="condition">The condition to evaluate against the count of items.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> Count<T, TAttribute>(
          this RuleBuilder<T, TAttribute> rb,
          Func<int, bool> condition,
          Message message)where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);

            int totalCount = 0;
            if (value is IEnumerable collection)
            {
               // Optimization: Use ICollection.Count if available, otherwise iterate
               if (collection is ICollection col)
               {
                  totalCount = col.Count;
               }
               else
               {
                  var enumerator = collection.GetEnumerator();
                  while (enumerator.MoveNext()) totalCount++;
               }
            }

            // Evaluate the numeric condition against the total count
            return condition(totalCount);
         }, message);
      }

      /// <summary>
      /// Validates that the collection is empty (contains no items).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      public static RuleBuilder<T, TAttribute> IsEmpty<T, TAttribute>(this RuleBuilder<T, TAttribute> rb) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.IsEmpty(new IsNotEmptyError(attributeName));
      }

      /// <summary>
      /// Validates that the collection is empty using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsEmpty<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            if (value is IEnumerable collection)
            {
               var enumerator = collection.GetEnumerator();
               return !enumerator.MoveNext();
            }
            return true; // Null is considered empty by convention
         }, message);
      }

      /// <summary>
      /// Validates that the attribute value is present within a sequence of allowed values using a default message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      public static RuleBuilder<T, TAttribute> IsIn<T, TAttribute>(
          this RuleBuilder<T, TAttribute> rb,
          IEnumerable<TAttribute> values) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.IsIn(values, new ContainsInvalidValueError(attributeName));
      }

      /// <summary>
      /// Validates that the attribute value is present within a sequence of allowed values.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <param name="allowedValues">The sequence of allowed values.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsIn<T, TAttribute>(
          this RuleBuilder<T, TAttribute> rb,
          IEnumerable<TAttribute> allowedValues,
          Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            return value is not null && allowedValues.Contains(value);
         }, message);
      }

      /// <summary>
      /// Validates that the collection is not empty and has at least one item.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      public static RuleBuilder<T, TAttribute> HasItems<T, TAttribute>(this RuleBuilder<T, TAttribute> rb) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.HasItems(new IsNotEmptyError(attributeName));
      }

      /// <summary>
      /// Validates that the collection is not empty and has at least one item using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> HasItems<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            if (value is IEnumerable collection)
            {
               var enumerator = collection.GetEnumerator();
               return enumerator.MoveNext();
            }
            return false;
         }, message);
      }

      /// <summary>
      /// Validates that the collection contains no duplicate items using a default message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      public static RuleBuilder<T, TAttribute> HasNoDuplicates<T, TAttribute>(this RuleBuilder<T, TAttribute> rb) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.HasNoDuplicates(new ContainsDuplicateItemsError(attributeName));
      }

      /// <summary>
      /// Validates that the collection contains no duplicate items using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> HasNoDuplicates<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            if (value is IEnumerable collection)
            {
               var set = new HashSet<object?>();
               foreach (var item in collection)
               {
                  // If Add returns false, it means the item is already in the set (it's a duplicate)
                  if (!set.Add(item))
                  {
                     return false;
                  }
               }
            }
            return true;
         }, message);
      }

      /// <summary>
      /// Validates that the collection does not exceed a maximum number of items.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="max">The maximum numbers of items allowed.</param>
      [Obsolete("Use Count(condition) instead.")]
      public static RuleBuilder<T, TAttribute> MaxNumberOfItems<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, int max) where T : class
      {
         var attributeName = rb.GetAttributeName();
         return rb.MaxNumberOfItems(max, new MaxNumberOfItemsError(attributeName, max));
      }

      /// <summary>
      /// Validates that the collection does not exceed a maximum number of items using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The property type (must implement IEnumerable).</typeparam>
      /// <param name="max">The maximum numbers of items allowed.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      [Obsolete("Use Count(condition, message) instead.")]
      public static RuleBuilder<T, TAttribute> MaxNumberOfItems<T, TAttribute>(this RuleBuilder<T, TAttribute> rb, int max, Message message) where T : class
      {
         return rb.AddRule(instance =>
         {
            var value = rb.GetAttributeValue(instance);
            if (value is IEnumerable collection)
            {
               int count = 0;
               var enumerator = collection.GetEnumerator();
               while (enumerator.MoveNext())
               {
                  count++;
                  if (count > max) return false;
               }
               return true;
            }
            return true; // Null collections do not exceed max
         }, message);
      }
   }
}
