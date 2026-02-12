# MYCE.Wrappers
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of wrappers for the most common System.IO classes, which is especially useful for mock methods in unit tests.

Supports `net6.0`, `net8.0`, `net9.0`, and `netstandard2.0`.

## Library
This is a simple wrapper for some of the most used classes of System.IO Namespace. For more information, including examples, visit 
[Microsoft Learn - System.IO Namespace](https://learn.microsoft.com/en-us/dotnet/api/system.io). All method descriptions below were copied from there,
if you find any information inconsistent or out of date, please contact us.

### Directory
    - CreateDirectory(string path) - Creates all directories and subdirectories in the specified path unless they already exist.
    - Delete(string path) - Deletes an empty directory from a specified path.
    - Delete(string path, bool recursive) - Deletes the specified directory and, if indicated, any subdirectories and files in the directory.
    - EnumerateDirectories(string path) - Returns an enumerable collection of directory names in a specified path.
    - EnumerateDirectories(string path, string searchPattern) - Returns an enumerable collection of directory names that match a search pattern in a specified path.
    - EnumerateDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns an enumerable collection of directory names that match a search pattern and enumeration options in a specified path.
    - EnumerateDirectories(string path, string searchPattern, SearchOption searchOption) - Returns an enumerable collection of directory names that match a search pattern in a specified path, and optionally searches subdirectories.
    - EnumerateFiles(string path) - Returns an enumerable collection of full file names in a specified path.
    - EnumerateFiles(string path, string searchPattern) - Returns an enumerable collection of full file names that match a search pattern in a specified path.
    - EnumerateFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns an enumerable collection of full file names that match a search pattern and enumeration options in a specified path.
    - EnumerateFiles(string path, string searchPattern, SearchOption searchOption) - Returns an enumerable collection of full file names that match a search pattern in a specified path, and optionally searches subdirectories.
    - EnumerateFileSystemEntries(string path) - Returns an enumerable collection of file-system entries in a specified path.
    - EnumerateFileSystemEntries(string path, string searchPattern) - Returns an enumerable collection of file-system entries that match a search pattern in a specified path.
    - EnumerateFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns an enumerable collection of file-system entries that match a search pattern and enumeration options in a specified path.
    - EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption) - Returns an enumerable collection of file-system entries that match a search pattern in a specified path, and optionally searches subdirectories.
    - Exists(string? path) - Determines whether the given path refers to an existing directory on disk.
    - GetCreationTime(string path) - Gets the creation date and time of a directory.
    - GetCreationTimeUtc(string path) - Gets the creation date and time, in Coordinated Universal Time (UTC), of a directory.
    - GetCurrentDirectory() - Gets the current working directory of the application.
    - GetDirectories(string path) - Returns the names of subdirectories (including their paths) in the specified directory.
    - GetDirectories(string path, string searchPattern) - Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory.
    - GetDirectories(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns the names of subdirectories (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetDirectories(string path, string searchPattern, SearchOption searchOption) - Returns the names of subdirectories (including their paths) that match the specified search pattern in the specified directory, and optionally searches subdirectories.
    - GetDirectoryRoot(string path) - Returns the volume information, root information, or both for the specified path.
    - GetFiles(string path) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern, SearchOption searchOption) - Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.
    - GetFileSystemEntries(string path) - Returns the names of all files and subdirectories in a specified path.
    - GetFileSystemEntries(string path, string searchPattern) - Returns the names of files and subdirectories that match a search pattern in a specified path.
    - GetFileSystemEntries(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns the names of files and subdirectories that match a specified search pattern and enumeration options in a specified path.
    - GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption) - Returns the names of files and subdirectories that match a search pattern in a specified path, and optionally searches subdirectories.
    - GetLastAccessTime(string path) - Returns the date and time the specified file or directory was last accessed.
    - GetLastAccessTimeUtc(string path) - Returns the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last accessed.
    - GetLastWriteTime(string path) - Returns the date and time the specified file or directory was last written to.
    - GetLastWriteTimeUtc(string path) - Returns the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last written to.
    - GetLogicalDrives() - Retrieves the names of the logical drives on this computer in the form "<drive letter>:\".
    - GetParent(string path) - Retrieves the parent directory of the specified path, including both absolute and relative paths.
    - Move(string sourceDirName, string destDirName) - Moves a file or a directory and its contents to a new location.
    - SetCreationTime(string path, DateTime creationTime) - Sets the creation date and time for the specified file or directory.
    - SetCreationTimeUtc(string path, DateTime creationTimeUtc) - Sets the creation date and time, in Coordinated Universal Time (UTC), for the specified file or directory.
    - SetCurrentDirectory(string path) - Sets the application's current working directory to the specified directory.
    - SetLastAccessTime(string path, DateTime lastAccessTime) - Sets the date and time the specified file or directory was last accessed.
    - SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) - Sets the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last accessed.
    - SetLastWriteTime(string path, DateTime lastWriteTime) - Sets the date and time that the specified file or directory was last written to.
    - SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) - Sets the date and time, in Coordinated Universal Time (UTC), that the specified file or directory was last written to.

