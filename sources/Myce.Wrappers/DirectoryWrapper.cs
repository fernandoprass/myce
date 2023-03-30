using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
    public class DirectoryWrapper : IDirectoryWrapper
   {
      public void Delete(string path)
      {
         Directory.Delete(path);
      }

      public void Delete(string path, bool recursive)
      {
         Directory.Delete(path, recursive);
      }

      public bool Exists(string? path)
      {
         return Directory.Exists(path);
      }

      public string[] GetFiles(string path)
      {
         return Directory.GetFiles(path);
      }

      public string[] GetFiles(string path, string searchPattern)
      {
         return Directory.GetFiles(path, searchPattern);
      }

      public string[] GetFiles(string path, string searchPattern, EnumerationOptions enumerationOptions)
      {
         return Directory.GetFiles(path, searchPattern, enumerationOptions);
      }

      public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
      {
         return Directory.GetFiles(path, searchPattern, searchOption);
      }
   }
}
