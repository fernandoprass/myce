# Myce.Response

A lightweight .NET library implementing the Result Pattern to standardize API responses, business-flow outcomes, messages, and backend-driven internationalization.

Supports `net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`, and `netstandard2.0`.

## Features

- **Unified Result Envelope**: Standardized `Result` and `Result<T>` API contracts.
- **Rich Messaging**: Information, warning, and error messages.
- **Variable Interpolation**: Supports `{name}` and `[name]` placeholders.
- **Multilingual Messages**: Stores templates and variable values by culture.
- **Lean Payloads**: Internal state properties use `[JsonIgnore]`.
- **Smart Titles**: An unset title uses the formatted first error, or the first available message.
- **Result Conversion and Merge**: Preserves messages when converting or combining results.

## Installation

```bash
dotnet add package Myce.Response
```

## Usage

### Basic Result

Use `Result` when an operation does not return data:

```csharp
public Result UpdateSystemSetting(string key, string value)
{
    if (string.IsNullOrEmpty(key))
        return Result.Failure(
            new ErrorMessage("KEY_REQUIRED", "Setting key is mandatory"));

    return Result.Success("Setting updated successfully");
}
```

### Returning Data

Use `Result<T>` to wrap an operation's data:

```csharp
public Result<User> GetUser(int id)
{
    var user = _repository.Find(id);

    if (user is null)
        return Result<User>.Failure(
            new ErrorMessage("USER_NOT_FOUND", "The requested user does not exist"));

    return Result<User>.Success(user);
}
```

### Messages with Variables

```csharp
var message = new ErrorMessage(
    "INSUFFICIENT_FUNDS",
    "You need at least {required}, but you have {current}.");

message.AddVariable("required", "50.00");
message.AddVariable("current", "10.50");

return Result.Failure(message);
```

`AddVariable(name, value)` adds a value for the message's current language, which is `en-US` for ordinary messages.

## Multilingual Messages

### Bulk Initialization

The first entry in the translation dictionary becomes the message's initial language.

```csharp
var templates = new Dictionary<string, string>
{
    { "en-US", "The field {fieldName} must be today." },
    { "pt-BR", "O campo {fieldName} deve ser a data de hoje." }
};

var message = new ErrorMessage("DATETIME_IS_TODAY", templates);

message.AddVariable("en-US", "fieldName", "Birth Date");
message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");

string english = message.Show("en-US");
string portuguese = message.Show("pt-BR");
```

Calling `Show(language)` renders that language without changing the message's current language.

### Incremental Configuration

```csharp
var message = new ErrorMessage();
message.Code = "INVALID_FIELD";

message.AddTextTranslation("en-US", "The {fieldName} is invalid.");
message.AddTextTranslation("pt-BR", "O {fieldName} é inválido.");

message.AddVariable("en-US", "fieldName", "Email Address");
message.AddVariable("pt-BR", "fieldName", "Endereço de E-mail");

string result = message.Show("pt-BR");
// "O Endereço de E-mail é inválido."
```

Adding the same language and variable-name pair again updates its value instead of creating a duplicate.

### Changing the Current Language

`TranslateTo(language)` updates both `Language` and `Text`. Parameterless `Show()` then renders the message using that language and its corresponding variables.

```csharp
message.TranslateTo("pt-BR");

Console.WriteLine(message.Language); // "pt-BR"
Console.WriteLine(message.Text);     // "O {fieldName} é inválido."
Console.WriteLine(message.Show());   // "O Endereço de E-mail é inválido."
```

If the requested template is unavailable, rendering falls back to the message's current language and then to the first available template. Standard messages and variables default to `en-US`.

### Translating a Failed Result

Use the language overload to translate every message before returning a failure:

```csharp
var messages = new List<Message>
{
    requiredFieldMessage,
    invalidDateMessage
};

return Result.Failure(messages, "pt-BR");
```

`Result.Failure(...)` requires at least one `ErrorMessage`; otherwise, it throws an exception.

## Architecture

### Result

- **Title**: Explicit summary, or the formatted first error message; if there is no error, the formatted first message.
- **IsSuccess**: `true` when no error message exists.
- **Messages**: Read-only collection of all messages.
- **HasError**: Indicates whether an error exists.
- **HasWarning**: Indicates whether a warning exists.
- **HasMessage**: Indicates whether any message exists.

### Result&lt;T&gt;

In addition to the base properties:

- **Data**: Generic result payload.
- **HasData**: Indicates whether `Data` is non-null.
- **IsValidAndDataIsNotNull**: Indicates success with non-null data.
- **IsValidAndDataIsNull**: Indicates success with null data.
- **HasErrorOrDataIsNull**: Indicates failure or null data.

### Message

- **Language**: Current culture code.
- **Code**: Stable identifier for the message.
- **Text**: Current untranslated template.
- **Type**: Information, warning, or error.
- **Variables**: Read-only collection of language-specific placeholder values.

### Message Types

1. **InformationMessage**: Non-critical status information.
2. **WarningMessage**: An alert that does not make the result unsuccessful.
3. **ErrorMessage**: A critical failure that makes `IsSuccess` return `false`.

## JSON and Frontend Integration

Messages expose the selected culture alongside their code, text, and culture-specific variables:

| Property | Purpose | Example |
|---|---|---|
| `Language` | Current message culture | `"pt-BR"` |
| `Code` | Stable message identifier | `"VALIDATION_ERROR"` |
| `Text` | Template for the current culture | `"O campo {fieldName} é inválido."` |
| `Variables` | Values used for interpolation | `[{"language":"pt-BR","name":"fieldName","value":"E-mail"}]` |

The frontend may display `Text` and interpolate `Variables`, or the backend may return formatted text through `Show()`.

## Best Practices

1. Use culture names such as `en-US`, `pt-BR`, and `es-ES`.
2. Keep placeholder names consistent across all translations.
3. Set `Title` explicitly when the UI summary should differ from the first error.
4. Use `ToResult<V>(map)` to map data while preserving messages.
5. Use `ToResultWithErrors<V>()` when only the messages should be carried to another result type.

## Notes

Version 1.5.2

- Fixed multilingual template and variable resolution.
- Added culture-specific variables through `AddVariable(language, name, value)`.
- Added `TranslateTo(language)` to update the current language and text.
- Added `Result.Failure(messages, language)` to translate returned messages.
- Updated title fallback to prefer the first formatted error message.

Version 1.5.0

- Added internationalization support for `Message`.

Version 1.3.0

- Removed obsolete `IsValid`; use `IsSuccess`.

Version 1.2.0

- Added `net10.0` support.

Version 1.0.0

- Initial stable release.
