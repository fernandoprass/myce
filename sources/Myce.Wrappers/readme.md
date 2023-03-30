# MYCE.Wrappers
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of wrappers for the most common System.IO classes, which is especially useful for mock methods in unit tests.

## Library
Directory
    - Delete (string path) - Deletes an empty directory from a specified path.
    - Delete (string path, bool recursive) - Deletes the specified directory and, if indicated, any subdirectories and files in the directory.
    - Exists (string? path) - Determines whether the given path refers to an existing directory on disk.
    - GetFiles(string path) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions) - Returns the names of files (including their paths) that match the specified search pattern and enumeration options in the specified directory.
    - GetFiles(string path, string searchPattern, SearchOption searchOption) - Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.

- File
    - Exists(string path) - Determines whether the specified file exists.
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

Develeped by Fernando Prass