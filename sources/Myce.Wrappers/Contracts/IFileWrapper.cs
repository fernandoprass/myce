using System.Text;

namespace Myce.Wrappers.Contracts
{
   public interface IFileWrapper
   {
      /// <summary>
      /// Copies an existing file to a new file.
      /// </summary>
      /// <param name="sourceFileName">The file to copy.</param>
      /// <param name="destFileName">The name of the destination file. This cannot be a directory.</param>
      void Copy(string sourceFileName, string destFileName);

      /// <summary>
      /// Copies an existing file to a new file. Overwriting a file of the same name is allowed.
      /// </summary>
      /// <param name="sourceFileName">The file to copy.</param>
      /// <param name="destFileName">The name of the destination file. This cannot be a directory.</param>
      /// <param name="overwrite">TRUE if the destination file can be overwritten; otherwise, FALSE.</param>
      void Copy(string sourceFileName, string destFileName, bool overwrite);

      /// <summary>
      /// Creates or overwrites a file in the specified path.
      /// </summary>
      /// <param name="path">The path and name of the file to create.</param>
      /// <returns>A FileStream that provides read/write access to the file specified in path.</returns>
      FileStream Create(string path);

      /// <summary>
      /// Creates or overwrites a file in the specified path, specifying a buffer size.
      /// </summary>
      /// <param name="path">The path and name of the file to create.</param>
      /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
      /// <returns>A FileStream with the specified buffer size that provides read/write access to the file specified in path.</returns>
      FileStream Create(string path, int bufferSize);

      /// <summary>
      /// Creates or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.
      /// </summary>
      /// <param name="path">The path and name of the file to create.</param>
      /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
      /// <param name="options">One of the FileOptions values that describes how to create or overwrite the file.</param>
      /// <returns>A new file with the specified buffer size.</returns>
      FileStream Create(string path, int bufferSize, System.IO.FileOptions options);

      /// <summary>
      /// Deletes the specified file.
      /// </summary>
      /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
      void Delete(string path);

      /// <summary>
      /// Determines whether the specified file exists.
      /// </summary>
      /// <param name="path">The file to check.</param>
      /// <returns>TRUE if the caller has the required permissions and path contains the name of an existing file; otherwise, FALSE. 
      /// This method also returns FALSE if path is null, an invalid path, or a zero-length string. If the caller does not have sufficient 
      /// permissions to read the specified file, no exception is thrown and the method returns FALSE regardless of the existence of path.</returns>
      bool Exists(string path);

      /// <summary>
      /// Moves a specified file to a new location, providing the option to specify a new file name.
      /// </summary>
      /// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path.</param>
      /// <param name="destFileName">The new path and name for the file.</param>
      void Move(string sourceFileName, string destFileName);

      /// <summary>
      /// Moves a specified file to a new location, providing the options to specify a new file name and to overwrite the destination file if it already exists.
      /// </summary>
      /// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path.</param>
      /// <param name="destFileName">The new path and name for the file.</param>
      /// <param name="overwrite">TRUE to overwrite the destination file if it already exists; FALSE otherwise.</param>
      void Move(string sourceFileName, string destFileName, bool overwrite);

      /// <summary>
      /// Opens a FileStream on the specified path with read/write access with no sharing.
      /// </summary>
      /// <param name="path">The file to open.</param>
      /// <param name="mode">An object that describes optional FileStream parameters to use.</param>
      /// <returns>A FileStream opened in the specified mode and path, with read/write access and not shared.</returns>
      FileStream Open(string path, FileMode mode);

      /// <summary>
      /// Initializes a new instance of the FileStream class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, additional file options and the allocation size.
      /// </summary>
      /// <param name="path">The file to open.</param>
      /// <param name="options">An object that describes optional FileStream parameters to use.</param>
      /// <returns>A FileStream instance that wraps the opened file.</returns>
      FileStream Open(string path, FileStreamOptions options);

      /// <summary>
      /// Opens a FileStream on the specified path, with the specified mode and access with no sharing.
      /// </summary>
      /// <param name="path">The file to open.</param>
      /// <param name="mode">A FileMode value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
      /// <param name="access">A FileAccess value that specifies the operations that can be performed on the file.</param>
      /// <returns>An unshared FileStream that provides access to the specified file, with the specified mode and access.</returns>
      FileStream Open(string path, FileMode mode, FileAccess access);

      /// <summary>
      /// Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
      /// </summary>
      /// <param name="path">The file to open.</param>
      /// <param name="mode">A FileMode value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
      /// <param name="access">A FileAccess value that specifies the operations that can be performed on the file.</param>
      /// <param name="share">A FileShare value specifying the type of access other threads have to the file.</param>
      /// <returns>A FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
      FileStream Open(string path, FileMode mode, FileAccess access, FileShare share);

      /// <summary>
      /// Opens a text file, reads all the text in the file, and then closes the file.
      /// </summary>
      /// <param name="path">The file to open for reading.</param>
      /// <returns>A string containing all the text in the file.</returns>
      string ReadAllText(string path);

      /// <summary>
      /// Opens a file, reads all text in the file with the specified encoding, and then closes the file.
      /// </summary>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The encoding applied to the contents of the file.</param>
      /// <returns>A string containing all the text in the file.</returns>
      string ReadAllText(string path, Encoding encoding);

      /// <summary>
      /// Asynchronously opens a text file, reads all the text in the file, and then closes the file.
      /// </summary>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
      /// <returns>A string containing all the text in the file.</returns>
      Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default);

      /// <summary>
      /// Asynchronously opens a text file, reads all text in the file with the specified encoding, and then closes the file.
      /// </summary>
      /// <param name="path">The file to open for reading.</param>
      /// <param name="encoding">The encoding applied to the contents of the file.</param>
      /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
      /// <returns>A string containing all the text in the file.</returns>
      Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default);

      /// <summary>
      /// Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
      /// </summary>
      /// <param name="path">The file to write to.</param>
      /// <param name="contents">The string to write to the file.</param>
      void WriteAllText(string path, string? contents);

      /// <summary>
      /// Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
      /// </summary>
      /// <param name="path">The file to write to.</param>
      /// <param name="contents">The string to write to the file.</param>
      /// <param name="encoding">The encoding to apply to the string.</param>
      void WriteAllText(string path, string? contents, Encoding encoding);

      /// <summary>
      /// Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, 
      /// it is overwritten.
      /// </summary>
      /// <param name="path">The file to write to.</param>
      /// <param name="contents">The string to write to the file.</param>
      /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
      /// <returns>A task that represents the asynchronous write operation.</returns>
      Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default);

      /// <summary>
      /// Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, 
      /// it is overwritten.
      /// </summary>
      /// <param name="path">The file to write to.</param>
      /// <param name="contents">The string to write to the file.</param>
      /// <param name="encoding">The encoding to apply to the string.</param>
      /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is None.</param>
      /// <returns>A task that represents the asynchronous write operation.</returns>
      Task WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default);
   }
}