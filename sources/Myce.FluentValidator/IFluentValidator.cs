using Myce.Response.Messages;
using System.Linq.Expressions;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Defines the contract for an entity validator.
   /// </summary>
   /// <typeparam name="T">The type of the entity to validate.</typeparam>
   public interface IFluentValidator<T> where T : class
   {
      /// <summary>
      /// Returns the list of error messages from the last validation.
      /// </summary>
      List<ErrorMessage> Messages { get; }

      /// <summary>
      /// Starts the fluent rule definition for a specific property.
      /// </summary>
      RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute);

      /// <summary>
      /// Validates an instance against all registered rules.
      /// </summary>
      /// <param name="instance">The object to validate.</param>
      /// <returns>True if all rules pass, otherwise false.</returns>
      bool Validate(T instance);
   }
}

