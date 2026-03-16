using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class MustBeEqualError : ErrorMessage
   {
      public MustBeEqualError(string fieldName, string value)
         : base("MustBeEqualError", $"'{fieldName}' must be equal to {value}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("value", value);
      }
   }

   public class MustNotBeEqualError : ErrorMessage
   {
      public MustNotBeEqualError(string fieldName, string value)
         : base("MustNotBeEqualError", $"'{fieldName}' must not be equal to {value}.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("value", value);
      }
   }

   public class ComparisonError : ErrorMessage
   {
      public ComparisonError(string fieldName, string label, string comparisonName)
         : base("ComparisonError", $"'{fieldName}' must {label} '{comparisonName}'.")
      {
         AddVariable("fieldName", fieldName);
         AddVariable("label", label);
         AddVariable("comparisonName", comparisonName);
      }
   }
}
