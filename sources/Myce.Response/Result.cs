using Myce.Extensions;
using Myce.Response.Messages;

namespace Myce.Response
{
   public class Result
   {
      private readonly List<Message> _messages = new List<Message>();

      public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

      public bool IsValid => !HasError;

      public bool HasError => _messages.Any(x => x.Type == MessageType.Error);

      public bool HasMessage => _messages.Any();

      public bool HasWarning => _messages.Any(x => x.Type == MessageType.Warning);

      public string Message { get; set; }

      public Result() { }

      public Result(string message) => Message = message;

      public Result(Message message) => AddMessage(message);

      public Result(IEnumerable<Message> messages) => AddMessages(messages);

      public void AddMessage(Message message) => _messages.Add(message);

      public void AddMessages(IEnumerable<Message> messages) => _messages.AddRange(messages);
   }

   public class Result<T> : Result
   {
      public bool IsValidAndDataIsNotNull => IsValid && Data.IsNotNull();

      public bool IsValidAndDataIsNull => IsValid && Data.IsNull();

      public bool HasData => Data != null;

      public bool HasErrorOrDataIsNull => !IsValidAndDataIsNotNull;

      public T Data { get; set; }

      public Result() { }

      public Result(T data) => Data = data;

      public Result(Message message) : base(message) { }

      public Result(IEnumerable<Message> messages) : base(messages) { }

      public Result(T data, string message) : base(message) => Data = data;

      public Result(T data, Message message) : base(message) => Data = data;

      public Result(T data, IEnumerable<Message> messages) : base(messages) => Data = data;

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
