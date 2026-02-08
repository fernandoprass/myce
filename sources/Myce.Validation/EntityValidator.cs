using Myce.Response.Messages;
using System.Linq.Expressions;

namespace Myce.Validation
{

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
      private readonly List<string> _errorMessages = new();


      public List<ErrorMessage> Messages 
      { 
         get { return _globalErrorMessages.Where(x => x.ErrorFound).Select(x => x.Message).ToList(); } 
      }

      public RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute)
      {
         return new RuleBuilder<T, TProperty>(this, attribute);
      }

      public EntityValidator<T> AddRule(Func<T, bool> rule, ErrorMessage errorMessage)
      {
         _globalRules.Add(rule);
         _globalErrorMessages.Add(new ValidatorErrors(errorMessage, false));
         return this;
      }

      public bool Validate(T instance)
      {
         var isValid = true;
         _errorMessages.Clear();

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
