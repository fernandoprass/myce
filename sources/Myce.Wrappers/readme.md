# MYCE.Wrappers
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of wrappers for the most common IO classes, it is usefull to mock methods in unit tests.

## Library
- File
    - Exists(string path) - Determines whether the specified file exists.
    - ReadAllText(string path) - Opens a text file, reads all the text in the file, and then closes the file.
    - ReadAllTextAsync(string path, CancellationToken cancellationToken = default) - Asynchronously opens a text file, reads all the text in the file, and then closes the file.
    - ReadAllText(string path, Encoding encoding) - Opens a file, reads all text in the file with the specified encoding, and then closes the file.
    - ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = default) - Asynchronously opens a text file, reads all text in the file with the specified encoding, and then closes the file.
    - WriteAllText(string path, string? contents) - Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllText(string path, string? contents, Encoding encoding) - Creates a new file, write the contents to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllTextAsync(string path, string? contents, CancellationToken cancellationToken = default) - Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
    - WriteAllTextAsync(string path, string? contents, Encoding encoding, CancellationToken cancellationToken = default) - Asynchronously creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.

## Dependencies
- None

## Installation

Package Manager Console:

```sh
Install-Package Myce.Wrappers -Version 0.1.0
```

Package Reference (editing the Project File):
```
<PackageReference Include="Myce.Wrappers" Version="0.1.0" />
```

.NET.CLI:
```
dotnet add package Myce.Wrappers --version 0.1.0
```

Change 0.1.0 for the current version

Develeped by Fernando Prass