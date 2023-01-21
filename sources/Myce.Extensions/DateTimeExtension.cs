using System;

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
   }
}
