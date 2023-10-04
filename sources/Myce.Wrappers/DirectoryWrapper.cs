using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
    public class DirectoryWrapper : IDirectoryWrapper
   {
      /// <inheritdoc/>
      public DirectoryInfo CreateDirectory(string path)
      {
         return Directory.CreateDirectory(path);
      }

      /// <inheritdoc/>
      public void Delete(string path)
      {
         Directory.Delete(path);
      }

      /// <inheritdoc/>
      public void Delete(string path, bool recursive)
      {
         Directory.Delete(path, recursive);
      }

      /// <inheritdoc/>
      public bool Exists(string? path)
      {
         return Directory.Exists(path);
      }

      /// <inheritdoc/>
      public string GetDirectoryRoot(string path)
      {
        return Directory.GetDirectoryRoot(path);
      }

      /// <inheritdoc/>
      public string[] GetFiles(string path)
      {
         return Directory.GetFiles(path);
      }

      /// <inheritdoc/>
      public string[] GetFiles(string path, string searchPattern)
      {
         return Directory.GetFiles(path, searchPattern);
      }

      /// <inheritdoc/>
      public string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
      {
         return Directory.GetFiles(path, searchPattern, enumerationOptions);
      }

      /// <inheritdoc/>
      public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
      {
         return Directory.GetFiles(path, searchPattern, searchOption);
      }

      /// <inheritdoc/>
      public DirectoryInfo? GetParent(string path)
      {
         return Directory.GetParent(path);
      }
   }
}
