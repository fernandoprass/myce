using Myce.Response;
using Myce.Response.Messages;

namespace Myce.Validation
{
   public class ValidatorBuilder
   {
      private readonly List<IValidator> Validators = new List<IValidator>();
      private readonly List<Message> Messages = new List<Message>();

      public void Add(IValidator validator)
      {
         Validators.Add(validator);
      }

      public Result Validate()
      {
         foreach (var validator in Validators)
         {
            var result = validator.Validate();
            if (result.HasMessage)
            {
               Messages.AddRange(result.Messages);
            }
         }
         return new Result(Messages);
      }
   }
}
