namespace Myce.Wrappers.Contracts
{
   public interface IPathWrapper
   {
      /// <summary>
      /// Changes the extension of a path string.
      /// </summary>
      /// <param name="path">The path information to modify.</param>
      /// <param name="extension">The new extension (with or without a leading period). Specify null to remove an existing extension from path.</param>
      /// <returns>
      /// The modified path information. 
      /// On Windows-based desktop platforms, if path is null or an empty string (), the path information is returned unmodified.
      /// If EXTENSION is null, the returned string contains the specified path with its extension removed. If path has no extension, 
      /// and EXTENSION is not null, the returned path string contains EXTENSION appended to the end of path.
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
      /// Returns the directory information for the specified path.
      /// </summary>
      /// <param name="path">The path of a file or directory.</param>
      /// <returns>Directory information for path, or null if path denotes a root directory or is null. 
      /// Returns Empty if path does not contain directory information.</returns>
      string? GetDirectoryName(string? path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns the directory information for the specified path represented by a character span.
      /// </summary>
      /// <param name="path">The path to retrieve the directory information from.</param>
      /// <returns>Directory information for path, or an empty span if path is null, an empty span, or a root (such as \, C:, or \server\share).</returns>
      ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path);
#endif

      /// <summary>
      /// Returns the extension (including the period ".") of the specified path string.
      /// </summary>
      /// <param name="path">The path string from which to get the extension.</param>
      /// <returns>The extension of the specified path (including the period "."), or null, or Empty. 
      /// If path is null, returns null. If path does not have extension information, returns Empty.</returns>
      string? GetExtension(string? path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns the file name and extension of a file path that is represented by a read-only character span.
      /// </summary>
      /// <param name="path">The file path from which to get the extension.</param>
      /// <returns>The extension of the specified path (including the period, "."), or Empty if path does not have extension information.</returns>
      ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns the extension of a file path that is represented by a read-only character span.
      /// </summary>
      /// <param name="path">A read-only span that contains the path from which to obtain the file name and extension.</param>
      /// <returns>The characters after the last directory separator character in path.</returns>   
      ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path);
#endif

