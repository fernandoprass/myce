using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using System;

namespace Myce.FluentValidator
{
   /// <summary>
   /// Extension methods for <see cref="RuleBuilder{T, TProperty}"/> providing validation for Enumeration types.
   /// </summary>
   public static partial class RuleBuilderEnumExtensions
   {
      #region IsInEnum
      /// <summary>
      /// Validates that the property value is a defined constant within the <typeparamref name="TProperty"/> enumeration.
      /// </summary>
      /// <typeparam name="T">The type of the object being validated.</typeparam>
      /// <typeparam name="TProperty">The enumeration type.</typeparam>
      /// <param name="rb">The rule builder instance.</param>
      /// <returns>The same <see cref="RuleBuilder{T, TProperty}"/> instance for method chaining.</returns>
      public static RuleBuilder<T, TProperty> IsInEnum<T, TProperty>(this RuleBuilder<T, TProperty> rb)
         where T : class where TProperty : struct, Enum
         => rb.IsInEnum(new InvalidEnumValueError(rb.GetAttributeName(), typeof(TProperty).Name));

      /// <summary>
      /// Validates that the property value is a defined constant within the <typeparamref name="TProperty"/> enumeration, using a custom error message.
      /// </summary>
      public static RuleBuilder<T, TProperty> IsInEnum<T, TProperty>(this RuleBuilder<T, TProperty> rb, ErrorMessage message)
         where T : class where TProperty : struct, Enum
         => rb.AddRule(instance => Enum.IsDefined(typeof(TProperty), rb.GetAttributeValue(instance)), message);

      /// <summary>
      /// Validates that the nullable property value is either null or a defined constant within the <typeparamref name="TProperty"/> enumeration.
      /// </summary>
      public static RuleBuilder<T, TProperty?> IsInEnum<T, TProperty>(this RuleBuilder<T, TProperty?> rb)
         where T : class where TProperty : struct, Enum
         => rb.IsInEnum(new InvalidEnumValueError(rb.GetAttributeName(), typeof(TProperty).Name));

      /// <summary>
      /// Validates that the nullable property value is either null or a defined constant within the <typeparamref name="TProperty"/> enumeration, using a custom error message.
      /// </summary>
      public static RuleBuilder<T, TProperty?> IsInEnum<T, TProperty>(this RuleBuilder<T, TProperty?> rb, ErrorMessage message)
         where T : class where TProperty : struct, Enum
         => rb.AddRule(instance => {
            var val = rb.GetAttributeValue(instance);
            return !val.HasValue || Enum.IsDefined(typeof(TProperty), val.Value);
         }, message);
      #endregion

      #region IsNotDefault
      /// <summary>
      /// Validates that the property value is not the default value of the enumeration (usually 0 or the first element).
      /// </summary>
      public static RuleBuilder<T, TProperty> IsNotDefault<T, TProperty>(this RuleBuilder<T, TProperty> rb)
         where T : class where TProperty : struct, Enum
      {
         var name = rb.GetAttributeName();
         return rb.AddRule(instance => !rb.GetAttributeValue(instance).Equals(default(TProperty)),
            new MustNotBeDefaultValueError(name));
      }
      #endregion

      #region IsNotInEnum
      /// <summary>
      /// Validates that the property value is NOT a defined constant within the <typeparamref name="TProperty"/> enumeration.
      /// </summary>
      public static RuleBuilder<T, TProperty> IsNotInEnum<T, TProperty>(this RuleBuilder<T, TProperty> rb)
         where T : class where TProperty : struct, Enum
         => rb.IsNotInEnum(new NotInEnumError(rb.GetAttributeName(), typeof(TProperty).Name));

      /// <summary>
      /// Validates that the property value is NOT a defined constant within the <typeparamref name="TProperty"/> enumeration, using a custom error message.
      /// </summary>
      public static RuleBuilder<T, TProperty> IsNotInEnum<T, TProperty>(this RuleBuilder<T, TProperty> rb, ErrorMessage message)
         where T : class where TProperty : struct, Enum
         => rb.AddRule(instance => !Enum.IsDefined(typeof(TProperty), rb.GetAttributeValue(instance)), message);
      #endregion
   }
}