
# Myce.Response

A lightweight and robust .NET library implementing the **Result Pattern** to standardize API responses, handle business logic flow, and manage complex messaging with frontend-driven internationalization support.

## Features

* **Unified Result Envelope**: Standardized `Result` and `Result<T>` classes for consistent API contracts.
* **Rich Messaging System**: Support for multiple message types (Information, Warning, Error).
* **Dynamic Variable Interpolation**: Messages support placeholders (using `{}` or `[]`) for runtime data injection.
* **Frontend-Ready i18n**: Messages carry unique codes and variable dictionaries, allowing the frontend to handle translations.
* **Lean Payloads**: Internal logic properties are marked with `[JsonIgnore]` to keep JSON responses small and efficient.

* **Smart Fallback**: The `Title` property automatically defaults to the text of the first message if not explicitly set.  

---

## Installation

`dotnet add package Myce.Response`

## Usage
 
1. Basic Result

Use the `Result` class for operations that report status without returning a data payload.

    public Result UpdateSystemSetting(string key, string value)
    {
      if (string.IsNullOrEmpty(key))
       return Result.Failure(new ErrorMessage("KEY_REQUIRED", "Setting key is mandatory"));
     
      // Business logic execution...
      return Result.Success("Setting updated successfully");
    }

2. Returning Data with Result<T>

Use `Result<T>` to wrap the return value of your services.

    public Result<User> GetUser(int id)
    {
        var user = _repository.Find(id);
        
        if (user == null)
            return Result<User>.Failure(new ErrorMessage("USER_NOT_FOUND", "The requested user does not exist"));
    
        return Result<User>.Success(user);
    }
    
3. Messaging with Variables (i18n Support)

Placeholders in messages allow the frontend to perform translation using a dictionary while maintaining dynamic context.

    var message = new ErrorMessage("INSUFFICIENT_FUNDS", "You need at least {Required} to complete this, but you have {Current}");
    message.AddVariable("Required", "50.00");
    message.AddVariable("Current", "10.50");
    
    return Result.Failure(message);

## Architecture

### The Result Object

-   **Title**: (string) A high-level summary. If null, it returns `Messages.FirstOrDefault()?.Text`.   
-   **IsValid**: (bool) Returns `true` only if no `ErrorMessage` is present.   
-   **Messages**: (IReadOnlyCollection) A list of `Information`, `Warning`, or `Error` objects.   
-   **Data**: (T) The generic payload (specific to `Result<T>`).

### Message Types

1.  **InformationMessage**: Used for non-critical status updates.   
2.  **WarningMessage**: Used for alerts that do not block the operation.   
3.  **ErrorMessage**: Critical failures. Presence of this type makes `IsValid` return `false`.

## Frontend Integration (Internationalization)

This library follows a **Client-Side Translation** strategy. The backend provides the structural data, and the frontend applies the locale based on the `Code`.
| Property | Purpose |Example|
|---|---|---|
|`Code`|Unique translation key|"VALIDATION_ERROR"|
| `Text` | Default fallback (English) |"Invalid input"|
|`Variables`|Key-value pairs for interpolation|`[{"Name": "Field", "Value": "Email"}]`|

## Best Practices

1.  **Explicit Titles**: Set the `Title` property when you want a specific summary for the UI that differs from individual error messages.  
2.  **Controller Mapping**: Use the `ToActionResult()` extension to automatically map your `Result` to the appropriate HTTP status code (200 OK, 204 No Content, 400 Bad Request, or 404 Not Found).    
3.  **ToResult Mapping**: Use `.ToResult<V>(map)` to convert between types (e.g., Entity to DTO) while preserving all messages and state.

    [HttpGet("{id}")]
    public IActionResult Get(int id) => _userService.GetById(id).ToActionResult();
