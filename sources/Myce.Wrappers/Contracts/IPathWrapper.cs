using System.IO;

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

      /// <summary>
      /// Returns the file name without the extension of a file path that is represented by a read-only character span.
      /// </summary>
      /// <param name="path">A read-only span that contains the path from which to obtain the file name without the extension.</param>
      /// <returns>The characters in the read-only span returned by GetFileName(ReadOnlySpan<Char>), minus the last period (.) and all characters following it.</returns>
      ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path);

      /// <summary>
      /// Returns the file name of the specified path string without the extension.
      /// </summary>
      /// <param name="path">The path of the file.</param>
      /// <returns>The string returned by GetFileName(ReadOnlySpan<Char>), minus the last period (.) and all characters following it.</returns>
      string? GetFileNameWithoutExtension(string? path);

      /// <summary>
      /// Returns the absolute path for the specified path string.
      /// </summary>
      /// <param name="path">The file or directory for which to obtain absolute path information.</param>
      /// <returns>The fully qualified location of PATH, such as "C:\MyFile.txt".</returns>
      string GetFullPath(string path);

      /// <summary>
      /// Returns an absolute path from a relative path and a fully qualified base path.
      /// </summary>
      /// <param name="path">A relative path to concatenate to BASEPATH.</param>
      /// <param name="basePath">The beginning of a fully qualified path.</param>
      /// <returns>The absolute path.</returns>
      string GetFullPath(string path, string basePath);

      /// <summary>
      /// Gets the root directory information from the path contained in the specified string.
      /// </summary>
      /// <param name="path">A string containing the path from which to obtain root directory information.</param>
      /// <returns>The root directory of PATH if it is rooted. 
      /// -or-
      /// Empty if PATH does not contain root directory information.
      /// -or-
      /// NULL if PATH is null or is effectively empty.
      /// </returns>
      string? GetPathRoot(string? path);

      /// <summary>
      /// Gets the root directory information from the path contained in the specified character span.
      /// </summary>
      /// <param name="path">A read-only span of characters containing the path from which to obtain root directory information.</param>
      /// <returns>A read-only span of characters containing the root directory of PATH.</returns>
      ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path);

      /// <summary>
      /// Returns the path of the current user's temporary folder.
      /// </summary>
      /// <returns>The path to the temporary folder, ending with a DirectorySeparatorChar.</returns>
      string GetTempPath();
   }
}
