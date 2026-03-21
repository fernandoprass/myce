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
      [InlineData(10, true, true)]
      [InlineData(5, false, true)]
      [InlineData(5, true, false)]
      public void RuleForValue_WithConditionalRule_ShouldWorkCorrectly(int externalCount, bool condition, bool expectedResult)
      {
         var request = new SimpleEntity();
         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(externalCount).If(condition, x => x.IsGreaterThan(7));

         var isValid = validator.Validate(request);

         Assert.Equal(expectedResult, isValid);
      }

      [Theory]
      [InlineData("ABC", true, false)]
      [InlineData("ABC", false, true)]
      [InlineData("ABCDE", true, true)]
      [InlineData("ABCD5", true, false)]
      public void RuleForValue_WithRuleForMethod_ShouldChainCorrectly(string externalToken, bool condition, bool expectedResult)
      {
         var request = new SimpleEntity() { Name = "Person Name" };

         var validator = new FluentValidator<SimpleEntity>()
             .RuleForValue(externalToken, "token")
                 .IsAlpha()
                 .If(condition, x => x.MinLength(5))
             .RuleFor(x => x.Name).IsRequired();

         var isValid = validator.Validate(request);

         Assert.Equal(expectedResult, isValid);
      }
   }
}