### File
    - Copy(string sourceFileName, string destFileName) - Copies an existing file to a new file.
    - Copy(string sourceFileName, string destFileName, bool overwrite) - Copies an existing file to a new file. Overwriting a file of the same name is allowed.
    - Create(string path) - Creates or overwrites a file in the specified path.
    - Create(string path, int bufferSize) - Creates or overwrites a file in the specified path, specifying a buffer size.
    - Create(string path, int bufferSize, FileOptions options) - Creates or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.
    - CreateText(string path) - Creates or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.
    - Delete(string path) - Deletes the specified file.
    - Exists(string path) - Determines whether the specified file exists.
    - Move(string sourceFileName, string destFileName) - Moves a specified file to a new location, providing the option to specify a new file name.
    - Move(string sourceFileName, string destFileName, bool overwrite) - Moves a specified file to a new location, providing the options to specify a new file name and to overwrite the destination file if it already exists.
    - Open(string path, FileMode mode) - Opens a FileStream on the specified path with read/write access with no sharing.
    - Open(string path, FileStreamOptions options) - Initializes a new instance of the FileStream class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, additional file options and the allocation size.
    - Open(string path, FileMode mode, FileAccess access) - Opens a FileStream on the specified path, with the specified mode and access with no sharing.
    - Open(string path, FileMode mode, FileAccess access, FileShare share) - Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
    - ReadAllText(string path) - Opens a text file, reads all the text in the file, and then closes the file.
    - ReadAllText(string path, Encoding encoding) - Opens a file, reads all text in the file with the specified encoding, and then closes the file.
    - ReadAllTextAsync(string path, CancellationToken cancellationToken = default) - Asynchronously opens a text file, reads all the text in the file, and then closes the file.
    - ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default) - Asynchronously opens a text file, reads all text in the file with the specified encoding, and then closes the file.
    - WriteAllText(string path, string? contents) - Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllText(string path, string? contents, Encoding encoding) - Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default) - Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default) - Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.

