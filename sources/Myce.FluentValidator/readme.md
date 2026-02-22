# MYCE.FluentValidator
MYCE (Makes Your Coding Easier) FluentValidator is a fluent validation library designed to simplify entity validation in .NET applications.

Supports `net6.0`, `net8.0`, `net9.0`, and `netstandard2.0`.

## Installation
Package Manager Console:
```powershell
Install-Package Myce.FluentValidator
```

## Features
- **FluentValidator**: Fluent validation class for entities
- **ValidatorBuilder**: Orchestrates multiple validations in a single process

## Usage

### 1. Create your Entity
```csharp
public class Person
{
   public string Name { get; set; }
   public int Age { get; set; }
   public string Email { get; set; }
   public string Code { get; set; }
}
```

### 2. Define Validation Rules
Use the `FluentValidator` to define rules for your entity properties.

```csharp
var person = new Person { Name = "John Doe", Age = 30, Email = "john.doe@example.com", Code = "A123" };

var validator = new FluentValidator<Person>();
    .RuleFor(x => x.Name)
        .IsRequired()
        .MinLength(3)
        .MaxLength(50)
    .RuleFor(x => x.Age)
        .IsGreaterThanOrEqualTo(18)
        .IsLessThanOrEqualTo(100)
    .RuleFor(x => x.Email)
        .IsRequired()
        .IsValidEmailAddress()
    .RuleFor(x => x.Code)
        .ExactNumberOfCharacters(4)
        .ContainsOnlyNumber();

var result = validator.Validate(person);

if (validator.Messages.Any())
{
    foreach (var message in validator.Messages)
    {
        Console.WriteLine(message.Text);
    }
}
```

## Supported Validators

| Validator | Description |
| :--- | :--- |
| `Contains` | Validates that the value exists within a provided collection. |
| `ContainsOnlyNumber` | Validates that a string contains only numeric characters. |
| `ExactNumberOfCharacters` | Validates that a string has an exact length. |
| `ExactNumberOfCharactersIf` | Validates exact length if a condition is true. |
| `IsEqualTo` | Validates equality to a specific value. |
| `IsGreaterThan` | Validates that the value is greater than a specified value. |
| `IsGreaterThanOrEqualTo` | Validates that the value is greater than or equal to a specified value. |
| `IsLessThan` | Validates that the value is less than a specified value. |
| `IsLessThanOrEqualTo` | Validates that the value is less than or equal to a specified value. |
| `IsNotEqualTo` | Validates if the property value is not equal to a fixed value. |
| `IsRequired` | Validates that the property is not null or empty. |
| `IsRequiredIf` | Validates that the property is required if a condition is true. |
| `IsNotNull` |Validates if the property value is not null. |
| `IsNull` |Validates if the property value is null. |
| `IsValidDate` | Validates that a string can be parsed as a valid date. |
| `IsValidEmailAddress` | Validates that a string is a valid email format. |
| `MaxLength` | Validates the maximum length of a string. |
| `MaxLengthIf` | Validates maximum length if a condition is true. |
| `MinLength` | Validates the minimum length of a string. |
| `MinLengthIf` | Validates minimum length if a condition is true. |

## Notes
- Version 1.2.o adds RuleForValue, which allows you to validate a property based on the value of another property or variable.
- Version 1.1.1 adds new validators: `IsNotNull`, and `IsNull`.
- Version 1.1.0 introduces multi-targeting support (`net6.0`, `net7.0`, `net8.0`, `net9.0`, and `netstandard2.0`) and full nullability support.
- Version 1.0.0 was the initial release of Myce.FluentValidator, providing basic validation capabilities for .NET applications.

## Dependencies
- Myce.Extensions
- Myce.Response

## Contributions
Contributions are welcome! If you have a validation method you think is useful and can make life easier for other developers, please create a Pull Request and submit it.
**Attention**: All submitted methods must include a unit test.

Developed by Fernando Prass