using Myce.Response.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Myce.FluentValidator
{
   public class FluentValidator<T> : IFluentValidator<T> where T : class
   {
      private readonly List<Func<T, bool>> _globalRules = new();
      private readonly List<FluentValidatorError> _globalErrorMessages = new();

      public List<ErrorMessage> Messages => _globalErrorMessages
                                                .Where(x => x.ErrorFound)
                                                .Select(x => x.Message)
                                                .ToList();

      public RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute)
      {
         return new RuleBuilder<T, TProperty>(this, attribute);
      }

      public bool Validate(T instance)
      {
         var isValid = true;
         foreach (var error in _globalErrorMessages) error.ErrorFound = false;

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

      /// <summary>
      /// Internal method to register rules. Not visible via IFluentValidator interface.
      /// </summary>
      internal void AddRule(Func<T, bool> rule, ErrorMessage errorMessage)
      {
         _globalRules.Add(rule);
         _globalErrorMessages.Add(new FluentValidatorError(errorMessage, false));
      }
   }
}