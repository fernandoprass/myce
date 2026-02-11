using Myce.Response.Messages;
using System.Linq.Expressions;

namespace Myce.Validation
{
   /// <summary>
   /// Internal class to track validation rules and their state.
   /// </summary>
   internal class ValidatorErrors
   {
      public ErrorMessage Message { get; set; }
      public bool ErrorFound { get; set; }

      public ValidatorErrors(ErrorMessage message, bool show)
      {
         Message = message;
         ErrorFound = show;
      }
   }

   public class EntityValidator<T>
   {
      private readonly List<Func<T, bool>> _globalRules = new();
      private readonly List<ValidatorErrors> _globalErrorMessages = new();

      /// <summary>
      /// Returns only the messages where a validation error was found.
      /// </summary>
      public List<ErrorMessage> Messages
      {
         get { return _globalErrorMessages.Where(x => x.ErrorFound).Select(x => x.Message).ToList(); }
      }

      /// <summary>
      /// Starts the fluent rule definition for a specific property.
      /// </summary>
      public RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute)
      {
         return new RuleBuilder<T, TProperty>(this, attribute);
      }

      /// <summary>
      /// Internal method used by RuleBuilder to register new rules.
      /// </summary>
      internal EntityValidator<T> AddRule(Func<T, bool> rule, ErrorMessage errorMessage)
      {
         _globalRules.Add(rule);
         _globalErrorMessages.Add(new ValidatorErrors(errorMessage, false));
         return this;
      }

      /// <summary>
      /// Validates an instance against all registered rules.
      /// </summary>
      /// <param name="instance">The object to validate.</param>
      /// <returns>True if all rules pass, otherwise false.</returns>
      public bool Validate(T instance)
      {
         var isValid = true;

         // Reset the error state before starting a new validation
         foreach (var errorRecord in _globalErrorMessages)
         {
            errorRecord.ErrorFound = false;
         }

         for (int i = 0; i < _globalRules.Count; i++)
         {
            if (!_globalRules[i](instance))
            {
               isValid = false;
               _globalErrorMessages[i].ErrorFound = true;
            }
         }
         return isValid;
      }
   }
}