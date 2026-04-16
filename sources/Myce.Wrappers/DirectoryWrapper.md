# DirectoryWrapper

`DirectoryWrapper` is a wrapper around the `System.IO.Directory` class, providing a mockable interface for file system operations.

## Methods

- **CreateDirectory(string path)**: Creates all directories and subdirectories in the specified path unless they already exist.
- **Delete(string path)**: Deletes an empty directory from a specified path.
- **Delete(string path, bool recursive)**: Deletes the specified directory and, if indicated, any subdirectories and files in the directory.
- **EnumerateDirectories(string path)**: Returns an enumerable collection of directory names in a specified path.
- **EnumerateDirectories(string path, string searchPattern)**: Returns an enumerable collection of directory names that match a search pattern in a specified path.
- **EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)**: Returns an enumerable collection of directory names that match a search pattern and enumeration options in a specified path.
- **EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)**: Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.
- **EnumerateFiles(string path)**: Returns an enumerable collection of full file names in a specified path.
- **EnumerateFiles(string path, string searchPattern)**: Returns an enumerable collection of full file names that match a search pattern in a specified path.
- **EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)**: Returns an enumerable collection of full file names that match a search pattern and enumeration options in a specified path.
- **EnumerateFiles(string path, string searchPattern, SearchOption searchOption)**: Returns an enumerable collection of full file names that match a search pattern in a specified path, and optionally searches subdirectories.
- **EnumerateFileSystemEntries(string path)**: Returns an enumerable collection of file-system entries in a specified path.
- **EnumerateFileSystemEntries(string path, string searchPattern)**: Returns an enumerable collection of file-system entries that match a search pattern in a specified path.
- **EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)**: Returns an enumerable collection of file-system entries that match a search pattern and enumeration options in a specified path.
- **EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)**: Returns an enumerable collection of file-system entries that match a search pattern in a specified path, and optionally searches subdirectories.
- **Exists(string? path)**: Determines whether the given path refers to an existing directory on disk.
- **GetCreationTime(string path)**: Gets the creation date and time of a directory.
- **GetCreationTimeUtc(string path)**: Gets the creation date and time, in Coordinated Universal Time (UTC), of a directory.
- **GetCurrentDirectory()**: Gets the current working directory of the application.
- **GetDirectories(string path)**: Returns the names of subdirectories (including their paths) in the specified directory.
- **GetDirectories(string path, string searchPattern)**: Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.
- **GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions)**: Returns the names of subdirectories (including their paths) that match the specified search pattern and enumeration options in the specified directory.
- **GetDirectories(string path, string searchPattern, SearchOption searchOption)**: Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.
- **GetDirectoryRoot(string path)**: Returns the volume information, root information, or both for the specified path.
- **GetFiles(string path)**: Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
- **GetFiles(string path, string searchPattern)**: Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
- **GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)**: Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
- **GetFiles(string path, string searchPattern, SearchOption searchOption)**: Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.
- **GetFileSystemEntries(string path)**: Returns the names of all files and subdirectories in a specified path.
- **GetFileSystemEntries(string path, string searchPattern)**: Returns the names of files and subdirectories that match a search pattern in a specified path.
- **GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions)**: Returns the names of files and subdirectories that match a specified search pattern and enumeration options in a specified path.
- **GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)**: Returns the names of files and subdirectories that match a search pattern in a specified path, and optionally searches subdirectories.
- **GetLastAccessTime(string path)**: Returns the date and time the specified file or directory was last accessed.
- **GetLastAccessTimeUtc(string path)**: Returns the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last accessed.
- **GetLastWriteTime(string path)**: Returns the date and time the specified file or directory was last written to.
- **GetLastWriteTimeUtc(string path)**: Returns the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last written to.
- **GetLogicalDrives()**: Retrieves the names of the logical drives on this computer in the form "<drive letter>:\".
- **GetParent(string path)**: Retrieves the parent directory of the specified path, including both absolute and relative paths.
- **Move(string sourceDirName, string destDirName)**: Moves a file or a directory and its contents to a new location.
- **SetCreationTime(string path, DateTime creationTime)**: Sets the creation date and time for the specified file or directory.
- **SetCreationTimeUtc(string path, DateTime creationTimeUtc)**: Sets the creation date and time, in Coordinated Universal Time (UTC), for the specified file or directory.
- **SetCurrentDirectory(string path)**: Sets the application's current working directory to the specified directory.
- **SetLastAccessTime(string path, DateTime lastAccessTime)**: Sets the date and time the specified file or directory was last accessed.
- **SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)**: Sets the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last accessed.
- **SetLastWriteTime(string path, DateTime lastWriteTime)**: Sets the date and time that the specified file or directory was last written to.
- **SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)**: Sets the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last written to.
