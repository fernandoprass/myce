using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
   internal class PathWrapper : IPathWrapper
   {
      public string? ChangeExtension(string? path, string? extension)
      {
         return Path.ChangeExtension(path, extension);
      }
   }
}
