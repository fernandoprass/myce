using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for RuleBuilder for requirement and nullability validations.
   /// </summary>
   public static class RuleBuilderCommonExtensions
   {
      /// <summary>
      /// Validates if the property value is not null.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsNotNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            return attrValue is not null;
         }, new ErrorMessage($"'{attributeName}' is null."));
      }

      /// <summary>
      /// Validates if the property value is null.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsNull<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         var attributeName = ruleBuilder.GetAttributeName();
         return ruleBuilder.AddRule(instance =>
         {
            var attrValue = ruleBuilder.GetAttributeValue(instance);
            return attrValue is null;
         }, new ErrorMessage($"'{attributeName}' is not null."));
      }

      /// <summary>
      /// Determines whether a value was filled (not null and not empty/whitespace if string).
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequired<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder)
         where T : class
      {
         return ruleBuilder.IsRequired(new ErrorIsRequired(ruleBuilder.GetAttributeName()));
      }

      /// <summary>
      /// Determines whether a value was filled with a custom message.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequired<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, ErrorMessage message)
         where T : class
      {
         return ruleBuilder.AddRule(instance =>
         {
            var value = ruleBuilder.GetAttributeValue(instance);
            if (value is null) return false;

            // Check for empty strings or whitespace
            if (value is string s) return !string.IsNullOrWhiteSpace(s);

            return true;
         }, message);
      }

      /// <summary>
      /// Determines whether the property is required if a given condition is true.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequiredIf<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, bool condition)
         where T : class
      {
         return condition ? ruleBuilder.IsRequired() : ruleBuilder;
      }

      /// <summary>
      /// Determines whether the property is required if a condition is true with a custom message.
      /// </summary>
      public static RuleBuilder<T, TAttribute> IsRequiredIf<T, TAttribute>(this RuleBuilder<T, TAttribute> ruleBuilder, bool condition, ErrorMessage message)
         where T : class
      {
         return condition ? ruleBuilder.IsRequired(message) : ruleBuilder;
      }
   }
}