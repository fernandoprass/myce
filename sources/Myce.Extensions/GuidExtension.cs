using System.Diagnostics.CodeAnalysis;

namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for Guid type
   /// </summary>
   public static class GuidExtension
   {
      /// <summary> Return TRUE if the guid is empty</summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsEmpty(this Guid guid)
      {
         return guid == Guid.Empty;
      }

      /// <summary> Return TRUE if the guid is not empty</summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsNotEmpty(this Guid guid)
      {
         return guid != Guid.Empty;
      }

      /// <summary> Return TRUE if a nullable guid is empty</summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsEmpty(
#if !NETSTANDARD2_0
         [NotNullWhen(false)] 
#endif
         this Guid? guid)
      {
         return guid == null || guid.Value.IsEmpty();
      }

      /// <summary> Return TRUE if a nullable guid is empty</summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static bool IsNotEmpty(
#if !NETSTANDARD2_0
         [NotNullWhen(true)] 
#endif
         this Guid? guid)
      {
         return guid != null && guid.Value.IsNotEmpty();
      }
   }
}
