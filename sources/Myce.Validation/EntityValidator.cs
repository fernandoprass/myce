using Myce.Extensions;
using Myce.Response.Messages;
using Myce.Validation.ErrorMessages;
using System.Text.RegularExpressions;

namespace Myce.Validation
{
    public class EntityValidator : Validator<EntityValidator>
   {
      /// <summary>
      /// Determines whether a sequence contains a specified elemen
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      /// <param name="values">The sequence</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator Contains(object value, string[] values, Message message)
      {
         return If(!values.Contains(value), message);
      }

      /// <summary>
      /// Determines whether a sequence contains only numeric characters
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      public EntityValidator ContainsOnlyNumber(string value)
      {
         return ContainsOnlyNumber(value, new ErrorNotContainsOnlyNumber(value));
      }

      /// <summary>
      /// Determines whether a sequence contains only numeric characters
      /// </summary>
      /// <param name="value">The value to be found in the sequence</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator ContainsOnlyNumber(string value, Message message)
      {
         if (!string.IsNullOrEmpty(value))
         {
            return If(value.Any(c => !char.IsNumber(c)), message);
         }
         return this;
      }

      /// <summary>
      /// Determines whether a string has an exact character length
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      public EntityValidator ExactNumberOfCharacteres(string value, int lenght)
      {
         return ExactNumberOfCharacteres(value, lenght, new ErrorNotExactNumberOfCharacteres(value, lenght));
      }

      /// <summary>
      /// Determines whether a string has an exact character length
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator ExactNumberOfCharacteres(string value, int lenght, Message message)
      {
         return If(!string.IsNullOrEmpty(value) && value.Length != lenght, message);
      }

      /// <summary>
      ///  Determines whether a string has an exact character length if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="lenght">Expected size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public EntityValidator ExactNumberOfCharacteresIf(string value, int lenght, bool expression)
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
      public EntityValidator ExactNumberOfCharacteresIf(string value, int lenght, bool expression, Message message)
      {
         return expression ? MaxLenght(value, lenght, message) : this;
      }

      /// <summary>
      ///  Determines whether was filled if a given condition is true
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="fieldName">The field name</param>
      public EntityValidator IsMandatory(object value, string fieldName)
      {
         return IsMandatory(value, new ErrorIsMandatory(fieldName));
      }

      /// <summary>
      ///  Determines whether a value was filled
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator IsMandatory(object value, Message message)
      {
         return value is string
                ? If(string.IsNullOrEmpty(value.ToString()), message)
                : If(value.IsNull(), message);
      }

      /// <summary>
      /// Determines whether was filled if a given condition is true
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="fieldName">The field name</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public EntityValidator IsMandatoryIf(string value, string fieldName, bool expression)
      {
         return expression ? IsMandatory(value, fieldName) : this;
      }

      /// <summary>
      /// Determines whether was filled if a given condition is true
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator IsMandatoryIf(string value, bool expression, Message message)
      {
         return expression ? IsMandatory(value, message) : this;
      }

      /// <summary>
      /// Determines whether a string is a valid date
      /// </summary>
      /// <param name="date">The date value</param>
      public EntityValidator IsValidDate(string date)
      {
         return IsValidDate(date, new ErrorInvalidDate(date));
      }

      /// <summary>
      /// Determines whether a string is a valid date
      /// </summary>
      /// <param name="date">The date value</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator IsValidDate(string date, Message message)
      {
         if (!string.IsNullOrEmpty(date))
         {
            var result = DateTime.TryParse(date, out var dateTime);
            return If(!result, message);
         }
         return this;
      }

      /// <summary>
      /// Determines whether a string is a valid email address
      /// </summary>
      /// <param name="value">The value</param>
      public EntityValidator IsValidEmailAddress(string email)
      {
         return IsValidEmailAddress(email, new ErrorInvalidEmail(email));
      }

      /// <summary>
      /// Determines whether a string is a valid email address
      /// </summary>
      /// <param name="value">The value</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator IsValidEmailAddress(string email, Message message)
      {
         if (!string.IsNullOrEmpty(email))
         {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            return If(!match.Success, message);
         }
         return this;
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      public EntityValidator MaxLenght(string value, int maxLenght)
      {
         return MaxLenght(value, maxLenght, new ErrorMoreCharactersThanExpected(value, maxLenght));
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator MaxLenght(string value, int maxLenght, Message message)
      {
         return If(!string.IsNullOrEmpty(value) && value.Length > maxLenght, message);
      }

      /// <summary>
      /// Determines whether a string has the maximum size allowed if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="maxLenght">Expected maximum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public EntityValidator MaxLenghtIf(string value, int maxLenght, bool expression)
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
      public EntityValidator MaxLenghtIf(string value, int maxLenght, bool expression, Message message)
      {
         return expression ? MaxLenght(value, maxLenght, message) : this;
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      public EntityValidator MinLenght(string value, int minLenght)
      {
         return MinLenght(value, minLenght, new ErrorFewerCharactersThanExpected(value, minLenght));
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      /// <param name="message">The message to be returned in case of an error</param>
      public EntityValidator MinLenght(string value, int minLenght, Message message)
      {
         return If(string.IsNullOrEmpty(value) || value.Length < minLenght, message);
      }

      /// <summary>
      /// Determines whether a string has the minimum size allowed if a given condition is true
      /// </summary>
      /// <param name="value">The string to be validated</param>
      /// <param name="minLenght">Expected minimum size</param>
      /// <param name="expression">The condition necessary to apply the rule</param>
      public EntityValidator MinLenghtIf(string value, int minLenght, bool expression)
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
      public EntityValidator MinLenghtIf(string value, int minLenght, bool expression, Message message)
      {
         return expression ? MinLenght(value, minLenght, message) : this;
      }
   }

    public class EntityValidator<TEntity> : EntityValidator {
        public EntityValidator IsEqual(decimal value)
        {
            return value == 0 ? throw new NotImplementedException() : this;
        }
    }
}
