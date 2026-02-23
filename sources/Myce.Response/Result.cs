using Myce.Extensions;
using Myce.Response.Messages;
using System.Text.Json.Serialization;

namespace Myce.Response
{
   public interface IResult
   {
      IReadOnlyCollection<Message> Messages { get; }
      bool IsValid { get; }
   }

   public class Result : IResult
   {
      private string _title = string.Empty;

      private readonly List<Message> _messages = new List<Message>();

      /// <summary>
      /// Gets a read-only collection of messages associated with this instance.
      /// </summary>
      public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

      /// <summary>
      /// Gets a value indicating whether the current state is valid.
      /// </summary>
      public bool IsValid => !HasError;

      /// <summary>
      /// Gets a value indicating whether any error messages are present.
      /// </summary>
      [JsonIgnore]
      public bool HasError => _messages.Any(x => x.Type == MessageType.Error);

      /// <summary>
      /// Gets a value indicating whether any messages are present.
      /// </summary>
      [JsonIgnore]
      public bool HasMessage => _messages.Any();

      /// <summary>
      /// Gets a value indicating whether any warning messages are present.
      /// </summary>
      [JsonIgnore]
      public bool HasWarning => _messages.Any(x => x.Type == MessageType.Warning);

      /// <summary>
      /// Gets or sets the title associated with the current instance.
      /// </summary>
      /// <remarks>If the title has not been explicitly set, getting this property returns the text of the
      /// first message in the collection, if available; otherwise, it returns null.</remarks>
      public string? Title
      {
         get => string.IsNullOrWhiteSpace(_title) ? _messages.FirstOrDefault()?.Show() : _title;
         set => _title = value ?? string.Empty;
      }

      /// <summary>
      /// Initializes a new instance of the Result class.
      /// </summary>
      public Result() { }

      /// <summary>
      /// Initializes a new instance of the Result class with the specified title.
      /// </summary>
      /// <param name="title">The title to associate with the result. Cannot be null.</param>
      public Result(string title) => Title = title;

      /// <summary>
      /// Initializes a new instance of the Result class with the specified message.
      /// </summary>
      /// <param name="message">The message to add to the result. Cannot be null.</param>
      public Result(Message message) => AddMessage(message);

      /// <summary>
      /// Initializes a new instance of the Result class with the specified collection of messages.
      /// </summary>
      /// <param name="messages">The collection of Message objects to include in the result. Cannot be null.</param>
      public Result(IEnumerable<Message> messages) => AddMessages(messages);

      /// <summary>
      /// Creates a new instance of the Result type that represents a successful operation.
      /// </summary>
      /// <returns>A Result instance indicating success.</returns>
      public static Result Success() => new Result();

      /// <summary>
      /// Creates a successful result with the specified title.
      /// </summary>
      /// <param name="title">The title that describes the successful result. Cannot be null.</param>
      /// <returns>A <see cref="Result"/> instance representing a successful outcome with the given title.</returns>
      public static Result Success(string title) => new Result(title);

      /// <summary>
      /// Creates a successful result with the specified information message.
      /// </summary>
      /// <param name="message">The message that confirm the operation. Cannot be null.</param>
      /// <returns>A <see cref="Result"/> instance representing a failure with the provided error message.</returns>
      public static Result Success(InformationMessage message) => new Result(message);

      /// <summary>
      /// Creates a failed result with the specified error message.
      /// </summary>
      /// <param name="message">The error message that describes the reason for the failure. Cannot be null.</param>
      /// <returns>A <see cref="Result"/> instance representing a failure with the provided error message.</returns>
      public static Result Failure(ErrorMessage message) => new Result(message);

      /// <summary>
      /// Creates a failed result containing the specified error messages.
      /// </summary>
      /// <param name="messages">A collection of messages that describe the reasons for the failure. Cannot be null or contain null elements.</param>
      /// <returns>A <see cref="Result"/> instance representing a failure with the provided error messages.</returns>
      public static Result Failure(IEnumerable<ErrorMessage> messages) => new Result(messages);

      /// <summary>
      /// Adds a message to the collection.
      /// </summary>
      /// <param name="title">The message to add to the collection. Cannot be null.</param>
      public void AddMessage(Message title) => _messages.Add(title);

      /// <summary>
      /// Adds the specified collection of messages to the current list.
      /// </summary>
      /// <param name="messages">The collection of messages to add. Cannot be null.</param>
      public void AddMessages(IEnumerable<Message> messages) => _messages.AddRange(messages);
   }

   public class Result<T> : Result
   {
      /// <summary>
      /// Gets a value indicating whether the object is valid and its associated data is not null.
      /// </summary>
      [JsonIgnore]
      public bool IsValidAndDataIsNotNull => IsValid && Data != null;

      /// <summary>
      /// Gets a value indicating whether the object is valid and its data is null.
      /// </summary>
      [JsonIgnore]
      public bool IsValidAndDataIsNull => IsValid && Data == null;

      /// <summary>
      /// Gets a value indicating whether the current instance contains data.
      /// </summary>
      [JsonIgnore]
      public bool HasData => Data != null;

