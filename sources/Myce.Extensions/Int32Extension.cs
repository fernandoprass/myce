namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for int type
   /// </summary>
   public static class Int32Extension
   {
      /// <summary> Return TRUE if the value is equal zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool EqualZero(this int value)
      {
         return value == 0;
      }

      /// <summary> Return TRUE if the value is equal zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool EqualZero(this int? value)
      {
         return value.HasValue && value.Value.EqualZero();
      }

      /// <summary> Return TRUE if the value is greater zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool GreaterThanZero(this int value)
      {
         return value > 0;
      }

      /// <summary> Return TRUE if the value is greater than zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool GreaterThanZero(this int? value)
      {
         return value.HasValue && value.Value.GreaterThanZero();
      }

      /// <summary> Return TRUE if the value is greater than or equal zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool GreaterThanOrEqualZero(this int value)
      {
         return value >= 0;
      }

      /// <summary> Return TRUE if the value is greater than or equal zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool GreaterThanOrEqualZero(this int? value)
      {
         return value.HasValue && value.Value.GreaterThanOrEqualZero();
      }

      /// <summary> Return TRUE if the value is less than zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool LessThanZero(this int value)
      {
         return value < 0;
      }

      /// <summary> Return TRUE if the value is less than zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool LessThanZero(this int? value)
      {
         return value.HasValue && value.Value.LessThanZero();
      }

      /// <summary> Return TRUE if the value is less than or equal zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool LessThanOrEqualZero(this int value)
      {
         return value <= 0;
      }

      /// <summary> Return TRUE if the value is less than or equal zero </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool LessThanOrEqualZero(this int? value)
      {
         return value.HasValue && value.Value.LessThanOrEqualZero();
      }

      /// <summary>
      /// Compare int values is between other two values (inclusive)
      /// </summary>
      /// <typeparam name="T">Any int that implements IComparable</typeparam>
      /// <param name="value">The int value</param>
      /// <param name="from">The FROM value</param>
      /// <param name="to">The TO value</param>
      /// <returns>Return TRUE if the value of the object is BETWEEN other two values (inclusive)</returns>
      public static bool IsBetween<T>(this int value, T from, T to) where T : IComparable<T>
      {
         return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
      }
   }
}
