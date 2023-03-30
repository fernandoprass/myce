namespace Myce.Wrappers.Contracts
{
   public interface IDirectoryWrapper
   {
      /// <summary>
      /// Determines whether the given path refers to an existing directory on disk.
      /// </summary>
      /// <param name="path">The path to test.</param>
      /// <returns>TRUE if path refers to an existing directory; FALSE if the directory does not exist or an error occurs when trying to determine if the specified directory exists.</returns>
      bool Exists(string? path);

      /// <summary>
      /// Deletes an empty directory from a specified path.
      /// </summary>
      /// <param name="path">The name of the empty directory to remove. This directory must be writable and empty.</param>
      void Delete(string path);

      /// <summary>
      /// Deletes the specified directory and, if indicated, any subdirectories and files in the directory.
      /// </summary>
      /// <param name="path">The name of the directory to remove.</param>
      /// <param name="recursive">True to remove directories, subdirectories, and files in path; otherwise, FALSE.</param>
      void Delete(string path, bool recursive);

      /// <summary>
      /// Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
      /// </summary>
      /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
      /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search 
      /// pattern and option, or an empty array if no files are found. </returns>
      string[] GetFiles(string path);

      /// <summary>
      /// An array of the full names (including paths) for the files in the specified directory that match the specified search pattern, 
      /// or an empty array if no files are found.
      /// </summary>
      /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
      /// <param name="seacrhPattern">The search string to match against the names of files in path. This parameter can contain a
      /// combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
      /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search 
      /// pattern and option, or an empty array if no files are found. </returns>    
      string[] GetFiles(string path, string searchPattern);

      /// <summary>
      /// Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the 
      /// specified directory.
      /// </summary>
      /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
      /// <param name="seacrhPattern">The search string to match against the names of files in path. This parameter can contain a
      /// combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
      /// <param name="enumerationOptions">An object that describes the search and enumeration configuration to use.</param>
      /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search 
      /// pattern and option, or an empty array if no files are found. </returns>
      string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions);

      /// <summary>
      /// Returns the names of files (including their paths) that match the specified search pattern in the specified directory, 
      /// using a value to determine whether to search subdirectories.
      /// </summary>
      /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
      /// <param name="seacrhPattern">The search string to match against the names of files in path. This parameter can contain a
      /// combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
      /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all 
      /// subdirectories or only the current directory.</param>
      /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search 
      /// pattern and option, or an empty array if no files are found. </returns>
      string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
   }
}
