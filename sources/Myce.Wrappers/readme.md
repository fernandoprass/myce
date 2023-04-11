# MYCE.Wrappers
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of wrappers for the most common System.IO classes, which is especially useful for mock methods in unit tests.

## Library
This is a simple wrapper for some of the most used classes of System.IO Namespace. For more information, including examples, visit 
[Microsoft Learn - System.IO Namespace](https://learn.microsoft.com/en-us/dotnet/api/system.io). All method descriptions below were copied from there,
if you find any information inconsistent or out of date, please contact us.

- Directory
    - CreateDirectory (string path) - Creates all directories and subdirectories in the specified path unless they already exist.
    - CreateDirectory (string path, UnixFileMode unixCreateMode) - Creates all directories and subdirectories in the specified path with the specified permissions unless they already exist.
    - Delete (string path) - Deletes an empty directory from a specified path.
    - Delete (string path, bool recursive) - Deletes the specified directory and, if indicated, any subdirectories and files in the directory.
    - Exists (string? path) - Determines whether the given path refers to an existing directory on disk.
    - GetFiles(string path) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern, SearchOption searchOption) - Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.

- File
    - Copy(string sourceFileName, string destFileName) - Copies an existing file to a new file.
    - Copy(string sourceFileName, string destFileName, bool overwrite) - Copies an existing file to a new file. Overwriting a file of the same name is allowed.
    - Create(String) - Creates or overwrites a file in the specified path.
    - Create(String, Int32)	- Creates or overwrites a file in the specified path, specifying a buffer size.
    - Create(String, Int32, FileOptions) - Creates or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.
    - Delete (string path) - Deletes the specified file.
    - Exists(string path) - Determines whether the specified file exists.
    - Move(string sourceFileName, string destFileName) - Moves a specified file to a new location, providing the option to specify a new file name.
    - Move(string sourceFileName, string destFileName, bool overwrite) - Moves a specified file to a new location, providing the options to specify a new file name and to overwrite the destination file if it already exists.
    - ReadAllText(string path) - Opens a text file, reads all the text in the file, and then closes the file.
    - ReadAllTextAsync(string path, CancellationToken cancellationToken = default) - Asynchronously opens a text file, reads all the text in the file, and then closes the file.
    - ReadAllText(string path, Encoding encoding) - Opens a file, reads all text in the file with the specified encoding, and then closes the file.
    - ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default) - Asynchronously opens a text file, reads all text in the file with the specified encoding, and then closes the file.
    - WriteAllText(string path, string? contents) - Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllText(string path, string? contents, Encoding encoding) - Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default) - Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default) - Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.

## Dependencies
- None

## Attention:
None of this library's methods have unit test for the simple fact that, as the name implies, they are wrappers of original methods from the System.IO namespace.

## Contributions
Contributions are welcome on this library. If you use a class or method from the System.IO namespace that is not already here, create a Pull Request and submit it.

Developed by Fernando Prass