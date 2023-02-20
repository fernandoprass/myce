namespace Myce.Extensions
{
   /// <summary>
   /// Extensions for ICollections type
   /// </summary>
   public static class CollectionExtensions
   {
      /// <summary>
      /// Add a item to a collection of the item is not null
      /// </summary>
      /// <param name="collection">The collection</param>
      /// <param name="item">The item</param>
      public static void AddIfNotNull<T>(this ICollection<T> collection, T item)
      {
         if (item.IsNotNull())
            collection.Add(item);
      }
   }
}