      /// <summary>
      /// Gets a value indicating whether an error has occurred or the data is null.
      /// </summary>
      [JsonIgnore] 
      public bool HasErrorOrDataIsNull => !IsValidAndDataIsNotNull;

      /// <summary>
      /// Gets or sets the data associated with the current instance.
      /// </summary>
      public T? Data { get; set; }

      /// <summary>
      /// Initializes a new instance of the Result class.
      /// </summary>
      public Result() { }

      /// <summary>
      /// Initializes a new instance of the Result class with the specified data.
      /// </summary>
      /// <param name="data">The data value to be associated with this result. Can be null for reference types.</param>
      public Result(T? data) => Data = data;

      /// <summary>
      /// Initializes a new instance of the Result class with the specified title message.
      /// </summary>
      /// <param name="title">The message to use as the title for the result. Cannot be null.</param>
      public Result(Message title) : base(title) { }

      /// <summary>
      /// Initializes a new instance of the Result class with the specified collection of messages.
      /// </summary>
      /// <param name="messages">The collection of Message objects to include in the result. Cannot be null.</param>
      public Result(IEnumerable<Message> messages) : base(messages) { }

      /// <summary>
      /// Initializes a new instance of the Result class with the specified data and title.
      /// </summary>
      /// <param name="data">The data value to associate with the result. This value is assigned to the Data property.</param>
      /// <param name="title">The title that describes the result. This value is passed to the base class.</param>
      public Result(T? data, string title) : base(title) => Data = data;

      /// <summary>
      /// Initializes a new instance of the Result class with the specified data and message.
      /// </summary>
      /// <param name="data">The data value associated with the result. This value represents the result's payload and may be null
      /// depending on the operation.</param>
      /// <param name="message">The message that describes the result. Cannot be null.</param>
      public Result(T? data, Message message) : base(message) => Data = data;

      /// <summary>
      /// Initializes a new instance of the Result class with the specified data and associated messages.
      /// </summary>
      /// <param name="data">The result data to be encapsulated by this instance.</param>
      /// <param name="messages">A collection of messages that provide additional information about the result. Cannot be null.</param>
      public Result(T? data, IEnumerable<Message> messages) : base(messages) => Data = data;

      /// <summary>
      /// Creates a successful result containing the specified data.
      /// </summary>
      /// <param name="data">The data to include in the successful result. Can be null if the result type allows null values.</param>
      /// <returns>A Result<T> instance representing a successful operation with the provided data.</returns>
      public static Result<T> Success(T? data) => new Result<T>(data);

      /// <summary>
      /// Creates a successful result containing the specified data and title.
      /// </summary>
      /// <param name="data">The data to include in the successful result.</param>
      /// <param name="title">The title that describes the result.</param>
      /// <returns>A Result<T> instance representing a successful outcome with the provided data and title.</returns>
      public static Result<T> Success(T? data, string title) => new Result<T>(data, title);

      /// <summary>
      /// Creates a failed result with the specified error message.
      /// </summary>
      /// <param name="message">The error message that describes the reason for the failure. Cannot be null.</param>
      /// <returns>A failed <see cref="Result{T}"/> instance containing the specified error message.</returns>
      public static new Result<T> Failure(Message message) => new Result<T>(message);

      /// <summary>
      /// Creates a failed result containing the specified error messages.
      /// </summary>
      /// <param name="messages">A collection of messages that describe the reasons for the failure. Cannot be null or empty.</param>
      /// <returns>A failed result that encapsulates the provided error messages.</returns>
      public static new Result<T> Failure(IEnumerable<Message> messages) => new Result<T>(messages);

      /// <summary>
      /// Creates a new generic result by copying the messages from an existing non-generic result.
      /// </summary>
      /// <param name="result">The non-generic result whose messages will be used to initialize the new generic result. Cannot be null.</param>
      /// <returns>A new instance of <see cref="Result{T}"/> containing the messages from the specified <paramref
      /// name="result"/>.</returns>
      public static Result<T> FromResult(Result result) => new Result<T>(result.Messages);

      /// <summary>
      /// Converts the current result to a new result of type V by applying the specified mapping function to the data value.
      /// </summary>
      /// <typeparam name="V">The type of the value returned by the mapping function.</typeparam>
      /// <param name="map">A function that maps the current data value to a value of type V. If the data is null, the default value of
      /// type V is used.</param>
      /// <returns>A new Result<V> containing the mapped value and the original messages.</returns>
      public Result<V> ToResult<V>(Func<T, V> map)
      {
         V? mappedData = Data != null ? map(Data) : default;

         return new Result<V>(mappedData, Messages);
      }

      /// <summary>
      /// Creates a new result of the specified type, carrying any error messages from the current result.
      /// </summary>
      /// <typeparam name="V">The type of the value to be associated with the new result.</typeparam>
      /// <returns>A new result of type <typeparamref name="V"/> that contains the same error messages as the current result.</returns>
      public Result<V> ToResultWithErrors<V>()
      {
         return new Result<V>(Messages);
      }
   }
}
