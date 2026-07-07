using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class CollectionErrorMessages
{
   public const string ContainsInvalidValueError = nameof(ContainsInvalidValueError);
   public const string IsEmptyError = nameof(IsEmptyError);
   public const string IsNotEmptyError = nameof(IsNotEmptyError);
   public const string InvalidNumberOfItemsError = nameof(InvalidNumberOfItemsError);
   public const string MaxNumberOfItemsError = nameof(MaxNumberOfItemsError);
   public const string ContainsDuplicateItemsError = nameof(ContainsDuplicateItemsError);

   public static ErrorMessage ContainsInvalidValue(string fieldName)
      => Create(ContainsInvalidValueError, fieldName);

   public static ErrorMessage IsEmpty(string fieldName)
      => Create(IsEmptyError, fieldName);

   public static ErrorMessage IsNotEmpty(string fieldName)
      => Create(IsNotEmptyError, fieldName);

   public static ErrorMessage InvalidNumberOfItems(
      string fieldName,
      int condition)
      => ValidationError.Create(
         InvalidNumberOfItemsError,
         ("fieldName", fieldName),
         ("condition", condition.ToString()));

   public static ErrorMessage MaxNumberOfItems(string fieldName, int max)
      => ValidationError.Create(
         MaxNumberOfItemsError,
         ("fieldName", fieldName),
         ("max", max.ToString()));

   public static ErrorMessage ContainsDuplicateItems(string fieldName)
      => Create(ContainsDuplicateItemsError, fieldName);

   private static ErrorMessage Create(string code, string fieldName)
      => ValidationError.Create(code, ("fieldName", fieldName));
}
