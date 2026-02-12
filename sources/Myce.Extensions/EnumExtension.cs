using System.Collections;
using System.ComponentModel;
using System.Reflection;

using System.Linq;

namespace Myce.Extensions
{
   /// <summary> Extensions for Enums type </summary>
   public static class EnumExtension
   {
      /// <summary> Return the enumerator description as a string </summary>
      /// <param name="value">Enumerator value</param>
      /// <returns></returns>
      public static string? GetDescription(this Enum? value)
      {
         if (value == null)
         {
            return null;
         }

         FieldInfo? fi = value.GetType().GetField(value.ToString());

         if (fi == null)
         {
            return value.ToString();
         }

         DescriptionAttribute[]? attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

         if (attributes != null && attributes.Any())
         {
            return attributes.First().Description;
         }

         return value.ToString();
      }
   }
}
