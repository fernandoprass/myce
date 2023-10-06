namespace Myce.Wrappers.Contracts
{
   internal interface IPathWrapper
   {
      /// <summary>
      /// Changes the extension of a path string.
      /// </summary>
      /// <param name="path">The path information to modify.</param>
      /// <param name="extension">The new extension (with or without a leading period). Specify null to remove an existing extension from path.</param>
      /// <returns>
      /// The modified path information. 
      /// On Windows-based desktop platforms, if path is null or an empty string (), the path information is returned unmodified.
      /// If extension is null, the returned string contains the specified path with its extension removed.If path has no extension, 
      /// and extension is not null, the returned path string contains extension appended to the end of path.
      /// </returns>

   }
}
