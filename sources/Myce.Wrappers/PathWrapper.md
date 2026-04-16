# PathWrapper

`PathWrapper` is a wrapper around the `System.IO.Path` class, providing a mockable interface for path manipulation operations.

## Methods

- **ChangeExtension(string? path, string? extension)**: Changes the extension of a path string.
- **Combine(params string[] paths)**: Combines an array of strings into a path.
- **Combine(string path1, string path2)**: Combines two strings into a path.
- **Combine(string path1, string path2, string path3)**: Combines three strings into a path.
- **Combine(string path1, string path2, string path3, string path4)**: Combines four strings into a path.
- **EndsInDirectorySeparator(ReadOnlySpan<char> path)**: Returns a value that indicates whether the path, represented by a read-only character span, ends in a directory separator.
- **EndsInDirectorySeparator(string path)**: Returns a value that indicates whether the path, specified as a string, ends in a directory separator.
- **GetDirectoryName(ReadOnlySpan<char> path)**: Returns the directory information for the specified path represented by a character span.
- **GetDirectoryName(string? path)**: Returns the directory information for the specified path.
- **GetExtension(ReadOnlySpan<char> path)**: Returns the file name and extension of a file path that is represented by a read-only character span.
- **GetExtension(string? path)**: Returns the extension (including the period ".") of the specified path string.
- **GetFileName(ReadOnlySpan<char> path)**: Returns the file name and extension of a file path that is represented by a read-only character span.
- **GetFileName(string? path)**: Returns the file name and extension of the specified path string.
- **GetFileNameWithoutExtension(ReadOnlySpan<char> path)**: Returns the file name without the extension of a file path that is represented by a read-only character span.
- **GetFileNameWithoutExtension(string path)**: Returns the file name of the specified path string without the extension.
- **GetFullPath(string path)**: Returns the absolute path for the specified path string.
- **GetFullPath(string path, string basePath)**: Returns an absolute path from a relative path and a fully qualified base path.
- **GetInvalidFileNameChars()**: Gets the array of characters that are not allowed in file names.
- **GetInvalidPathChars()**: Gets the array of characters that are not allowed in path names.
- **GetPathRoot(ReadOnlySpan<char> path)**: Gets the root directory information from the path contained in the specified character span.
- **GetPathRoot(string? path)**: Gets the root directory information from the path contained in the specified string.
- **GetRandomFileName()**: Returns a random folder name or file name.
- **GetRelativePath(string relativeTo, string path)**: Returns a relative path from one path to another.
- **GetTempFileName()**: Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
- **GetTempPath()**: Returns the path of the current user's temporary folder.
- **HasExtension(ReadOnlySpan<char> path)**: Determines whether the path includes a file name extension.
- **HasExtension(string? path)**: Determines whether a path includes a file name extension.
- **IsPathFullyQualified(ReadOnlySpan<char> path)**: Returns a value that indicates whether the specified path, represented by a read-only character span, is fully qualified.
- **IsPathFullyQualified(string path)**: Returns a value that indicates whether the specified path is fully qualified.
- **IsPathRooted(ReadOnlySpan<char> path)**: Returns a value that indicates whether the specified path, represented by a read-only character span, contains a root.
- **IsPathRooted(string? path)**: Returns a value that indicates whether the specified path contains a root.
- **Join(params string?[] paths)**: Concatenates an array of paths into a single path.
- **Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)**: Concatenates two path components into a single path.
- **Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)**: Concatenates three path components into a single path.
- **Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, ReadOnlySpan<char> path4)**: Concatenates four path components into a single path.
- **Join(string? path1, string? path2)**: Concatenates two paths into a single path.
- **Join(string? path1, string? path2, string? path3)**: Concatenates three paths into a single path.
- **Join(string? path1, string? path2, string? path3, string? path4)**: Concatenates four paths into a single path.
- **TrimEndingDirectorySeparator(ReadOnlySpan<char> path)**: Trims one trailing directory separator beyond the root of the specified path.
- **TrimEndingDirectorySeparator(string path)**: Trims one trailing directory separator beyond the root of the specified path.
