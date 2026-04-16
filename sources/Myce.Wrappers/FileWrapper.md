# FileWrapper

`FileWrapper` is a wrapper around the `System.IO.File` class, providing a mockable interface for file system operations.

## Methods

- **AppendAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)**: Asynchronously opens a specified file, appends the specified byte array to the file, and then closes the file.
- **AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)**: Asynchronously opens a file, appends the specified lines to the file, and then closes the file.
- **AppendAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)**: Asynchronously opens a file, appends the specified string to the file, and then closes the file.
- **Copy(string sourceFileName, string destFileName)**: Copies an existing file to a new file.
- **Copy(string sourceFileName, string destFileName, bool overwrite)**: Copies an existing file to a new file. Overwriting a file of the same name is allowed.
- **Create(string path)**: Creates or overwrites a file in the specified path.
- **Create(string path, int bufferSize)**: Creates or overwrites a file in the specified path, specifying a buffer size.
- **Create(string path, int bufferSize, FileOptions options)**: Creates or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.
- **CreateText(string path)**: Creates or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.
- **Delete(string path)**: Deletes the specified file.
- **Exists(string path)**: Determines whether the specified file exists.
- **Move(string sourceFileName, string destFileName)**: Moves a specified file to a new location, providing the option to specify a new file name.
- **Move(string sourceFileName, string destFileName, bool overwrite)**: Moves a specified file to a new location, providing the options to specify a new file name and to overwrite the destination file if it already exists.
- **Open(string path, FileMode mode)**: Opens a FileStream on the specified path with read/write access with no sharing.
- **Open(string path, FileStreamOptions options)**: Initializes a new instance of the FileStream class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, additional file options and the allocation size.
- **Open(string path, FileMode mode, FileAccess access)**: Opens a FileStream on the specified path, with the specified mode and access with no sharing.
- **Open(string path, FileMode mode, FileAccess access, FileShare share)**: Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
- **ReadAllBytesAsync(string path, CancellationToken cancellationToken = default)**: Asynchronously opens a binary file, reads the contents of the file into a byte array, and then closes the file.
- **ReadAllLinesAsync(string path, CancellationToken cancellationToken = default)**: Asynchronously opens a text file, reads all lines of the file, and then closes the file.
- **ReadAllText(string path)**: Opens a text file, reads all the text in the file, and then closes the file.
- **ReadAllText(string path, Encoding encoding)**: Opens a file, reads all text in the file with the specified encoding, and then closes the file.
- **ReadAllTextAsync(string path, CancellationToken cancellationToken = default)**: Asynchronously opens a text file, reads all the text in the file, and then closes the file.
- **ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)**: Asynchronously opens a text file, reads all text in the file with the specified encoding, and then closes the file.
- **ReadLinesAsync(string path, CancellationToken cancellationToken = default)**: Asynchronously read the lines of a file.
- **WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)**: Asynchronously creates a new file, writes the specified byte array to the file, and then closes the file.
- **WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)**: Asynchronously creates a new file, writes the specified lines to the file, and then closes the file.
- **WriteAllText(string path, string? contents)**: Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
- **WriteAllText(string path, string? contents, Encoding encoding)**: Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
- **WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)**: Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
- **WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default)**: Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.

> [!NOTE]
> Some methods have additional overloads for `ReadOnlyMemory<byte>`, `ReadOnlyMemory<char>`, and specific `Encoding`. These are available in newer .NET targets (.NET 8.0/9.0+). Check the interface for full details.
