using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;

namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for IEnumerable type
   /// </summary>
   public static class EnumerableExtensions
   {
      /// <summary>
      /// Return TRUE if the collection object is not null and has any record
      /// </summary>
      /// <param name="enumerable">The enumerable</param>
      public static bool HasData<T>(this IEnumerable<T> enumerable)
      {
         return enumerable.IsNotNull() && enumerable.Any();
      }

      /// <summary>
      /// Check for duplicates in a collection
      /// </summary>
      /// <typeparam name="T">The Enumerable Type</typeparam>
      /// <param name="enumerable">The enumerable</param>
      /// <returns></returns>
      public static bool ContainsDuplicates<T>(this IEnumerable<T> enumerable)
      {
         var knownElements = new HashSet<T>();

         if (enumerable.HasData())
         {
            foreach (T element in enumerable)
            {
               if (!knownElements.Add(element))
               {
                  return true;
               }
            }
         }

         return false;
      }
   }
}
