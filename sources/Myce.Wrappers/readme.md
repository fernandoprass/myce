# MYCE.Wrappers
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of wrappers for the most common System.IO classes, which is especially useful for mock methods in unit tests.

Supports `net6.0`, `net8.0`, `net9.0`, `net10.0`, and `netstandard2.0`.

## Library
This is a simple wrapper for some of the most used classes of System.IO Namespace. For more information, including examples, visit 
[Microsoft Learn - System.IO Namespace](https://learn.microsoft.com/en-us/dotnet/api/system.io).

### Wrappers index
- [DirectoryWrapper](DirectoryWrapper.md)
- [FileWrapper](FileWrapper.md)
- [PathWrapper](PathWrapper.md)

## Dependencies
- None

## Compatibility
This library supports `netstandard2.0`, but some modern APIs are excluded in that target to ensure compatibility:
- **Span-based methods**: Methods using `ReadOnlySpan<char>` (e.g., in `Path`) are not available in `netstandard2.0`.
- **Advanced Options**: Methods using `EnumerationOptions` or `FileStreamOptions` are excluded.
- **Modern Path/File methods**: `Join`, `GetRelativePath`, `ReadAllTextAsync`, `WriteAllTextAsync`, and `Move(overwrite)` are not available in `netstandard2.0`.

## Attention:
None of this library's methods have unit test for the simple fact that, as the name implies, they are wrappers of original methods from the System.IO namespace.

## Notes
Version 1.1.0
- Add async methods, such as `ReadAllTextAsync` and `WriteAllTextAsync`, for improved performance in asynchronous programming scenarios.
- Introduces support for `net10.0`, ensuring compatibility with the latest .NET features and improvements.

Version 1.0.0 
- The initial stable release of Myce.Wrappers, providing basic wrapper capabilities for .NET applications.

## Contributions
Contributions are welcome on this library. If you use a class or method from the System.IO namespace that is not already here, create a Pull Request and submit it.

Developed by Fernando Prass
