namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for IEnumerable type
   /// </summary>
   public static class EnumerableExtensions
   {
      /// <summary>
      /// Check for duplicates in an enumerable
      /// </summary>
      /// <param name="enumerable">The enumerable</param>
      /// <returns></returns>
      public static bool ContainsDuplicates<T>(this IEnumerable<T> enumerable)
      {
         if (enumerable.IsNull())
         {
            return false;
         }

         var knownElements = new HashSet<T>(enumerable.Count());

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

      /// <summary>
      /// Splits an enumerable into chunks of a specified size.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="enumerable">The enumerable</param>
      /// <param name="chunkSize">The chunk size</param>
      /// <returns></returns>
      /// <exception cref="ArgumentException"></exception>
      public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> enumerable, int chunkSize)
      {
         if (chunkSize <= 0)
         {
            throw new ArgumentException("chunkSize must be greater than 0.");
         }

         while (enumerable.Any())
         {
            yield return enumerable.Take(chunkSize);
            enumerable = enumerable.Skip(chunkSize);
         }
      }

      /// <summary>
      /// Return an enumerable of elements distinct by specific property
      /// </summary>
      /// <param name="enumerable">The enumerable</param>
      /// <param name="keySelector">The distinct property</param>
      /// <returns></returns>
      public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> enumerable, Func<TSource, TKey> keySelector)
      {
         HashSet<TKey> seenKeys = new HashSet<TKey>(enumerable.Count());
         foreach (TSource element in enumerable)
         {
            if (seenKeys.Add(keySelector(element)))
            {
               yield return element;
            }
         }
      }

      /// <summary>
      /// The foreach loop
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="enumerable">The enumerable</param>
      /// <param name="action">The action to be executed</param>
      /// <exception cref="ArgumentNullException">Action is mandatory</exception>
      public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
      {
         if (action == null)
         {
            throw new ArgumentNullException("action");
         }
         foreach (var item in enumerable)
         {
            action(item);
         }
      }

      /// <summary>
      /// Return the duplicate elements in an enumerable
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="enumerable"></param>
      /// <returns></returns>
      public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> enumerable)
      {
         int size = enumerable.Count();
         HashSet<T> itemsSeen = new HashSet<T>(size);
         HashSet<T> itemsYielded = new HashSet<T>(size/2);

         foreach (T item in enumerable)
         {
            if (!itemsSeen.Add(item))
            {
               if (itemsYielded.Add(item))
               {
                  yield return item;
               }
            }
         }
      }

      /// <summary>
      /// Return TRUE if the enumerable is not null and has any record
      /// </summary>
      /// <param name="enumerable">The enumerable</param>
      public static bool HasData<T>(this IEnumerable<T> enumerable)
      {
         return enumerable.IsNotNull() && enumerable.Any();
      }

      /// <summary>
      /// Return TRUE if the enumerable is not null and has any record
      /// </summary>
      /// <param name="enumerable">The enumerable</param>
      public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
      {
         return enumerable.IsNull() || !enumerable.Any();
      }
   }
}
