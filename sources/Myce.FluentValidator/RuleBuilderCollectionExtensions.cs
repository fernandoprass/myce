using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for <see cref="RuleBuilder{T, TProperty}"/> providing validation for Collections.
   /// </summary>
   public static class RuleBuilderCollectionExtensions
   {
      /// <summary>
      /// Validates that the collection is empty (contains no items).
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      public static RuleBuilder<T, TProperty> IsEmpty<T, TProperty, TElement>(this RuleBuilder<T, TProperty> rb)
          where T : class
          where TProperty : IEnumerable<TElement>
      {
         var attributeName = rb.GetAttributeName();
         return rb.IsEmpty<T, TProperty, TElement>(new IsNotEmptyError(attributeName));
      }

      /// <summary>
      /// Validates that the collection is empty using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TProperty> IsEmpty<T, TProperty, TElement>(this RuleBuilder<T, TProperty> rb, ErrorMessage message)
          where T : class
          where TProperty : IEnumerable<TElement>
      {
         return rb.AddRule(instance =>
         {
            var collection = rb.GetAttributeValue(instance);
            // A null collection is considered empty by convention in many frameworks.
            return collection == null || !collection.Any();
         }, message);
      }


      /// <summary>
      /// Validates that the attribute value is present within a sequence of allowed allowedValues using a default message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      public static RuleBuilder<T, TAttribute> IsIn<T, TAttribute>(
          this RuleBuilder<T, TAttribute> ruleBuilder,
          IEnumerable<TAttribute> values)
          where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.IsIn(values, new ContainsInvalidValueError(attributeName));
      }

      /// <summary>
      /// Validates that the attribute value is present within a sequence of allowed valeus.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
      /// <param name="ruleBuilder">The rule builder instance.</param>
      /// <param name="allowedValues">The sequence of allowed values.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TAttribute> IsIn<T, TAttribute>(
          this RuleBuilder<T, TAttribute> ruleBuilder,
          IEnumerable<TAttribute> allowedValues,
          ErrorMessage message)
          where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            // Returns false if the property is null, otherwise checks if it's in the collection
            return value is not null && allowedValues.Contains(value);
         }, message);
      }

      /// <summary>
      /// Validates that the collection is not empty and has at least one item.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      public static RuleBuilder<T, TProperty> HasItems<T, TProperty, TElement>(this RuleBuilder<T, TProperty> rb)
         where T : class
         where TProperty : IEnumerable<TElement>
      {
         var attributeName = rb.GetAttributeName();
         return rb.HasItems<T, TProperty, TElement>(new IsNotEmptyError(attributeName));
      }

      /// <summary>
      /// Validates that the collection is not empty and has at least one item using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TProperty> HasItems<T, TProperty, TElement>(this RuleBuilder<T, TProperty> rb, ErrorMessage message)
         where T : class
         where TProperty : IEnumerable<TElement>
      {
         return rb.AddRule(instance =>
         {
            var collection = rb.GetAttributeValue(instance);
            return collection != null && collection.Any();
         }, message);
      }

      /// <summary>
      /// Validates that the collection does not exceed a maximum number of items.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      /// <param name="max">The maximum numbers of items allowed.</param>
      public static RuleBuilder<T, TProperty> MaxNumberOfItems<T, TProperty, TElement>(this RuleBuilder<T, TProperty> rb, int max)
         where T : class
         where TProperty : IEnumerable<TElement>
      {
         var attributeName = rb.GetAttributeName();
         return rb.MaxNumberOfItems<T, TProperty, TElement>(max, new MaxNumberOfItemsError(attributeName, max));
      }

      /// <summary>
      /// Validates that the collection does not exceed a maximum number of items using a custom error message.
      /// </summary>
      /// <typeparam name="T">The type of the entity being validated.</typeparam>
      /// <typeparam name="TProperty">The collection type (must implement IEnumerable).</typeparam>
      /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
      /// <param name="max">The maximum numbers of items allowed.</param>
      /// <param name="message">The custom error message to return if the validation fails.</param>
      public static RuleBuilder<T, TProperty> MaxNumberOfItems<T, TProperty, TElement>(this RuleBuilder<T, TProperty> rb, int max, ErrorMessage message)
         where T : class
         where TProperty : IEnumerable<TElement>
      {
         return rb.AddRule(instance =>
         {
            var collection = rb.GetAttributeValue(instance);
            return collection == null || collection.Count() <= max;
         }, message);
      }
   }
}