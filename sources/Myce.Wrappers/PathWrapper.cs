using Myce.Wrappers.Contracts;

namespace Myce.Wrappers
{
   public class PathWrapper : IPathWrapper
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

      /// <inheritdoc/>
      public string? GetDirectoryName(string? path)
      {
         return Path.GetDirectoryName(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path)
      {
         return Path.GetDirectoryName(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path)
      {
         return Path.GetExtension(path);
      }
#endif

      /// <inheritdoc/>
      public string? GetExtension(string? path)
      {
         return Path.GetExtension(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path)
      {
         return Path.GetFileName(path);
      }
#endif

      /// <inheritdoc/>
      public string? GetFileName(string? path)
      {
         return Path.GetFileName(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path)
      {
         return Path.GetFileNameWithoutExtension(path); 
      }
#endif

      /// <inheritdoc/>
      public string? GetFileNameWithoutExtension(string? path)
      {
         return Path.GetFileNameWithoutExtension(path);
      }

      /// <inheritdoc/>
      public string GetFullPath(string path)
      {
         return Path.GetFullPath(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public string GetFullPath(string path, string basePath)
      {
         return Path.GetFullPath(path, basePath);
      }
#endif

      /// <inheritdoc/>
      public string? GetPathRoot(string? path)
      {
         return Path.GetPathRoot(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
      {
         return Path.GetPathRoot(path);
      }
#endif

      /// <inheritdoc/>
      public string GetRandomFileName()
      {
         return Path.GetRandomFileName();
      }

      /// <inheritdoc/>
      public string GetTempFileName()
      {
         return Path.GetTempFileName();
      }

      /// <inheritdoc/>
      public string GetTempPath()
      {
         return Path.GetTempPath();
      }

      /// <inheritdoc/>
      public bool HasExtension(string? path)
      {
         return Path.HasExtension(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public bool HasExtension(ReadOnlySpan<char> path)
      {
         return Path.HasExtension(path);
      }
#endif

      /// <inheritdoc/>
      public bool IsPathRooted(string? path)
      {
         return Path.IsPathRooted(path);
      }

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public bool IsPathRooted(ReadOnlySpan<char> path)
      {
         return Path.IsPathRooted(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public bool IsPathFullyQualified(string path)
      {
         return Path.IsPathFullyQualified(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public bool IsPathFullyQualified(ReadOnlySpan<char> path)
      {
         return Path.IsPathFullyQualified(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public string Join(params string?[] paths)
      {
         return Path.Join(paths);
      }

      /// <inheritdoc/>
      public string Join(string? path1, string? path2)
      {
         return Path.Join(path1,path2);
      }

      /// <inheritdoc/>
      public string Join(string? path1, string? path2, string? path3)
      {
         return Path.Join(path1,path2,path3);
      }

      /// <inheritdoc/>
      public string Join(string? path1, string? path2, string? path3, string? path4)
      {
         return Path.Join(path1, path2, path3, path4);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
      {
         return Path.Join(path1, path2);
      }

      /// <inheritdoc/>
      public string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
      {
         return Path.Join(path1, path2, path3);
      }

      /// <inheritdoc/>
      public string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, ReadOnlySpan<char> path4)
      {
         return Path.Join(path1, path2, path3, path4);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public string GetRelativePath(string relativeTo, string path)
      {
         return Path.GetRelativePath(relativeTo, path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public string TrimEndingDirectorySeparator(string path)
      {
         return Path.TrimEndingDirectorySeparator(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path)
      {
         return Path.TrimEndingDirectorySeparator(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public bool EndsInDirectorySeparator(string path)
      {
         return Path.EndsInDirectorySeparator(path);
      }
#endif

#if !NETSTANDARD2_0
      /// <inheritdoc/>
      public bool EndsInDirectorySeparator(ReadOnlySpan<char> path)
      {
         return Path.EndsInDirectorySeparator(path);
      }
#endif

      /// <inheritdoc/>
      public char[] GetInvalidFileNameChars()
      {
         return Path.GetInvalidFileNameChars();
      }

      /// <inheritdoc/>
      public char[] GetInvalidPathChars()
      {
         return Path.GetInvalidPathChars();
      } 
   }
}
