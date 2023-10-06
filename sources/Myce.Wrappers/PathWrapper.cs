using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
   internal class PathWrapper : IPathWrapper
   {
      /// <inheritdoc/>
      public string? ChangeExtension(string? path, string? extension)
      {
         return Path.ChangeExtension(path, extension);
      }

      /// <inheritdoc/>
      public string Combine(params string[] paths)
      {
         return Path.Combine(paths);
      }

      /// <inheritdoc/>
      public string Combine(string path1, string path2)
      {
         return Path.Combine(path1, path2);  
      }

      /// <inheritdoc/>
      public string Combine(string path1, string path2, string path3)
      {
         return Path.Combine(path1, path2, path3);
      }

      /// <inheritdoc/>
      public string Combine(string path1, string path2, string path3, string path4)
      {
         return Path.Combine(path1, path2, path3, path4);
      }
   }
}
