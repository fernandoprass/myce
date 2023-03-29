namespace Myce.Wrappers.Contracts
{
   public interface IDirectoryWrapper
   {
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
