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
      /// On Windows-based desktop platforms, if PATH is null or an empty string (), the path information is returned unmodified.
      /// If EXTENSION is null, the returned string contains the specified path with its extension removed. If PATH has no extension, 
      /// and EXTENSION is not null, the returned path string contains EXTENSION appended to the end of PATH.
      /// </returns>
      string? ChangeExtension(string? path, string? extension);

      /// <summary>
      /// Combines an array of strings into a path.
      /// </summary>
      /// <param name="paths">An array of parts of the path.</param>
      /// <returns>The combined paths.</returns>
      string Combine(params string[] paths);

      /// <summary>
      /// Combines two strings into a path.
      /// </summary>
      /// <param name="path1">The first path to combine.</param>
      /// <param name="path2">The second path to combine.</param>
      /// <returns>The combined paths.</returns>
      string Combine(string path1, string path2);

      /// <summary>
      /// Combines three strings into a path.
      /// </summary>
      /// <param name="path1">The first path to combine.</param>
      /// <param name="path2">The second path to combine.</param>
      /// <param name="path3">The third path to combine.</param>
      /// <returns>The combined paths.</returns>
      string Combine(string path1, string path2, string path3);

      /// <summary>
      /// Combines four strings into a path.
      /// </summary>
      /// <param name="path1">The first path to combine.</param>
      /// <param name="path2">The second path to combine.</param>
      /// <param name="path3">The third path to combine.</param>
      /// <param name="path4">The fourth path to combine.</param>
      /// <returns>The combined paths.</returns>
      string Combine(string path1, string path2, string path3, string path4);

      /// <summary>
      /// Returns the file name and extension of a file path that is represented by a read-only character span.
      /// </summary>
      /// <param name="path">A read-only span that contains the path from which to obtain the file name and extension.</param>
      /// <returns>The characters after the last directory separator character in PATH.</returns>
      ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path);

      /// <summary>
      /// Returns the file name and extension of the specified path string.
      /// </summary>
      /// <param name="path">The path string from which to obtain the file name and extension.</param>
      /// <returns>The characters after the last directory separator character in PATH. If the last character of PATH is a directory 
      /// or volume separator character, this method returns Empty. If PATH is null, this method returns null.</returns>
      string? GetFileName(string? path);
   }
}
