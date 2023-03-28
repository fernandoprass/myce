using System.Text;
using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
    public class FileWrapper : IFileWrapper
   {
      /// <inheritdoc/>
      public bool Exists(string path)
      {
         return File.Exists(path);
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
      public void WriteAllText(string path, string? contents)
      {
         File.WriteAllText(path, contents);
      }

      /// <inheritdoc/>
      public void WriteAllText(string path, string? contents, Encoding encoding)
      {
         File.WriteAllText(path, contents, encoding);
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
   }
}