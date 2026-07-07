using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class StateErrorMessages
{
   public const string IsFalseError = nameof(IsFalseError);
   public const string IsTrueError = nameof(IsTrueError);
   public const string IsNullError = nameof(IsNullError);
   public const string IsNotNullError = nameof(IsNotNullError);
   public const string IsRequiredError = nameof(IsRequiredError);

   public static ErrorMessage IsFalse(string fieldName)
      => Create(IsFalseError, fieldName);
   public static ErrorMessage IsTrue(string fieldName)
      => Create(IsTrueError, fieldName);
   public static ErrorMessage IsNull(string fieldName)
      => Create(IsNullError, fieldName);
   public static ErrorMessage IsNotNull(string fieldName)
      => Create(IsNotNullError, fieldName);
   public static ErrorMessage IsRequired(string fieldName)
      => Create(IsRequiredError, fieldName);

   private static ErrorMessage Create(string code, string fieldName)
      => ValidationError.Create(code, ("fieldName", fieldName));
}
