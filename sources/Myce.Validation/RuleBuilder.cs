using Myce.Response.Messages;
using Myce.Validation.ErrorMessages;
using System.Linq.Expressions;

namespace Myce.Validation
{
   public class RuleBuilder<T, TAttribute>
   {
      private readonly EntityValidator<T> _validator;
      private readonly Expression<Func<T, TAttribute>> _attribute;
      private readonly List<(Func<T, bool> rule, ErrorMessage errorMessage)> _rules = new();

      public RuleBuilder(EntityValidator<T> validator, Expression<Func<T, TAttribute>> attribute)
      {
         _validator = validator;
         _attribute = attribute;
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
         _rules.Add((instance => GeAttribute(instance).Equals(value),
                     new ErrorMessage($"'{attributeName}' must be equal to {value}.")));
         return this;
      }

      /// <summary>
      /// Determines whether a sequence contains a specified element
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      /// <param name="values">The sequence</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> Contains(string[] values, ErrorMessage message)
      {
         return this;// If(!values.Contains(value), message);
      }

      /// <summary>
      /// Determines whether a sequence contains only numeric characters
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      public  RuleBuilder<T, TAttribute> ContainsOnlyNumber()
      {
         return ContainsOnlyNumber(new ErrorShouldContainOnlyNumber(GetAttributeName()));
      }

      /// <summary>
      /// Determines whether a sequence contains only numeric characters
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> ContainsOnlyNumber(ErrorMessage message)
      {
         var attributeName = GetAttributeName();
         _rules.Add((instance => _attribute.Compile()(instance).ToString().Any(c => !char.IsNumber(c)), message));
         return this;
      }

      /// <summary>
      /// Determines whether a string has an exact character length
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      public  RuleBuilder<T, TAttribute> ExactNumberOfCharacteres(string value, int lenght)
      {
         return ExactNumberOfCharacteres(value, lenght, new ErrorNotExactNumberOfCharacteres(value, lenght));
      }

      /// <summary>
      /// Determines whether a string has an exact character length
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> ExactNumberOfCharacteres(string value, int lenght, Message message)
      {
         return this;//If(!string.IsNullOrEmpty(value) && value.Length != lenght, message);
      }

      /// <summary>
      ///  Determines whether a string has an exact character length if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public  RuleBuilder<T, TAttribute> ExactNumberOfCharacteresIf(string value, int lenght, bool expression)
      {
         return ExactNumberOfCharacteresIf(value, lenght, expression, new ErrorNotExactNumberOfCharacteres(value, lenght));
      }

      /// <summary>
      ///  Determines whether a string has an exact character length if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> ExactNumberOfCharacteresIf(string value, int lenght, bool expression, Message message)
      {
         return expression ? MaxLenght(value, lenght, message) : this;
      }

      /// <summary>
      ///  Determines whether was filled if a given condition is true
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="fieldName">The field name</param>
      public  RuleBuilder<T, TAttribute> IsRequired()
      {
         return IsRequired(new ErrorIsRequired(GetAttributeName()));
      }

      /// <summary>
      ///  Determines whether a value was filled
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> IsRequired(ErrorMessage message)
      {
         var attributeName = GetAttributeName();
         _rules.Add((instance => _attribute.Compile()(instance) != null &&
                                  !string.IsNullOrEmpty(_attribute.Compile()(instance).ToString()), message));
         return this;
      }

      /// <summary>
      /// Determines whether was filled if a given condition is true
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="fieldName">The field name</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public  RuleBuilder<T, TAttribute> IsMandatoryIf(string value, string fieldName, bool expression)
      {
         return default;
         //return expression ? IsRequired(value, fieldName) : this;
      }

      /// <summary>
      /// Determines whether was filled if a given condition is true
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> IsMandatoryIf(string value, bool expression, Message message)
      {
         return default;
         //return expression ? IsRequired(value, message) : this;
      }

      /// <summary>
      /// Determines whether a string is a valid date
      /// </summary>
      /// <param name="date">The date value</param>
      public  RuleBuilder<T, TAttribute> IsValidDate(string date)
      {
         return IsValidDate(date, new ErrorInvalidDate(date));
      }

      /// <summary>
      /// Determines whether a string is a valid date
      /// </summary>
      /// <param name="date">The date value</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> IsValidDate(string date, Message message)
      {
         if (!string.IsNullOrEmpty(date))
         {
            var result = DateTime.TryParse(date, out var dateTime);
            return this;//If(!result, message);
         }
         return this;
      }

      /// <summary>
      /// Determines whether a string is a valid email address
      /// </summary>
      /// <param name="value">The value</param>
      public  RuleBuilder<T, TAttribute> IsValidEmailAddress(string email)
      {
         return IsValidEmailAddress(email, new ErrorInvalidEmail(email));
      }

      /// <summary>
      /// Determines whether a string is a valid email address
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> IsValidEmailAddress(string email, Message message)
      {
         return this;
         //if (!string.IsNullOrEmpty(email))
         //{
         //   var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
         //   var match = regex.Match(email);
         //   return If(!match.Success, message);
         //}
         //return this;
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      public  RuleBuilder<T, TAttribute> MaxLenght(string value, int maxLenght)
      {
         return MaxLenght(value, maxLenght, new ErrorMoreCharactersThanExpected(value, maxLenght));
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> MaxLenght(string value, int maxLenght, Message message)
      {
         return this;
         //return If(!string.IsNullOrEmpty(value) && value.Length > maxLenght, message);
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public  RuleBuilder<T, TAttribute> MaxLenghtIf(string value, int maxLenght, bool expression)
      {
         return MaxLenghtIf(value, maxLenght, expression, new ErrorMoreCharactersThanExpected(value, maxLenght));
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      /// <returns></returns>
      public  RuleBuilder<T, TAttribute> MaxLenghtIf(string value, int maxLenght, bool expression, Message message)
      {
         return expression ? MaxLenght(value, maxLenght, message) : this;
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      public  RuleBuilder<T, TAttribute> MinLenght(string value, int minLenght)
      {
         return MinLenght(value, minLenght, new ErrorFewerCharactersThanExpected(value, minLenght));
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> MinLenght(string value, int minLenght, Message message)
      {
         return this;
         //return If(string.IsNullOrEmpty(value) || value.Length < minLenght, message);
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public  RuleBuilder<T, TAttribute> MinLenghtIf(string value, int minLenght, bool expression)
      {
         return MinLenghtIf(value, minLenght, expression, new ErrorFewerCharactersThanExpected(value, minLenght));
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public  RuleBuilder<T, TAttribute> MinLenghtIf(string value, int minLenght, bool expression, Message message)
      {
         return expression ? MinLenght(value, minLenght, message) : this;
      }

      public EntityValidator<T> Apply()
      {
         foreach (var (rule, errorMessage) in _rules)
         {
            _validator.AddRule(rule, errorMessage);
         }
         return _validator;
      }
      private TAttribute GeAttribute(T instance)
      {
         var attribute = _attribute.Compile()(instance);
         ArgumentNullException.ThrowIfNull(attribute, $"Invalid attribute name {instance}");
         return attribute;
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
