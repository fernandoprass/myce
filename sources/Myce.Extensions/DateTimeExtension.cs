namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for DateTime type
   /// </summary>
   public static class DateTimeExtension
   {
      /// <summary>
      /// Return the first day of the mounth for a given date
      /// </summary>
      /// <param name="value">Given date</param>
      public static DateTime FirstDayOfMonth(this DateTime value)
      {
         return new DateTime(value.Year, value.Month, 1);
      }

      /// <summary>
      /// Return the number of days in the mounth for a given date
      /// </summary>
      /// <param name="value">Given date</param>
      public static int DaysInMonth(this DateTime value)
      {
         return DateTime.DaysInMonth(value.Year, value.Month);
      }

      /// <summary>
      /// Return the last day of the mounth for a given date
      /// </summary>
      /// <param name="value">Given date</param>
      public static DateTime LastDayOfMonth(this DateTime value)
      {
         return new DateTime(value.Year, value.Month, value.DaysInMonth());
      }

      /// <summary>
      /// Return yesterday's date, always taking into account the current date
      /// </summary>
      /// <returns></returns>
      public static DateTime Yesterday(this DateTime value)
      {
         return DateTime.Today.AddDays(-1) ;
      }

      /// <summary>
      /// Return tomorrow's date, always taking into account the current date
      /// </summary>
      /// <returns></returns>
      public static DateTime Tomorrow(this DateTime value)
      {
         return DateTime.Today.AddDays(1);
      }

      /// <summary>
      /// Return true if the date is a workday (Monday to Friday)
      /// </summary>
      /// <returns></returns>
      public static bool IsWorkDay(this DateTime value)
      {
         return !value.IsWeekend();
      }

      /// <summary>
      /// Return true if the date is a weekend (Saturday or Sunday). Holidays are not taken into account.
      /// </summary>
      /// <returns></returns>
      public static bool IsWeekend(this DateTime value)
      {
         var day = value.DayOfWeek;
         return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
      }

      /// <summary>
      /// Return the prior workday (Monday to Friday) for a given date. Holidays are not taken into account.
      /// </summary>
      /// <returns></returns>
      public static DateTime PriorWorkday(this DateTime value)
      {
         var priorWorday = value.AddDays(-1);
         while (!priorWorday.IsWorkDay())
         {
            priorWorday = priorWorday.AddDays(-1);
         }
         return priorWorday;
      }

      /// <summary>
      /// Return the next workday (Monday to Friday) for a given date. Holidays are not taken into account.
      /// </summary>
      /// <returns></returns>
      public static DateTime NextWorkday(this DateTime value)
      {
         var nextWorday = value.AddDays(1);
         while (!nextWorday.IsWorkDay())
         {
            nextWorday = nextWorday.AddDays(1);
         }
         return nextWorday;
      }
   }
}
