using Myce.Response.Messages;
using Myce.Validation.ErrorMessages;
using System.Linq.Expressions;

namespace Myce.Validation
{
   public class RuleBuilder<T, TAttribute>
   {
      private readonly EntityValidator<T> _validator;
      private readonly Expression<Func<T, TAttribute>> _attribute;
      private readonly Func<T, TAttribute> _attributeFunc;
      private readonly List<(Func<T, bool> rule, ErrorMessage errorMessage)> _rules = new();

      public RuleBuilder(EntityValidator<T> validator, Expression<Func<T, TAttribute>> attribute)
      {
         _validator = validator;
         _attribute = attribute;
         _attributeFunc = attribute.Compile();
      }

      private RuleBuilder<T, TAttribute> Compare(object value, Func<double, double, bool> comparison)
      {
         var attributeName = GetAttributeName();
         _rules.Add((instance =>
         {
            var attributeValue = _attribute.Compile()(instance);

            if (attributeValue == null || value == null)
            {
               return false;
            }

            double attributeAsDouble = Convert.ToDouble(attributeValue);
            double valueAsDouble = Convert.ToDouble(value);

            return comparison(attributeAsDouble, valueAsDouble);
         }, new ErrorMessage($"'{attributeName}' must be {comparison.Method.Name} {value}.")));

         return this;
      }

      public RuleBuilder<T, TAttribute> IsGreaterThan(object value)
      {
         return Compare(value, (attr, val) => attr > val);
      }

      public RuleBuilder<T, TAttribute> IsGreaterThanOrEqualTo(object value)
      {
         return Compare(value, (attr, val) => attr >= val);
      }

      public RuleBuilder<T, TAttribute> IsLessThan(object value)
      {
         return Compare(value, (attr, val) => attr < val);
      }

      public RuleBuilder<T, TAttribute> IsLessThanOrEqualTo(object value)
      {
         return Compare(value, (attr, val) => attr <= val);
      }

      public RuleBuilder<T, TAttribute> IsEqualTo(TAttribute value)
      {
         var attributeName = GetAttributeName();
         _rules.Add(((T instance) => {
             var attrValue = GetAttributeValue(instance);
             return attrValue != null && attrValue.Equals(value);
         }, new ErrorMessage($"'{attributeName}' must be equal to {value}.")));
         return this;
      }

      /// <summary>
      /// Determines whether a sequence contains a specified element
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      /// <param name="values">The sequence</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> Contains(string[] values, ErrorMessage message)
      {
         Func<T, bool> rule = (instance) => {
            var val = GetAttributeValue(instance)?.ToString();
            return val != null && values.Contains(val);
         };
         _rules.Add((rule, message));
         return this;
      }
      /// <summary>
      /// Determines whether a sequence contains only numeric characters
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      public RuleBuilder<T, TAttribute> ContainsOnlyNumber()
      {
         return ContainsOnlyNumber(new ErrorShouldContainOnlyNumber(GetAttributeName()));
      }

      /// <summary>
      /// Determines whether a sequence contains only numeric characters
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> ContainsOnlyNumber(ErrorMessage message)
      {
         Func<T, bool> rule = (instance) => {
             var val = GetAttributeValue(instance)?.ToString();
             return string.IsNullOrEmpty(val) || val.All(char.IsNumber);
         };
         _rules.Add((rule, message));
         return this;
      }

      /// <summary>
      /// Determines whether a string has an exact character length
      /// </summary>
      /// <param name="length">Expected size</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharacters(int length)
      {
         var attributeName = GetAttributeName();
         return ExactNumberOfCharacters(length, new ErrorNotExactNumberOfCharacters(attributeName, length));
      }

      /// <summary>
      /// Determines whether a string has an exact character length
      /// </summary>
      /// <param name="length">Expected size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharacters(int length, ErrorMessage message)
      {
         _rules.Add(((T instance) => {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || value.Length == length;
         }, message));
         return this;
      }

      /// <summary>
      /// Determines whether a string has an exact character length if a given condition is true
      /// </summary>
      /// <param name="length">Expected size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharactersIf(int length, bool expression)
      {
         var attributeName = GetAttributeName();
         return ExactNumberOfCharactersIf(length, expression, new ErrorNotExactNumberOfCharacters(attributeName, length));
      }

      /// <summary>
      /// Determines whether a string has an exact character length if a given condition is true
      /// </summary>
      /// <param name="length">Expected size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> ExactNumberOfCharactersIf(int length, bool expression, ErrorMessage message)
      {
         return expression ? ExactNumberOfCharacters(length, message) : this;
      }

      /// <summary>
      ///  Determines whether was filled if a given condition is true
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequired()
      {
         return IsRequired(new ErrorIsRequired(GetAttributeName()));
      }

