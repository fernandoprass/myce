using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages;

public static class DateErrorMessages
{
   public const string IsTodayError = nameof(IsTodayError);
   public const string IsYesterdayError = nameof(IsYesterdayError);
   public const string IsTomorrowError = nameof(IsTomorrowError);
   public const string IsInTheFutureError = nameof(IsInTheFutureError);
   public const string IsInThePastError = nameof(IsInThePastError);
   public const string IsWeekendError = nameof(IsWeekendError);
   public const string IsWeekdayError = nameof(IsWeekdayError);

   public static ErrorMessage IsToday(string fieldName)
      => Create(IsTodayError, fieldName);
   public static ErrorMessage IsYesterday(string fieldName)
      => Create(IsYesterdayError, fieldName);
   public static ErrorMessage IsTomorrow(string fieldName)
      => Create(IsTomorrowError, fieldName);
   public static ErrorMessage IsInTheFuture(string fieldName)
      => Create(IsInTheFutureError, fieldName);
   public static ErrorMessage IsInThePast(string fieldName)
      => Create(IsInThePastError, fieldName);
   public static ErrorMessage IsWeekend(string fieldName)
      => Create(IsWeekendError, fieldName);
   public static ErrorMessage IsWeekday(string fieldName)
      => Create(IsWeekdayError, fieldName);

   private static ErrorMessage Create(string code, string fieldName)
      => ValidationError.Create(code, ("fieldName", fieldName));
}
