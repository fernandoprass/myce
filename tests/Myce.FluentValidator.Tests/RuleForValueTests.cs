using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RulerForValueTests
   {
      private class SimpleEntity { }

      [Fact]
      public void RuleForValue_ShouldValidateExternalVariable_WhenConditionIsMet()
      {
         var request = new SimpleEntity();
         bool emailExists = false; // Simulating result from a repository

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(emailExists, "Email Availability").IsEqualTo(false);

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
             .RuleForValue(externalCount, "ItemsCount").IsGreaterThan(7);

         var isValid = validator.Validate(request);

         Assert.Equal(expectedResult, isValid);
      }

      [Fact]
      public void RuleForValue_WithMultipleRules_ShouldChainCorrectly()
      {
         var request = new SimpleEntity();
         string externalToken = "ABC";

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(externalToken, "SecurityToken")
                 .IsRequired()
                 .MinLength(5);

         var isValid = validator.Validate(request);

         Assert.False(isValid);
         // Should contain error from MinLength because "ABC" has length 3
         Assert.Contains("SecurityToken", validator.Messages[0].Show());
      }
   }
}
