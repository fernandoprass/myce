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
      private Func<Func<T, bool>, Func<T, bool>> _ruleWrapper = rule => rule;

      /// <summary>
      /// The list of error messages corresponding to the rules that failed during validation. 
      /// This property is populated after calling the Validate method, and it will contain only the
      /// messages for the rules that did not pass. Each message provides details about the specific
      /// validation failure, allowing you to understand what went wrong with the validated instance.
      /// </summary>
      public List<ErrorMessage> Messages => _globalErrorMessages
                                                .Where(x => x.ErrorFound)
                                                .Select(x => x.Message)
                                                .ToList();

      /// <summary>
      /// Registers a validation rule for a specific property of the object being validated. 
      /// The provided expression should point to the property you want to validate.
      /// </summary>
      /// <typeparam name="TProperty"></typeparam>
      /// <param name="attribute"></param>
      /// <returns></returns>
      public RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute)
      {
         return new RuleBuilder<T, TProperty>(this, attribute);
      }

      /// <summary>
      /// Starts a validation rule for any external object or variable.
      /// </summary>
      /// <typeparam name="TValue">The type of the value to validate.</typeparam>
      /// <param name="value">The value/variable itself.</param>
      public RuleBuilder<T, TValue> RuleForValue<TValue>(TValue value)
      {
         Expression<Func<T, TValue>> expression = _ => value;
         return new RuleBuilder<T, TValue>(this, expression, "Value");
      }

      /// <summary>
      /// Starts a validation rule for an external value or variable.
      /// </summary>
      /// <typeparam name="TValue">The type of the value to validate.</typeparam>
      /// <param name="value">The value/variable itself.</param>
      /// <param name="paramName">A descriptive name for the variable (used in error messages).</param>
      public RuleBuilder<T, TValue> RuleForValue<TValue>(TValue value, string paramName)
      {
         Expression<Func<T, TValue>> expression = _ => value;
         return new RuleBuilder<T, TValue>(this, expression, paramName);
      }

      /// <summary>
      /// Validates the given instance against all registered rules and returns true if all rules pass, otherwise false. 
      /// Any error messages for failed rules will be collected and can be accessed via the Messages property.
      /// </summary>
      /// <param name="instance">The instance of type T to validate.</param>
      /// <returns></returns>
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
         _globalRules.Add(_ruleWrapper(rule));
         _globalErrorMessages.Add(new FluentValidatorError(errorMessage, false));
      }

      internal IDisposable BeginIfScope(bool condition)
      {
         var previousWrapper = _ruleWrapper;

         _ruleWrapper = rule => instance => !condition || rule(instance);

         return new Scope(() => _ruleWrapper = previousWrapper);
      }

      internal IDisposable BeginIfScope(Func<T, bool> condition)
      {
         var parentWrapper = _ruleWrapper;

         _ruleWrapper = rule =>
         {
            Func<T, bool> conditionalRule = instance => !condition(instance) || rule(instance);

            return parentWrapper(conditionalRule);
         };

         return new Scope(() => _ruleWrapper = parentWrapper);
      }

      internal IDisposable BeginElseScope(Func<T, bool> condition) => BeginIfScope(instance => !condition(instance));

      internal IDisposable BeginElseScope(bool condition) => BeginIfScope(!condition);

      private class Scope : IDisposable
      {
         private readonly Action _onDispose;
         public Scope(Action onDispose) => _onDispose = onDispose;
         public void Dispose() => _onDispose();
      }
   }
}