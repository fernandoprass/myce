using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RulerForValueTests
   {
      private class SimpleEntity 
      {
         public string Name { get; set; }
      }

      [Fact]
      public void RuleForValue_WithSimpleObjectAndMultipleRules_ShouldAllowAnyRule()
      {
         var request = new SimpleEntity();
         var externalEmail = "test@gmail.com";

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(externalEmail)
             .IsRequired()
             .Custom(e => e.Contains("@"), new ErrorMessage("Invalid format"));

         var result = validator.Validate(request);

         Assert.True(result);
      }

      [Fact]
      public void RuleForValue_ShouldValidateExternalVariable_WhenConditionIsMet()
      {
         var request = new SimpleEntity();
         bool emailExists = false; // Simulating result from a repository

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(emailExists).IsEqualTo(false);

         var isValid = validator.Validate(request);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Fact]
      public void RuleForValue_ShouldFail_WhenExternalVariableDoesNotMeetCriteria()
      {
         var request = new SimpleEntity();
         bool emailExists = true; // Variable that should cause failure

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(emailExists, "UserEmail").IsEqualTo(false);

         var isValid = validator.Validate(request);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
         Assert.Equal("'UserEmail' must be equal to False.", validator.Messages[0].Show());
      }

      [Theory]
      [InlineData(10, true)]
      [InlineData(5, false)]
      public void RuleForValue_WithNumericRules_ShouldWorkCorrectly(int externalCount, bool expectedResult)
      {
         var request = new SimpleEntity();
         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(externalCount).IsGreaterThan(7);

         var isValid = validator.Validate(request);

         Assert.Equal(expectedResult, isValid);
      }

      [Fact]
      public void RuleForValue_WithRuleForMethod_ShouldChainCorrectly()
      {
         var request = new SimpleEntity();
         string externalToken = "ABC";

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(externalToken, "token")
                 .IsRequired()
                 .MinLength(5)
             .RuleFor(x => x.Name).IsRequired();

         var isValid = validator.Validate(request);

         Assert.False(isValid);
         Assert.Contains("token", validator.Messages.First().Show());
         Assert.Equal(2, validator.Messages.Count);
      }
   }
}