### Path
    - ChangeExtension(string? path, string? extension) - Changes the extension of a path string.
    - Combine(params string[] paths) - Combines an array of strings into a path.
    - Combine(string path1, string path2) - Combines two strings into a path.
    - Combine(string path1, string path2, string path3) - Combines three strings into a path.
    - Combine(string path1, string path2, string path3, string path4) - Combines four strings into a path.
    - EndsInDirectorySeparator(ReadOnlySpan<char> path) - Returns a value that indicates whether the path, represented by a read-only character span, ends in a directory separator.
    - EndsInDirectorySeparator(string path) - Returns a value that indicates whether the path, specified as a string, ends in a directory separator.
    - GetDirectoryName(ReadOnlySpan<char> path) - Returns the directory information for the specified path represented by a character span.
    - GetDirectoryName(string? path) - Returns the directory information for the specified path.
    - GetExtension(ReadOnlySpan<char> path) - Returns the file name and extension of a file path that is represented by a read-only character span.
    - GetExtension(string? path) - Returns the extension (including the period ".") of the specified path string.
    - GetFileName(ReadOnlySpan<char> path) - Returns the file name and extension of a file path that is represented by a read-only character span.
    - GetFileName(string? path) - Returns the file name and extension of the specified path string.
    - GetFileNameWithoutExtension(ReadOnlySpan<char> path) - Returns the file name without the extension of a file path that is represented by a read-only character span.
    - GetFileNameWithoutExtension(string path) - Returns the file name of the specified path string without the extension.
    - GetFullPath(string path) - Returns the absolute path for the specified path string.
    - GetFullPath(string path, string basePath) - Returns an absolute path from a relative path and a fully qualified base path.
    - GetInvalidFileNameChars() - Gets the array of characters that are not allowed in file names.
    - GetInvalidPathChars() - Gets the array of characters that are not allowed in path names.
    - GetPathRoot(ReadOnlySpan<char> path) - Gets the root directory information from the path contained in the specified character span.
    - GetPathRoot(string? path) - Gets the root directory information from the path contained in the specified string.
    - GetRandomFileName() - Returns a random folder name or file name.
    - GetRelativePath(string relativeTo, string path) - Returns a relative path from one path to another.
    - GetTempFileName() - Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
    - GetTempPath() - Returns the path of the current user's temporary folder.
    - HasExtension(ReadOnlySpan<char> path) - Determines whether the path includes a file name extension.
    - HasExtension(string? path) - Determines whether a path includes a file name extension.
    - IsPathFullyQualified(ReadOnlySpan<char> path) - Returns a value that indicates whether the specified path, represented by a read-only character span, is fully qualified.
    - IsPathFullyQualified(string path) - Returns a value that indicates whether the specified path is fully qualified.
    - IsPathRooted(ReadOnlySpan<char> path) - Returns a value that indicates whether the specified path, represented by a read-only character span, contains a root.
    - IsPathRooted(string? path) - Returns a value that indicates whether the specified path contains a root.
    - Join(params string?[] paths) - Concatenates an array of paths into a single path.
    - Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2) - Concatenates two path components into a single path.
    - Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3) - Concatenates three path components into a single path.
    - Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, ReadOnlySpan<char> path4) - Concatenates four path components into a single path.
    - Join(string? path1, string? path2) - Concatenates two paths into a single path.
    - Join(string? path1, string? path2, string? path3) - Concatenates three paths into a single path.
    - Join(string? path1, string? path2, string? path3, string? path4) - Concatenates four paths into a single path.
    - TrimEndingDirectorySeparator(ReadOnlySpan<char> path) - Trims one trailing directory separator beyond the root of the specified path.
    - TrimEndingDirectorySeparator(string path) - Trims one trailing directory separator beyond the root of the specified path.

## Dependencies
- None

## Compatibility
This library supports `netstandard2.0`, but some modern APIs are excluded in that target to ensure compatibility:
- **Span-based methods**: Methods using `ReadOnlySpan<char>` (e.g., in `Path`) are not available in `netstandard2.0`.
- **Advanced Options**: Methods using `EnumerationOptions` or `FileStreamOptions` are excluded.
- **Modern Path/File methods**: `Join`, `GetRelativePath`, `ReadAllTextAsync`, `WriteAllTextAsync`, and `Move(overwrite)` are not available in `netstandard2.0`.

## Attention:
None of this library's methods have unit test for the simple fact that, as the name implies, they are wrappers of original methods from the System.IO namespace.

## Contributions
Contributions are welcome on this library. If you use a class or method from the System.IO namespace that is not already here, create a Pull Request and submit it.

Developed by Fernando Prass