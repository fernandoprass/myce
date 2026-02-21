using Myce.Response.Messages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Orchestrates the creation of validation rules for a specific property.
   /// </summary>
   /// <typeparam name="T">The type of the entity being validated.</typeparam>
   /// <typeparam name="TAttribute">The type of the property being validated.</typeparam>
   public class RuleBuilder<T, TAttribute> where T : class
   {
      private readonly FluentValidator<T> _validator;
      private readonly Expression<Func<T, TAttribute>> _attribute;
      private readonly Func<T, TAttribute> _attributeFunc;

      /// <summary>
      /// Initializes a new instance of the RuleBuilder class.
      /// </summary>
      /// <param name="validator">The parent validator instance.</param>
      /// <param name="attribute">The expression representing the property to validate.</param>
      public RuleBuilder(FluentValidator<T> validator, Expression<Func<T, TAttribute>> attribute)
      {
         _validator = validator;
         _attribute = attribute;
         _attributeFunc = attribute.Compile();
      }

      /// <summary>
      /// Shortcut to access error messages from the parent validator.
      /// </summary>
      public List<ErrorMessage> Messages => _validator.Messages;

      /// <summary>
      /// Starts a rule for a different property, allowing fluent chaining.
      /// </summary>
      public RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute) => _validator.RuleFor(attribute);

      /// <summary>
      /// Performs validation directly from the builder.
      /// </summary>
      public bool Validate(T instance) => _validator.Validate(instance);

      /// <summary>
      /// Implicitly converts the RuleBuilder to the parent FluentValidator.
      /// </summary>
      public static implicit operator FluentValidator<T>(RuleBuilder<T, TAttribute> builder) => builder._validator;

      #region InternalAndPrivateMethods
      /// <summary>
      /// Internal helper to register rules directly into the parent validator.
      /// </summary>
      internal RuleBuilder<T, TAttribute> AddRule(Func<T, bool> rule, ErrorMessage errorMessage)
      {
         _validator.AddRule(rule, errorMessage);
         return this;
      }

      /// <summary>
      /// Extracts the property name from the attribute expression.
      /// </summary>
      internal string GetAttributeName()
      {
         if (_attribute.Body is MemberExpression me) return me.Member.Name;
         if (_attribute.Body is UnaryExpression ue && ue.Operand is MemberExpression ume) return ume.Member.Name;
         return "Unknown";
      }

      /// <summary>
      /// Executes the attribute function to get the current value.
      /// </summary>
      internal object? GetAttributeValue(T instance) => _attributeFunc(instance);

      /// <summary>
      /// Core logic for equality/inequality comparisons.
      /// </summary>
      private RuleBuilder<T, TAttribute> EqualityCompare(Expression<Func<T, TAttribute>> comparisonProperty, bool expected, string label)
      {
         var attributeName = GetAttributeName();
         var (comparisonFunc, comparisonName) = GetComparisonInfo(comparisonProperty);

         return AddRule(instance =>
         {
            var attrValue = GetAttributeValue(instance);
            var compValue = comparisonFunc(instance);
            bool areEqual = (attrValue is null && compValue is null) || (attrValue is not null && attrValue.Equals(compValue));
            return areEqual == expected;
         }, new ErrorMessage($"'{attributeName}' must {label} '{comparisonName}'."));
      }

      /// <summary>
      /// Compiles the comparison property expression and extracts its name.
      /// </summary>
      private (Func<T, TAttribute> func, string name) GetComparisonInfo(Expression<Func<T, TAttribute>> expression)
      {
         var name = (expression.Body is MemberExpression me) ? me.Member.Name : "other property";
         return (expression.Compile(), name);
      }
      #endregion
   }
}