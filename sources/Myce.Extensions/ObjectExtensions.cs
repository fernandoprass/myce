namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for Object type
   /// </summary>
   public static class ObjectExtensions
   {
      /// <summary>
      /// Check if the value of the object is null
      /// </summary>
      /// <param name="obj">The object</param>
      /// <returns>Return TRUE if the object is null</returns>
      public static bool IsNull(this object obj)
      {
         return obj == null;
      }

      /// <summary>
      ///  Check if the value of the object is not null
      /// </summary>
      /// <param name="obj">The object value</param>
      /// <returns>Return TRUE if the object is NOT null</returns>
      public static bool IsNotNull(this object obj)
      {
         return obj != null;
      }

      /// <summary>
      /// Compare object values is between other two values (inclusive)
      /// </summary>
      /// <typeparam name="T">Any object that implements IComparable</typeparam>
      /// <param name="value">The object value</param>
      /// <param name="from">The FROM value</param>
      /// <param name="to">The TO value</param>
      /// <returns>Return TRUE if the value of the object is BETWEEN other two values (inclusive)</returns>
      public static bool IsBetween<T>(this T value, T from, T to) where T : IComparable<T>
      {
         return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
      }
   }
}
