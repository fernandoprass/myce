using System.Text;
using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
    public class FileWrapper : IFileWrapper
    {
      /// <inheritdoc/>
      public void Copy(string sourceFileName, string destFileName)
      {
         File.Copy(sourceFileName, destFileName);
      }

      /// <inheritdoc/>
      public void Copy(string sourceFileName, string destFileName, bool overwrite)
      {
         File.Copy(sourceFileName, destFileName, overwrite);
      }

      /// <inheritdoc/>
      public FileStream Create(string path)
      {
         return File.Create(path);
      }

      /// <inheritdoc/>
      public FileStream Create(string path, int bufferSize)
      {
         return File.Create(path, bufferSize);
      }

      /// <inheritdoc/>
      public FileStream Create(string path, int bufferSize, FileOptions options)
      {
         return File.Create(path, bufferSize, options);
      }

      /// <inheritdoc/>
      public StreamWriter CreateText(string path)
      {
         return File.CreateText(path);
      }

      /// <inheritdoc/>
      public void Delete(string path)
      {
         File.Delete(path);
      }

      /// <inheritdoc/>
      public bool Exists(string path)
      {
         return File.Exists(path);
      }

      /// <inheritdoc/>
      public void Move(string sourceFileName, string destFileName)
      {
         File.Move(sourceFileName, destFileName);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public void Move(string sourceFileName, string destFileName, bool overwrite)
      {
         File.Move(sourceFileName, destFileName, overwrite);
      }
#endif

      /// <inheritdoc/>
      public FileStream Open(string path, FileMode mode)
      {
         return File.Open(path, mode);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public FileStream Open(string path, FileStreamOptions options)
      {
         return File.Open(path, options);
      }
#endif

      /// <inheritdoc/>
      public FileStream Open(string path, FileMode mode, FileAccess access)
      {
         return File.Open(path, mode, access);
      }

      /// <inheritdoc/>
      public FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
      {
         return File.Open(path, mode, access, share);
      }

      /// <inheritdoc/>
      public string ReadAllText(string path)
      {
         return File.ReadAllText(path);
      }

      /// <inheritdoc/>
      public string ReadAllText(string path, Encoding encoding)
      {
         return File.ReadAllText(path, encoding);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
      {
         return File.AppendAllLinesAsync(path, contents, cancellationToken);
      }

      /// <inheritdoc/>
      public Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.AppendAllLinesAsync(path, contents, encoding, cancellationToken);
      }

      /// <inheritdoc/>
      public Task AppendAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)
      {
         return File.AppendAllTextAsync(path, contents, cancellationToken);
      }

      /// <inheritdoc/>
      public Task AppendAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.AppendAllTextAsync(path, contents, encoding, cancellationToken);
      }

      /// <inheritdoc/>
      public Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default)
      {
         return File.ReadAllBytesAsync(path, cancellationToken);
      }

      /// <inheritdoc/>
      public Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default)
      {
         return File.ReadAllLinesAsync(path, cancellationToken);
      }

      /// <inheritdoc/>
      public Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.ReadAllLinesAsync(path, encoding, cancellationToken);
      }

      /// <inheritdoc/>
      public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
      {
         return File.ReadAllTextAsync(path, cancellationToken);
      }

      /// <inheritdoc/>
      public Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.ReadAllTextAsync(path, encoding, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
      {
         return File.WriteAllBytesAsync(path, bytes, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = default)
      {
         return File.WriteAllLinesAsync(path, contents, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.WriteAllLinesAsync(path, contents, encoding, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default)
      {
         return File.WriteAllTextAsync(path, contents, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.WriteAllTextAsync(path, contents, encoding, cancellationToken);
      }

#if NET8_0_OR_GREATER
      /// <inheritdoc/>
      public IAsyncEnumerable<string> ReadLinesAsync(string path, CancellationToken cancellationToken = default)
      {
         return File.ReadLinesAsync(path, cancellationToken);
      }

      /// <inheritdoc/>
      public IAsyncEnumerable<string> ReadLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.ReadLinesAsync(path, encoding, cancellationToken);
      }
#endif

#if NET9_0_OR_GREATER
      /// <inheritdoc/>
      public Task AppendAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = default)
      {
         return File.AppendAllBytesAsync(path, bytes, cancellationToken);
      }

      /// <inheritdoc/>
      public Task AppendAllBytesAsync(string path, ReadOnlyMemory<byte> bytes, CancellationToken cancellationToken = default)
      {
         return File.AppendAllBytesAsync(path, bytes, cancellationToken);
      }

      /// <inheritdoc/>
      public Task AppendAllTextAsync(string path, ReadOnlyMemory<char> contents, CancellationToken cancellationToken = default)
      {
         return File.AppendAllTextAsync(path, contents, cancellationToken);
      }

      /// <inheritdoc/>
      public Task AppendAllTextAsync(string path, ReadOnlyMemory<char> contents, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.AppendAllTextAsync(path, contents, encoding, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllBytesAsync(string path, ReadOnlyMemory<byte> bytes, CancellationToken cancellationToken = default)
      {
         return File.WriteAllBytesAsync(path, bytes, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllTextAsync(string path, ReadOnlyMemory<char> contents, CancellationToken cancellationToken = default)
      {
         return File.WriteAllTextAsync(path, contents, cancellationToken);
      }

      /// <inheritdoc/>
      public Task WriteAllTextAsync(string path, ReadOnlyMemory<char> contents, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.WriteAllTextAsync(path, contents, encoding, cancellationToken);
      }
#endif
#endif
   }
}