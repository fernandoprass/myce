using Myce.Response;
using Myce.Response.Messages;

namespace Myce.Validation
{
   /// <summary> A fluent validator interface </summary>
   public interface IValidator
   {
      Result Validate();
   }

   public abstract class Validator<T> : IValidator where T : Validator<T>
   {
      private readonly IList<Message> Messages = new List<Message>();

      public T If(bool expression, Message mesage)
      {
         if (expression)
         {
            Messages.Add(mesage);
         }
         return (T)this;
      }

      public Result Validate()
      {
         return new Result(Messages);
      }
   }

   public class Validator : Validator<Validator>
   {

   }
}
