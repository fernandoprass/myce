using Myce.Response.Messages;
using Myce.FluentValidator.ErrorMessages;
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

      public RuleBuilder(FluentValidator<T> validator, Expression<Func<T, TAttribute>> attribute)
      {
         _validator = validator;
         _attribute = attribute;
         _attributeFunc = attribute.Compile();
      }

      /// <summary>
      /// Internal helper to register rules directly into the parent validator.
      /// </summary>
      private RuleBuilder<T, TAttribute> AddRule(Func<T, bool> rule, ErrorMessage errorMessage)
      {
         _validator.AddRule(rule, errorMessage);
         return this;
      }

      private RuleBuilder<T, TAttribute> Compare(object value, Func<double, double, bool> comparison, string comparisonLabel)
      {
         var attributeName = GetAttributeName();
         return AddRule(instance =>
         {
            var attributeValue = _attributeFunc(instance);

            if (attributeValue == null || value == null)
               return false;

            try
            {
               double attributeAsDouble = Convert.ToDouble(attributeValue);
               double valueAsDouble = Convert.ToDouble(value);
               return comparison(attributeAsDouble, valueAsDouble);
            }
            catch
            {
               return false;
            }
         }, new ErrorMessage($"'{attributeName}' must be {comparisonLabel} {value}."));
      }

      /// <summary> Validates that the value is greater than a specified value. </summary>
      public RuleBuilder<T, TAttribute> IsGreaterThan(object value) => Compare(value, (attr, val) => attr > val, "greater than");

      /// <summary> Validates that the value is greater than or equal to a specified value. </summary>
      public RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo(object value) => Compare(value, (attr, val) => attr >= val, "greater than or equal to");

      /// <summary> Validates that the value is less than a specified value. </summary>
      public RuleBuilder<T, TAttribute> IsLessThan(object value) => Compare(value, (attr, val) => attr < val, "less than");

      /// <summary> Validates that the value is less than or equal to a specified value. </summary>
      public RuleBuilder<T, TAttribute> IsLessThanOrEqualTo(object value) => Compare(value, (attr, val) => attr <= val, "less than or equal to");

      /// <summary> Validates equality to a specific value. </summary>
      public RuleBuilder<T, TAttribute> IsEqualTo(TAttribute value)
      {
         var attributeName = GetAttributeName();
         return AddRule(instance =>
         {
            var attrValue = GetAttributeValue(instance);
            return attrValue != null && attrValue.Equals(value);
         }, new ErrorMessage($"'{attributeName}' must be equal to {value}."));
      }

      /// <summary>
      /// Determines whether a sequence contains a specified element.
      /// </summary>
      /// <param name="values">The sequence.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> Contains(string[] values, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var val = GetAttributeValue(instance)?.ToString();
            return val != null && values.Contains(val);
         }, message);
      }

      /// <summary> Determines whether a sequence contains only numeric characters. </summary>
      public RuleBuilder<T, TAttribute> ContainsOnlyNumber() => ContainsOnlyNumber(new ErrorShouldContainOnlyNumber(GetAttributeName()));

      /// <summary>
      /// Determines whether a sequence contains only numeric characters.
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

      /// <summary> Determines whether a string has an exact character length. </summary>
      /// <param name="length">Expected size.</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharacters(int length) => ExactNumberOfCharacters(length, new ErrorNotExactNumberOfCharacters(GetAttributeName(), length));

      /// <summary>
      /// Determines whether a string has an exact character length.
      /// </summary>
      /// <param name="length">Expected size.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharacters(int length, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || value.Length == length;
         }, message);
      }

      /// <summary> Determines whether a string has an exact character length if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharactersIf(int length, bool expression) => expression ? ExactNumberOfCharacters(length) : this;

      /// <summary> Determines whether a string has an exact character length if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharactersIf(int length, bool expression, ErrorMessage message) => expression ? ExactNumberOfCharacters(length, message) : this;

      /// <summary> Determines whether a value was filled. </summary>
      public RuleBuilder<T, TAttribute> IsRequired() => IsRequired(new ErrorIsRequired(GetAttributeName()));

      /// <summary>
      /// Determines whether a value was filled.
      /// </summary>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> IsRequired(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance);
            return value != null && !string.IsNullOrEmpty(value.ToString());
         }, message);
      }

      /// <summary> Determines whether the property is required if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> IsRequiredIf(bool expression) => expression ? IsRequired() : this;

      /// <summary> Determines whether the property is required if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> IsRequiredIf(bool expression, ErrorMessage message) => expression ? IsRequired(message) : this;

      /// <summary> Determines whether a string is a valid date. </summary>
      public RuleBuilder<T, TAttribute> IsValidDate() => IsValidDate(new ErrorInvalidDate(GetAttributeName()));

      /// <summary>
      /// Determines whether a string is a valid date.
      /// </summary>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> IsValidDate(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || DateTime.TryParse(value, out _);
         }, message);
      }

      /// <summary> Determines whether a string is a valid email address. </summary>
      public RuleBuilder<T, TAttribute> IsValidEmailAddress() => IsValidEmailAddress(new ErrorInvalidEmail(GetAttributeName()));

      /// <summary>
      /// Determines whether a string is a valid email address.
      /// </summary>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> IsValidEmailAddress(ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var email = GetAttributeValue(instance)?.ToString();
            if (string.IsNullOrEmpty(email)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
         }, message);
      }

      /// <summary> Determines whether a string has the maximum size allowed. </summary>
      /// <param name="maxLength">Expected maximum size.</param>
      public RuleBuilder<T, TAttribute> MaxLength(int maxLength) => MaxLength(maxLength, new ErrorMoreCharactersThanExpected(GetAttributeName(), maxLength));

      /// <summary>
      /// Determines whether a string has the maximum size allowed.
      /// </summary>
      /// <param name="maxLength">Expected maximum size.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> MaxLength(int maxLength, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || value.Length <= maxLength;
         }, message);
      }

      /// <summary> Determines whether a string has the maximum size allowed if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> MaxLengthIf(int maxLength, bool expression) => expression ? MaxLength(maxLength) : this;

      /// <summary> Determines whether a string has the maximum size allowed if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> MaxLengthIf(int maxLength, bool expression, ErrorMessage message) => expression ? MaxLength(maxLength, message) : this;

      /// <summary> Determines whether a string has the minimum size allowed. </summary>
      /// <param name="minLength">Expected minimum size.</param>
      public RuleBuilder<T, TAttribute> MinLength(int minLength) => MinLength(minLength, new ErrorFewerCharactersThanExpected(GetAttributeName(), minLength));

      /// <summary>
      /// Determines whether a string has the minimum size allowed.
      /// </summary>
      /// <param name="minLength">Expected minimum size.</param>
      /// <param name="message">The message to be returned in case of an error.</param>
      public RuleBuilder<T, TAttribute> MinLength(int minLength, ErrorMessage message)
      {
         return AddRule(instance =>
         {
            var value = GetAttributeValue(instance)?.ToString();
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
         }, message);
      }

      /// <summary> Determines whether a string has the minimum size allowed if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> MinLengthIf(int minLength, bool expression) => expression ? MinLength(minLength) : this;

      /// <summary> Determines whether a string has the minimum size allowed if a given condition is true. </summary>
      public RuleBuilder<T, TAttribute> MinLengthIf(int minLength, bool expression, ErrorMessage message) => expression ? MinLength(minLength, message) : this;

      /// <summary>
      /// Starts a rule for a different property, allowing fluent chaining.
      /// </summary>
      public RuleBuilder<T, TProperty> RuleFor<TProperty>(Expression<Func<T, TProperty>> attribute) => _validator.RuleFor(attribute);

      /// <summary>
      /// Allows automatic conversion of RuleBuilder to FluentValidator.
      /// This makes it possible to instantiate and configure in a single line.
      /// </summary>
      public static implicit operator FluentValidator<T>(RuleBuilder<T, TAttribute> builder)
      {
         return builder._validator;
      }

      /// <summary>
      /// Performs validation directly from the builder (shortcut to the parent validator).
      /// </summary>
      public bool Validate(T instance) => _validator.Validate(instance);

      /// <summary>
      /// Shortcut to access error messages from the parent validator.
      /// </summary>
      public List<ErrorMessage> Messages => _validator.Messages;

      private object GetAttributeValue(T instance) => _attributeFunc(instance);

      private string GetAttributeName()
      {
         if (_attribute.Body is MemberExpression memberExpression)
            return memberExpression.Member.Name;

         return "Unknown";
      }
   }
}