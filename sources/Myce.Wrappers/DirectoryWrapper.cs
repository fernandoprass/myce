using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
    public class DirectoryWrapper : IDirectoryWrapper
   {
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
