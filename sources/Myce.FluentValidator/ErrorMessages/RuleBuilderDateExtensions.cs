using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class IsTodayError : ErrorMessage
   {
      public IsTodayError(string fieldName)
         : base("IsTodayError", $"'{fieldName}' must be today.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsYesterdayError : ErrorMessage
   {
      public IsYesterdayError(string fieldName)
         : base("IsYesterdayError", $"'{fieldName}' must be yesterday.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsTomorrowError : ErrorMessage
   {
      public IsTomorrowError(string fieldName)
         : base("IsTomorrowError", $"'{fieldName}' must be tomorrow.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsInTheFutureError : ErrorMessage
   {
      public IsInTheFutureError(string fieldName)
         : base("IsInTheFutureError", $"'{fieldName}' must be a future date.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsInThePastError : ErrorMessage
   {
      public IsInThePastError(string fieldName)
         : base("IsInThePastError", $"'{fieldName}' must be a past date.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsWeekendError : ErrorMessage
   {
      public IsWeekendError(string fieldName)
         : base("IsWeekendError", $"'{fieldName}' must be a weekend date.")
      {
         AddVariable("fieldName", fieldName);
      }
   }

   public class IsWeekdayError : ErrorMessage
   {
      public IsWeekdayError(string fieldName)
         : base("IsWeekdayError", $"'{fieldName}' must be a weekday.")
      {
         AddVariable("fieldName", fieldName);
      }
   }
}
