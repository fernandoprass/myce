# MYCE.FluentValidator
MYCE (Makes Your Coding Easier) FluentValidator is a fluent validation library designed to simplify entity validation in .NET applications.

Supports `net6.0`, `net7.0`, `net8.0`, `net9.0`, and `netstandard2.0`.

## Installation
Package Manager Console:
```powershell
Install-Package Myce.FluentValidator
```

## Features
- **FluentValidator**: Fluent validation class for entities
- **ValidatorBuilder**: Orchestrates multiple validations in a single process
- **High Performance**: Optimized with typed value access to avoid boxing/unboxing.
- **Reusable Templates**: Define rules once and apply them to multiple DTOs.
- **External Validation**: Validate standalone variables or external states with RuleForValue.

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

### 3. Reusable Templates (DRY Principle)
You can define validation logic for common fields (like Email) and reuse them across different classes.
```csharp
public static class SharedRules 
{
    public static void NameTemplate<T>(RuleBuilder<T, string> rb) where T : class
        => rb.IsRequired().MinLength(3).MaxLength(100);
}

// In your validator:
validator.RuleFor(x => x.FirstName).ApplyTemplate(SharedRules.NameTemplate);
validator.RuleFor(x => x.LastName).ApplyTemplate(SharedRules.NameTemplate);
```

### 4. External Value Validation
Use `RuleForValue` to validate data that isn't a property of your main entity, such as checking if a record already exists in the database.
```csharp
bool emailAlreadyExists = service.CheckEmail(request.Email);

var validator = new FluentValidator<Person>()
    .RuleForValue(emailAlreadyExists, "Email Uniqueness")
    .IsFalse(new ErrorMessage("This email is already taken"));
```

### 5. Conditional Validation ("If")
The `If` method applies a condition to the immediately preceding rule. Subsequent rules in the chain are unaffected and remain mandatory.
```csharp
validator.RuleFor(x => x.Name)
    .IsRequired().If(x => x.Age >= 18) // Name is only required for adults
    .IsAlpha();                        // Must always be alphabetic if provided
```


## Supported Validators

Core validators available:
| Validator | Description |
| :--- | :--- |
| `Custom` | Allows you to define a custom validation function. |
| `IsNull` |Validates if the property value is null. |
| `IsNotNull` |Validates if the property value is not null. |
| `IsRequired` | Validates that the property is not null or empty. |
| `IsTrue` | Validates that the boolean attribute is true.|
| `IsFalse` | Validates that the boolean attribute is false.|

Collection validators:
| Validator | Description |
| :--- | :--- |
| `IsEmpty` | Validates that the collection is empty (contains no items). |
| `IsIn` | Validates that the attribute value is present within a sequence of allowed valeus. |
| `HasItems` | Validates that the collection is not empty and has at least one item. |
| `MaxNumberOfItems` | Validates that the collection does not exceed a maximum number of items. |

Comparison validators:
| Validator | Description |
| :--- | :--- |
| `IsEqualTo` | Validates equality to a specific value. |
| `IsNotEqualTo` | Validates if the property value is not equal to a fixed value. |

Date validators:
| Validator | Description |
| :--- | :--- |
| `IsToday` | Validates that the property value represents today's date. |
| `IsYesterday` | Validates that the property value represents yesterday's date. |
| `IsTomorrow` | Validates that the property value represents tomorrow's date. |
| `IsInThePast` | Validates that the date and time is earlier than the current moment. |
| `IsInTheFuture` | Validates that the date and time is later than the current moment. |
| `IsWeekday` | Validates that the date falls on a business day (Monday to Friday). |
| `IsWeekend` | Validates that the date falls on a Saturday or Sunday. |

Enumerator validators:
| Validator | Description |
| :--- | :--- |
| `IsInEnum` | Validates that the value is a defined member of the enumeration. |
| `IsNotInEnum` | Validates that the value is not a defined member of the enumeration. |
| `IsNotDefault` | Validates that the value is not the default value of the enumeration. |

Numeric validators:
| Validator | Description |
| :--- | :--- |
| `IsBetween` | Validates that the value is between two specified values. |
| `IsGreaterThan` | Validates that the value is greater than a specified value. |
| `IsGreaterThanOrEqualTo` | Validates that the value is greater than or equal to a specified value. |
| `IsLessThan` | Validates that the value is less than a specified value. |
| `IsLessThanOrEqualTo` | Validates that the value is less than or equal to a specified value. |
| `IsPositive` | Validates that the integer value is positive (greater than zero). |
| `IsNegative` | Validates that the integer value is negative (less than zero). |


String validators:
| Validator | Description |
| :--- | :--- |
| `Contains` | Validates that the string contains a specific substring. |
| `ContainsOnlyNumber` | Validates that a string contains only numeric characters. |
| `ExactNumberOfCharacters` | Validates that a string has an exact length. |
| `IsAlpha` | Validates that the string contains only alphabetic characters. |
| `IsAlphaNumeric` | Validates that the string contains only letters and numbers. |
| `IsValidDate` | Validates that a string can be parsed as a valid date. |
| `IsValidEmailAddress` | Validates that a string is a valid email format. |
| `Matches` | Validates that the string matches a specific regular expression pattern. |
| `MaxLength` | Validates the maximum length of a string. |
| `MinLength` | Validates the minimum length of a string. |

## Notes
Version 1.4.0
- Adds the `If` method that makes a rule dependent on a condition.

Version 1.3.0 
- Add extension methods to validate enumarators and date attributes.

Version 1.2.4 
- Add Custom validator to allow users to define their own validation logic with a custom function.

Version 1.2.3 
- Add extension methods to validate collection and enumerable attributes.
- Added `IsPositive` and `IsNegative` validation rule for all numeric types (`int`, `double`, `decimal`) including nullable support.

Version 1.2.2 
- Added `IsBetween` validation rule for all numeric types (`int`, `double`, `decimal`) including nullable support-
- Fixed fluent chaining for RuleForValue, allowing multiple external value validations in a single statement.

Version 1.2.1 
- Internal engine updated to use `GetAttributeValue<T>` to eliminate boxing of primitive types and improve performance.

Version 1.2.0 
- Added `RuleForValue` for external variable validation and `ApplyTemplate` for rule reuse.

Version 1.1.1 
- Addedd new validators: `IsNotNull`, and `IsNull`.

Version 1.1.0
- Introduces multi-targeting support (`net6.0`, `net7.0`, `net8.0`, `net9.0`, and `netstandard2.0`) and full nullability support.

Version 1.0.0 
- The initial stable release of Myce.FluentValidator, providing basic validation capabilities for .NET applications.

## Dependencies
- Myce.Extensions
- Myce.Response

## Contributions
Contributions are welcome! If you have a validation method you think is useful and can make life easier for other developers, please create a Pull Request and submit it.
**Attention**: All submitted methods must include a unit test.

Developed by Fernando Prass