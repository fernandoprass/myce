using System.Text;

namespace Myce.Wrappers.Contracts
{
   public interface IFileWrapper
   {
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
      /// Determines whether the specified file exists.
      /// </summary>
      /// <param name="path">The file to check.</param>
      /// <returns>TRUE if the caller has the required permissions and path contains the name of an existing file; otherwise, FALSE. 
      /// This method also returns FALSE if path is null, an invalid path, or a zero-length string. If the caller does not have sufficient 
      /// permissions to read the specified file, no exception is thrown and the method returns FALSE regardless of the existence of path.</returns>
      bool Exists(string path);

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