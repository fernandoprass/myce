using Myce.Response.Messages;
using Xunit;

namespace Myce.Validation.Tests
{
   ///// <summary> Test Validator </summary>
   //public class ValidatorTests
   //{
   //   /// <summary> Verify If validators </summary>
   //   [Theory]
   //   [InlineData(1 == 1, 1)]
   //   [InlineData(1 == 2, 0)]
   //   public void If(bool expression, int expectedNumberOfErrors)
   //   {
   //      var errorMessage = new ErrorMessage { Code = "001", Text = "message" };

   //      var validator = new EntityValidatorOld()
   //         .If(expression, errorMessage);

   //      var result = validator.Validate();

   //      Assert.Equal(expectedNumberOfErrors, result.Messages.Count()); 

   //      if (result.HasMessage)
   //      {
   //         Assert.IsType<ErrorMessage>(result.Messages.First());
   //      }
   //   }
   //}
}
