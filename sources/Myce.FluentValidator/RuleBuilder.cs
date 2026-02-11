using Myce.FluentValidator;
using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
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
      /// Determines whether a sequence contains a specified element.
      /// </summary>
      /// <param name="values">The sequence of allowed values.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> Contains(string[] values, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var val = GetAttributeValue(instance)?.ToString();
            return val is not null && values.Contains(val);
         }, message);
      }

      /// <summary>
      /// Validates if a string contains only numeric characters.
      /// </summary>
      public RuleBuilder<T, TAttribute> ContainsOnlyNumber() => ContainsOnlyNumber(new ErrorShouldContainOnlyNumber(GetAttributeName()));

      /// <summary>
      /// Validates if a string contains only numeric characters with a custom message.
      /// </summary>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> ContainsOnlyNumber(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var val = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(val) || val.All(char.IsNumber);
         }, message);
      }

      /// <summary>
      /// Determines whether a string has an exact character length.
      /// </summary>
      /// <param name="length">Expected number of characters.</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharacters(int length) => ExactNumberOfCharacters(length, new ErrorNotExactNumberOfCharacters(GetAttributeName(), length));

      /// <summary>
      /// Determines whether a string has an exact character length with a custom message.
      /// </summary>
      /// <param name="length">Expected number of characters.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharacters(int length, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || value.Length == length;
         }, message);
      }

      /// <summary>
      /// Determines whether a string has an exact length if a given condition is true.
      /// </summary>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharactersIf(int length, bool expression) => expression ? ExactNumberOfCharacters(length) : this;

      /// <summary>
      /// Determines whether a string has an exact length if a given condition is true with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharactersIf(int length, bool expression, ErrorMessage message) => expression ? ExactNumberOfCharacters(length, message) : this;

      /// <summary>
      /// Validates equality to a fixed value.
      /// </summary>
      /// <param name="value">The value to compare against.</param>
      public RuleBuilder<T, TAttribute> IsEqualTo(TAttribute value)
      {
         var attributeName = GetAttributeName();
         return AddRule(instance =>
         {
            var attrValue = GetAttributeValue(instance);
            if (attrValue is null && value is null) return true;
            return attrValue is not null && attrValue.Equals(value);
         }, new ErrorMessage($"'{attributeName}' must be equal to {value}."));
      }

      /// <summary>
      /// Validates if the property value is equal to another property value.
      /// </summary>
      /// <param name="comparisonProperty">Expression representing the property to compare with.</param>
      public RuleBuilder<T, TAttribute> IsEqualTo(Expression<Func<T, TAttribute>> comparisonProperty)
         => EqualityCompare(comparisonProperty, true, "be equal to");

      /// <summary>
      /// Validates that the value is greater than a specified value.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsGreaterThan(object value) => CompareNumericValues(value, (attr, val) => attr > val, "greater than");

      /// <summary>
      /// Validates that the value is greater than or equal to a specified value.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo(object value) => CompareNumericValues(value, (attr, val) => attr >= val, "greater than or equal to");

      /// <summary>
      /// Validates that the value is less than a specified value.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsLessThan(object value) => CompareNumericValues(value, (attr, val) => attr < val, "less than");

      /// <summary>
      /// Validates that the value is less than or equal to a specified value.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsLessThanOrEqualTo(object value) => CompareNumericValues(value, (attr, val) => attr <= val, "less than or equal to");

      /// <summary>
      /// Validates if the property value is not equal to a fixed value.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsNotEqualTo(TAttribute value)
      {
         var attributeName = GetAttributeName();
         return AddRule(instance =>
         {
            var attrValue = GetAttributeValue(instance);
            if (attrValue is null && value is null) return false;
            return attrValue is null || !attrValue.Equals(value);
         }, new ErrorMessage($"'{attributeName}' must not be equal to {value}."));
      }

      /// <summary>
      /// Validates if the property value is not equal to another property value.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsNotEqualTo(Expression<Func<T, TAttribute>> comparisonProperty)
         => EqualityCompare(comparisonProperty, false, "not be equal to");

      /// <summary>
      /// Determines whether a value was filled.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequired() => IsRequired(new ErrorIsRequired(GetAttributeName()));

      /// <summary>
      /// Determines whether a value was filled with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequired(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance);
            return value is not null && !string.IsNullOrWhiteSpace(value.ToString());
         }, message);
      }

      /// <summary>
      /// Determines whether the property is required if a given condition is true.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequiredIf(bool expression) => expression ? IsRequired() : this;

      /// <summary>
      /// Determines whether the property is required if a condition is true with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequiredIf(bool expression, ErrorMessage message) => expression ? IsRequired(message) : this;

      /// <summary>
      /// Determines whether a string is a valid date.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsValidDate() => IsValidDate(new ErrorInvalidDate(GetAttributeName()));

      /// <summary>
      /// Determines whether a string is a valid date with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsValidDate(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || DateTime.TryParse(value, out _);
         }, message);
      }

      /// <summary>
      /// Determines whether a string is a valid email address.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsValidEmailAddress() => IsValidEmailAddress(new ErrorInvalidEmail(GetAttributeName()));

      /// <summary>
      /// Determines whether a string is a valid email address with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> IsValidEmailAddress(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var email = GetAttributeValue(instance)?.ToString();
            if (string.IsNullOrEmpty(email)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
         }, message);
      }

      /// <summary>
      /// Validates the maximum character length.
      /// </summary>
      public RuleBuilder<T, TAttribute> MaxLength(int maxLength) => MaxLength(maxLength, new ErrorMoreCharactersThanExpected(GetAttributeName(), maxLength));

      /// <summary>
      /// Validates the maximum character length with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> MaxLength(int maxLength, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || value.Length <= maxLength;
         }, message);
      }

      /// <summary>
      /// Validates the maximum character length if a condition is true.
      /// </summary>
      public RuleBuilder<T, TAttribute> MaxLengthIf(int maxLength, bool expression) => expression ? MaxLength(maxLength) : this;

      /// <summary>
      /// Validates the maximum length if a condition is true with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> MaxLengthIf(int maxLength, bool expression, ErrorMessage message) => expression ? MaxLength(maxLength, message) : this;

      /// <summary>
      /// Shortcut to access error messages from the parent validator.
      /// </summary>
      public List<ErrorMessage> Messages => _validator.Messages;

      /// <summary>
      /// Validates the minimum character length.
      /// </summary>
      public RuleBuilder<T, TAttribute> MinLength(int minLength) => MinLength(minLength, new ErrorFewerCharactersThanExpected(GetAttributeName(), minLength));

      /// <summary>
      /// Validates the minimum character length with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> MinLength(int minLength, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
         }, message);
      }

      /// <summary>
      /// Validates the minimum character length if a condition is true.
      /// </summary>
      public RuleBuilder<T, TAttribute> MinLengthIf(int minLength, bool expression) => expression ? MinLength(minLength) : this;

      /// <summary>
      /// Validates the minimum length if a condition is true with a custom message.
      /// </summary>
      public RuleBuilder<T, TAttribute> MinLengthIf(int minLength, bool expression, ErrorMessage message) => expression ? MinLength(minLength, message) : this;

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

      #region PrivateMethods
      /// <summary>
      /// Internal helper to register rules directly into the parent validator.
      /// </summary>
      private RuleBuilder<T, TAttribute> AddRule(Func<T, bool> rule, ErrorMessage errorMessage)
      {
         _validator.AddRule(rule, errorMessage);
         return this;
      }

      /// <summary>
      /// Core logic for numeric comparisons.
      /// </summary>
      private RuleBuilder<T, TAttribute> CompareNumericValues(object value, Func<double, double, bool> comparison, string label)
      {
         var attributeName = GetAttributeName();
         return AddRule(instance =>
         {
            var attrValue = GetAttributeValue(instance);
            if (attrValue is null || value is null) return false;

            try
            {
               return comparison(Convert.ToDouble(attrValue), Convert.ToDouble(value));
            }
            catch { return false; }
         }, new ErrorMessage($"'{attributeName}' must be {label} {value}."));
      }

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
      /// Extracts the property name from the attribute expression.
      /// </summary>
      private string GetAttributeName()
      {
         if (_attribute.Body is MemberExpression me) return me.Member.Name;
         if (_attribute.Body is UnaryExpression ue && ue.Operand is MemberExpression ume) return ume.Member.Name;
         return "Unknown";
      }

      /// <summary>
      /// Executes the attribute function to get the current value.
      /// </summary>
      private object? GetAttributeValue(T instance) => _attributeFunc(instance);

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