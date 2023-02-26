namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for ICollections type
   /// </summary>
   public static class CollectionExtensions
   {
      /// <summary>
      /// Add an item to a collection if the item is not null
      /// </summary>
      /// <param name="collection">The collection</param>
      /// <param name="item">The item</param>
      public static void AddIfNotNull<T>(this ICollection<T> collection, T item)
      {
         if (item.IsNotNull())
         {
            collection.Add(item);
         }

      }

      /// <summary>
      /// Add a collection of items to a collection if the collections of items has data
      /// </summary>
      /// <param name="collection">The collection</param>
      /// <param name="items">The items collection</param>
      public static void AddRangeIfHasData<T>(this ICollection<T> collection, IEnumerable<T> items)
      {
         if (items.HasData())
         {
            foreach (var item in items)
            {
               collection.Add(item);
            }

         }

      }
   }
}