      /// <summary>
      /// Returns the file name and extension of the specified path string.
      /// </summary>
      /// <param name="path">The path string from which to obtain the file name and extension.</param>
      /// <returns>The characters after the last directory separator character in path. If the last character of path is a directory 
      /// or volume separator character, this method returns Empty. If path is null, this method returns null.</returns>
      string? GetFileName(string? path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns the file name without the extension of a file path that is represented by a read-only character span.
      /// </summary>
      /// <param name="path">A read-only span that contains the path from which to obtain the file name without the extension.</param>
      /// <returns>The characters in the read-only span returned by GetFileName(ReadOnlySpan<Char>), minus the last period (.) and all characters following it.</returns>
      ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path);
#endif

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
      /// <returns>The fully qualified location of path, such as "C:\MyFile.txt".</returns>
      string GetFullPath(string path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns an absolute path from a relative path and a fully qualified base path.
      /// </summary>
      /// <param name="path">A relative path to concatenate to BASEpath.</param>
      /// <param name="basepath">The beginning of a fully qualified path.</param>
      /// <returns>The absolute path.</returns>
      string GetFullPath(string path, string basepath);
#endif

      /// <summary>
      /// Gets the root directory information from the path contained in the specified string.
      /// </summary>
      /// <param name="path">A string containing the path from which to obtain root directory information.</param>
      /// <returns>The root directory of path if it is rooted. 
      /// -or-
      /// Empty if path does not contain root directory information.
      /// -or-
      /// NULL if path is null or is effectively empty.
      /// </returns>
      string? GetPathRoot(string? path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Gets the root directory information from the path contained in the specified character span.
      /// </summary>
      /// <param name="path">A read-only span of characters containing the path from which to obtain root directory information.</param>
      /// <returns>A read-only span of characters containing the root directory of path.</returns>
      ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path);
#endif

      /// <summary>
      /// Returns a random folder name or file name.
      /// </summary>
      /// <returns>A random folder name or file name.</returns>
      string GetRandomFileName();

      /// <summary>
      /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
      /// </summary>
      /// <returns>The full path of the temporary file.</returns>
      string GetTempFileName();

      /// <summary>
      /// Returns the path of the current user's temporary folder.
      /// </summary>
      /// <returns>The path to the temporary folder, ending with a DirectorySeparatorChar.</returns>
      string GetTempPath();

      /// <summary>
      /// Determines whether a path includes a file name extension.
      /// </summary>
      /// <param name="path">The path to search for an extension.</param>
      /// <returns>TRUE if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, FALSE.</returns>
      bool HasExtension(string? path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Determines whether a path includes a file name extension.
      /// </summary>
      /// <param name="path">The path to search for an extension.</param>
      /// <returns>TRUE if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, FALSE.</returns>
      bool HasExtension(ReadOnlySpan<char> path);
#endif

      /// <summary>
      /// Gets a value indicating whether the specified path string contains a root.
      /// </summary>
      /// <param name="path">The path to test.</param>
      /// <returns>TRUE if path contains a root; otherwise, FALSE.</returns>
      bool IsPathRooted(string? path);

#if !NETSTANDARD2_0
      /// <summary>
      /// Gets a value indicating whether the specified path string contains a root.
      /// </summary>
      /// <param name="path">The path to test.</param>
      /// <returns>TRUE if path contains a root; otherwise, FALSE.</returns>
      bool IsPathRooted(ReadOnlySpan<char> path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns a value indicating whether the specified path is fully qualified.
      /// </summary>
      /// <param name="path">The path to test.</param>
      /// <returns>TRUE if path is a fully qualified path; otherwise, FALSE.</returns>
      bool IsPathFullyQualified(string path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns a value indicating whether the specified path is fully qualified.
      /// </summary>
      /// <param name="path">The path to test.</param>
      /// <returns>TRUE if path is a fully qualified path; otherwise, FALSE.</returns>
      bool IsPathFullyQualified(ReadOnlySpan<char> path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Concatenates an array of paths into a single path.
      /// </summary>
      /// <param name="paths">An array of paths.</param>
      /// <returns>The concatenated path.</returns>
      string Join(params string?[] paths);

      /// <summary>
      /// Concatenates two paths into a single path.
      /// </summary>
      /// <param name="path1">The first path to join.</param>
      /// <param name="path2">The second path to join.</param>
      /// <returns>The concatenated path.</returns>
      string Join(string? path1, string? path2);

      /// <summary>
      /// Concatenates three paths into a single path.
      /// </summary>
      /// <param name="path1">The first path to join.</param>
      /// <param name="path2">The second path to join.</param>
      /// <param name="path3">The third path to join.</param>
      /// <returns>The concatenated path.</returns>
      string Join(string? path1, string? path2, string? path3);

      /// <summary>
      /// Concatenates four paths into a single path.
      /// </summary>
      /// <param name="path1">The first path to join.</param>
      /// <param name="path2">The second path to join.</param>
      /// <param name="path3">The third path to join.</param>
      /// <param name="path4">The fourth path to join.</param>
      /// <returns>The concatenated path.</returns>
      string Join(string? path1, string? path2, string? path3, string? path4);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Concatenates two path components into a single path.
      /// </summary>
      /// <param name="path1">A character span that contains the first path to join.</param>
      /// <param name="path2">A character span that contains the second path to join.</param>
      /// <returns>The concatenated path.</returns>

      string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2);

      /// <summary>
      /// Concatenates three path components into a single path.
      /// </summary>
      /// <param name="path1">A character span that contains the first path to join.</param>
      /// <param name="path2">A character span that contains the second path to join.</param>
      /// <param name="path3">A character span that contains the third path to join.</param>
      /// <returns>The concatenated path.</returns>
      string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Concatenates four path components into a single path.
      /// </summary>
      /// <param name="path1">A character span that contains the first path to join.</param>
      /// <param name="path2">A character span that contains the second path to join.</param>
      /// <param name="path3">A character span that contains the third path to join.</param>
      /// <param name="path4">A character span that contains the fourth path to join.</param>
      /// <returns>The concatenated path.</returns>
      string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, ReadOnlySpan<char> path4);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns a relative path from one path to another.
      /// </summary>
      /// <param name="relativeTo">The source path the result should be relative to. This path is always considered to be a directory.</param>
      /// <param name="path">The destination path.</param>
      /// <returns>The relative path, or path if the paths don't share the same root.</returns>
      string GetRelativePath(string relativeTo, string path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Trims the trailing directory separator character from a path.
      /// </summary>
      /// <param name="path">The path to trim.</param>
      /// <returns>The path without the trailing directory separator. If the path is null, empty, or a root directory, the path is returned unchanged.</returns>
      string TrimEndingDirectorySeparator(string path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Trims the trailing directory separator character from a path.
      /// </summary>
      /// <param name="path">The path to trim.</param>
      /// <returns>The path without the trailing directory separator. If the path is null, empty, or a root directory, the path is returned unchanged.</returns>
      ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns a value indicating if the path ends in a directory separator.
      /// </summary>
      /// <param name="path">The path to check.</param>
      /// <returns>TRUE if the path ends in a directory separator; otherwise, FALSE.</returns>
      bool EndsInDirectorySeparator(string path);
#endif

#if !NETSTANDARD2_0
      /// <summary>
      /// Returns a value indicating if the path ends in a directory separator.
      /// </summary>
      /// <param name="path">The path to check.</param>
      /// <returns>TRUE if the path ends in a directory separator; otherwise, FALSE.</returns>
      bool EndsInDirectorySeparator(ReadOnlySpan<char> path);
#endif

      /// <summary>
      /// Gets an array containing the characters that are not allowed in file names.
      /// </summary>
      /// <returns>An array containing the characters that are not allowed in file names.</returns>
      char[] GetInvalidFileNameChars();

      /// <summary>
      /// Gets an array containing the characters that are not allowed in path names.
      /// </summary>
      /// <returns>An array containing the characters that are not allowed in path names.</returns>
      char[] GetInvalidPathChars();
   }
}