      /// <summary>
      ///  Determines whether a value was filled
      /// </summary>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> IsRequired(ErrorMessage message)
      {
         _rules.Add(((T instance) => {
            var value = GetAttributeValue(instance);
            return value != null && !string.IsNullOrEmpty(value.ToString());
         }, message));
         return this;
      }

      /// <summary>
      /// Determines whether the property is required if a given condition is true
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequiredIf(bool expression)
      {
         return IsRequiredIf(expression, new ErrorIsRequired(GetAttributeName()));
      }

      /// <summary>
      /// Determines whether the property is required if a given condition is true
      /// </summary>
      public RuleBuilder<T, TAttribute> IsRequiredIf(bool expression, ErrorMessage message)
      {
         return expression ? IsRequired(message) : this;
      }

      /// <summary>
      /// Determines whether a string is a valid date
      /// </summary>
      public RuleBuilder<T, TAttribute> IsValidDate()
      {
         var attributeName = GetAttributeName();
         return IsValidDate(new ErrorInvalidDate(attributeName));
      }

      /// <summary>
      /// Determines whether a string is a valid date
      /// </summary>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> IsValidDate(ErrorMessage message)
      {
         _rules.Add(((T instance) => {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || DateTime.TryParse(value, out _);
         }, message));
         return this;
      }

      /// <summary>
      /// Determines whether a string is a valid email address
      /// </summary>
      public RuleBuilder<T, TAttribute> IsValidEmailAddress()
      {
         var attributeName = GetAttributeName();
         return IsValidEmailAddress(new ErrorInvalidEmail(attributeName));
      }

      /// <summary>
      /// Determines whether a string is a valid email address
      /// </summary>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> IsValidEmailAddress(ErrorMessage message)
      {
         _rules.Add(((T instance) => {
            var email = GetAttributeValue(instance)?.ToString();
            if (string.IsNullOrEmpty(email)) return true;
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
         }, message));
         return this;
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed
      /// </summary>
      /// <param name="maxLength">Expected maximum size</param>
      public RuleBuilder<T, TAttribute> MaxLength(int maxLength)
      {
         var attributeName = GetAttributeName();
         return MaxLength(maxLength, new ErrorMoreCharactersThanExpected(attributeName, maxLength));
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed
      /// </summary>
      /// <param name="maxLength">Expected maximum size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> MaxLength(int maxLength, ErrorMessage message)
      {
         _rules.Add(((T instance) => {
            var value = GetAttributeValue(instance)?.ToString();
            return string.IsNullOrEmpty(value) || value.Length <= maxLength;
         }, message));
         return this;
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed if a given condition is true
      /// </summary>
      /// <param name="maxLength">Expected maximum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public RuleBuilder<T, TAttribute> MaxLengthIf(int maxLength, bool expression)
      {
         var attributeName = GetAttributeName();
         return MaxLengthIf(maxLength, expression, new ErrorMoreCharactersThanExpected(attributeName, maxLength));
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed if a given condition is true
      /// </summary>
      /// <param name="maxLength">Expected maximum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> MaxLengthIf(int maxLength, bool expression, ErrorMessage message)
      {
         return expression ? MaxLength(maxLength, message) : this;
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed
      /// </summary>
      /// <param name="minLength">Expected minimum size</param>
      public RuleBuilder<T, TAttribute> MinLength(int minLength)
      {
         var attributeName = GetAttributeName();
         return MinLength(minLength, new ErrorFewerCharactersThanExpected(attributeName, minLength));
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed
      /// </summary>
      /// <param name="minLength">Expected minimum size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> MinLength(int minLength, ErrorMessage message)
      {
         _rules.Add(((T instance) => {
            var value = GetAttributeValue(instance)?.ToString();
            return !string.IsNullOrEmpty(value) && value.Length >= minLength;
         }, message));
         return this;
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed if a given condition is true
      /// </summary>
      /// <param name="minLength">Expected minimum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public RuleBuilder<T, TAttribute> MinLengthIf(int minLength, bool expression)
      {
         var attributeName = GetAttributeName();
         return MinLengthIf(minLength, expression, new ErrorFewerCharactersThanExpected(attributeName, minLength));
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed if a given condition is true
      /// </summary>
      /// <param name="minLength">Expected minimum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public RuleBuilder<T, TAttribute> MinLengthIf(int minLength, bool expression, ErrorMessage message)
      {
         return expression ? MinLength(minLength, message) : this;
      }

      public EntityValidator<T> Apply()
      {
         foreach (var (rule, errorMessage) in _rules)
         {
            _validator.AddRule(rule, errorMessage);
         }
         return _validator;
      }
      private object GetAttributeValue(T instance)
      {
         return _attributeFunc(instance);
      }

      private string GetAttributeName()
      {
         // Extract the name of the property from the expression
         if (_attribute.Body is MemberExpression memberExpression)
         {
            return memberExpression.Member.Name;
         }
         return "Unknown";
      }
   }
}
