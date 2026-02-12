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
      public Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default)
      {
         return File.ReadAllTextAsync(path, cancellationToken);
      }

      /// <inheritdoc/>
      public Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default)
      {
         return File.ReadAllTextAsync(path, encoding, cancellationToken);
      }
#endif

      /// <inheritdoc/>
      public void WriteAllText(string path, string? contents)
      {
         File.WriteAllText(path, contents);
      }

      /// <inheritdoc/>
      public void WriteAllText(string path, string? contents, Encoding encoding)
      {
         File.WriteAllText(path, contents, encoding);
      }

#if !NETSTANDARD2_0
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
#endif
   }
}