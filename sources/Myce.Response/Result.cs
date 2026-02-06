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
      private readonly List<Message> _messages = new List<Message>();

      public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

      public bool IsValid => !HasError;

      [JsonIgnore]
      public bool HasError => _messages.Any(x => x.Type == MessageType.Error);

      [JsonIgnore]
      public bool HasMessage => _messages.Any();

      [JsonIgnore]
      public bool HasWarning => _messages.Any(x => x.Type == MessageType.Warning);

      public string Message { get; set; }

      public Result() { }

      public Result(string message) => Message = message;

      public Result(Message message) => AddMessage(message);

      public Result(IEnumerable<Message> messages) => AddMessages(messages);

      public static Result Success() => new Result();

      public static Result Success(string message) => new Result(message);

      public static Result Failure(Message message) => new Result(message);

      public static Result Failure(IEnumerable<Message> messages) => new Result(messages);

      public void AddMessage(Message message) => _messages.Add(message);

      public void AddMessages(IEnumerable<Message> messages) => _messages.AddRange(messages);
   }

   public class Result<T> : Result
   {
      [JsonIgnore]
      public bool IsValidAndDataIsNotNull => IsValid && Data.IsNotNull();

      [JsonIgnore]
      public bool IsValidAndDataIsNull => IsValid && Data.IsNull();

      [JsonIgnore]
      public bool HasData => Data != null;

      [JsonIgnore] 
      public bool HasErrorOrDataIsNull => !IsValidAndDataIsNotNull;

      public T Data { get; set; }

      public Result() { }

      public Result(T data) => Data = data;

      public Result(Message message) : base(message) { }

      public Result(IEnumerable<Message> messages) : base(messages) { }

      public Result(T data, string message) : base(message) => Data = data;

      public Result(T data, Message message) : base(message) => Data = data;

      public Result(T data, IEnumerable<Message> messages) : base(messages) => Data = data;

      public static Result<T> Success(T data) => new Result<T>(data);

      public static Result<T> Success(T data, string message) => new Result<T>(data, message);

      public static Result<T> Failure(Message message) => new Result<T>(message);

      public static Result<T> Failure(IEnumerable<Message> messages) => new Result<T>(messages);

      public static Result<T> FromResult(Result result) => new Result<T>(result.Messages);

      public Result<V> ToResult<V>(Func<T, V> map)
      {
         var mappedData = Data != null ? map(Data) : default;

         return new Result<V>(mappedData, Messages);
      }

      public Result<V> ToResultWithErrors<V>()
      {
         return new Result<V>(Messages);
      }
   }
}
