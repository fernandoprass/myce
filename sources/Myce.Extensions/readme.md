# MYCE.Extensions
MYCE (Makes Your Coding Easier) is a Nuget package for Visual Studio that contains a set of extensions for the most common types.

## Library
- CollectionExtensions
    - AddIfNotNull() - Add an item to a collection if the item is not null
    - AddRangeIfHasData() - Add a collection of items to a collection if the collections of items has data

- DateTimeExtension
    - FirstDayOfMonth() - Return the first day of the mounth for a given date
    - DaysInMonth() - Return the number of days in the mounth for a given date
    - LastDayOfMonth() - Return the last day of the mounth for a given date
    - IsWorkDay() - Return true if the date is a workday (Monday to Friday)
    - IsWeekend() - Return true if the date is a weekend (Saturday or Sunday). Holidays are not taken into account.
    - PriorWorkday() - Return the prior workday (Monday to Friday) for a given date. Holidays are not taken into account.
    - NextWorkday() - Return the next workday (Monday to Friday) for a given date. Holidays are not taken into account.
    - Yesterday() - Return yesterday's date
    - Tomorrow() - Return oomorrow's date

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

- EnumerableExtensions
    - Chunk() - Splits an enumerable into chunks of a specified size
    - HasData() - Return true the if the enumerable object is not null and has any record
    - ContainsDuplicates() - Return true the if the enumerable constains a duplicate element
    - DistinctBy() - Return a collection of elements distinct by specific property

- EnumExtensions
    - GetDescription() - Return the enumerator description as a string

- GuidExtension
    - IsEmpty() - Return true if the guid is empty
    - IsNotEmpty() - Return true if the guid is not empty

- IntegerExtension
    - EqualZero() - Return true if the value is equal zero
    - GreaterThanZero() - Return true if the value is greater than zero
    - GreaterThanOrEqualZero() - Return true if the value is greater than or equal zero
    - LessThanZero() - Return true if the value is less than zero
    - LessThanOrEqualZero() - Return true if the value is less than or equal zero

- ObjectExtension
    - IsNull() - Return true if the guid is null
    - IsNotNull() - Return true if the guid is not null

- StringExtension
    - EmptyIfIsNull() - Return an empty string if the string is null
    - KeepOnlyNumbers() - Remove letters and simbols, keeping only numbers
    - KeepOnlyNumbersAndLetters() - Remove simbols keeping only numbers and letters  
    - KeepOnlyNumbersAndLettersAndSpaces() - Remove simbols keeping only numbers, letters and spaces 
    - RemoveSimbols() Remove simbols keeping only numbers, letters and spaces (if asked)
    - RemoveAccents() - Remove accents

## Dependencies
- None

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