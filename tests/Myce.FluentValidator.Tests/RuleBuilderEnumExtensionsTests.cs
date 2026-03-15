using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderEnumExtensionsTests
   {
      private enum SmallEnum : byte { OptionA = 1 }
      private enum UserStatus
      {
         Active = 1,
         Inactive = 2
      }

      private class UserTestRequest
      {
         public UserStatus Status { get; set; }
         public UserStatus? StatusNullable { get; set; }
         public SmallEnum SmallEnum { get; set; }
         public int RawStatus { get; set; }
      }

      [Fact]
      public void IsInEnum_ShouldBeValid_WhenValueIsDefined()
      {
         var request = new UserTestRequest { Status = UserStatus.Active };
         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.Status).IsInEnum();

         var isValid = validator.Validate(request);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Fact]
      public void IsInEnum_ShouldBeInvalid_WhenValueIsNotDefined()
      {
         var request = new UserTestRequest { Status = (UserStatus)99 };
         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.Status).IsInEnum();

         var isValid = validator.Validate(request);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
         Assert.Contains("'Status' has an invalid value for UserStatus.", validator.Messages.First().Show());
      }

      [Fact]
      public void IsNotInEnum_ShouldBeValid_WhenValueIsUndefined()
      {
         var request = new UserTestRequest { Status = (UserStatus)50 };
         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.Status).IsNotInEnum();

         var isValid = validator.Validate(request);

         Assert.True(isValid);
      }

      [Fact]
      public void IsInEnum_WithCustomMessage_ShouldReturnCustomErrorMessage()
      {
         var request = new UserTestRequest { Status = (UserStatus)0 };
         var customMsg = new ErrorMessage("Custom enum error");

         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.Status).IsInEnum(customMsg);

         validator.Validate(request);

         Assert.Equal("Custom enum error", validator.Messages.First().Show());
      }

      [Fact]
      public void IsInEnum_ShouldBeValid_WhenNullableEnumIsNull()
      {
         var request = new UserTestRequest { StatusNullable = null };
         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.StatusNullable).IsInEnum();

         var isValid = validator.Validate(request);

         Assert.True(isValid);
      }

      [Fact]
      public void IsInEnum_ShouldWorkWithByteUnderlyingType()
      {
         var request = new UserTestRequest { SmallEnum = (SmallEnum)1 };
         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.SmallEnum).IsInEnum();

         Assert.True(validator.Validate(request));
      }

      [Fact]
      public void IsInEnum_ShouldFail_WhenValueIsZeroButNotDefined()
      {
         var request = new UserTestRequest { Status = 0 }; // UserStatus { Active=1, Inactive=2 }
         var validator = new FluentValidator<UserTestRequest>()
            .RuleFor(x => x.Status).IsInEnum();

         Assert.False(validator.Validate(request));
      }

      [Theory]
      [InlineData("Active", true)]
      [InlineData("SuperAdmin", false)] // Value that does not exist in the Enum
      public void IsInEnum_StringConversionTest(string statusStr, bool expected)
      {
         // Simulates what would happen after a JSON Deserializer attempts to map the string.
         bool isParsed = Enum.TryParse<UserStatus>(statusStr, out var status);
         Assert.Equal(expected, isParsed);
      }
   }
}
