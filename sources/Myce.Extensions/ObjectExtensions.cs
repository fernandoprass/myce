namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for Object type
   /// </summary>
   public static class ObjectExtensions
   {
      /// <summary>
      /// Return TRUE if the object is null
      /// </summary>
      /// <param name="obj">The object</param>
      public static bool IsNull(this object obj)
      {
         return obj == null;
      }

      /// <summary>
      /// Return TRUE if the object is NOT null
      /// </summary>
      /// <param name="obj">The object</param>
      public static bool IsNotNull(this object obj)
      {
         return obj != null;
      }
   }
}
