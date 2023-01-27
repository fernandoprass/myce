# MYCE.Extensions
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of extensions for the most common types.

## Library

- DateTimeExtension
    - FirstDayOfMonth() - Return the first day of the mounth for a given date
    - DaysInMonth() - Return the number of days in the mounth for a given date
    - LastDayOfMonth() - Return the last day of the mounth for a given date
- DecimalExtension
    - EqualZero() - Return true if the value is equal zero
    - GreaterThanZero() - Return true if the value is greater than zero
    - GreaterThanOrEqualZero() - Return true if the value is greater than or equal zero
    - LessThanZero() - Return true if the value is less than zero
    - LessThanOrEqualZero() - Return true if the value is less than or equal zero
- DoubleExtension
    - EqualZero() - Return true if the value is equal zero
    - GreaterThanZero() - Return true if the value is greater than zero
    - GreaterThanOrEqualZero() - Return true if the value is greater than or equal zero
    - LessThanZero() - Return true if the value is less than zero
    - LessThanOrEqualZero() - Return true if the value is less than or equal zero
- IntegerExtension
    - EqualZero() - Return true if the value is equal zero
    - GreaterThanZero() - Return true if the value is greater than zero
    - GreaterThanOrEqualZero() - Return true if the value is greater than or equal zero
    - LessThanZero() - Return true if the value is less than zero
    - LessThanOrEqualZero() - Return true if the value is less than or equal zero
- EnumerableExtensions
    - HasData() - Return true the if the enumerable object is not null and has any record
    - ContainsDuplicates() - Return true the if the enumerable constains a duplicate element
- EnumExtensions
    - GetDescription() - Return the enumerator description as a string
- GuidExtension
    - IsEmpty() - Return true if the guid is empty
    - IsNotEmpty() - Return true if the guid is not empty
- ObjectExtension
    - IsNull() - Return true if the guid is null
    - IsNotNull() - Return true if the guid is not null
- StringExtension
    - KeepOnlyNumbers() - Remove letters and simbols, keeping only numbers
    - KeepOnlyNumbersAndLetters() - Remove simbols keeping only numbers and letters  
    - KeepOnlyNumbersAndLettersAndSpaces() - Remove simbols keeping only numbers, letters and spaces 
    - RemoveSimbols() Remove simbols keeping only numbers, letters and spaces (if asked)
    - RemoveAccents() - Remove accents

## Installation

Package Manager Console:

```sh
Install-Package Myce.Extensions -Version 0.1.0
```

Package Reference (editing the Project File):
```
<PackageReference Include="Myce.Extensions" Version="0.1.0" />
```

.NET.CLI:
```
dotnet add package Myce.Extensions --version 0.1.0
```

Change 0.1.0 for the current version

Develeped by Fernando Prass