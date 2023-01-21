using Myce.Response.Messages;
using Xunit;

namespace Myce.Response.Tests
{
   /// <summary>
   /// Tests for Result class
   /// </summary>
   public class ResultTests
   {
      /// <summary>Validate without messages and with data</summary>
      [Fact]
      public void Result_WithStringMessage_ShouldBeValid()
      {
         string message = "text message";
         var result = new Result(message);

         Assert.Equal(message, result.Message);
         Assert.False(result.HasError);
         Assert.False(result.HasWarning);
         Assert.False(result.HasMessage);
         Assert.Empty(result.Messages);
         Assert.True(result.IsValid);
      }

      /// <summary>Validate without messages and with data</summary>
      [Fact]
      public void Result_WithoutMessagesAndWithData_ShouldRetunDataAndBeValid()
      {
         var result = new Result<int>(20);

         Assert.Null(result.Message);
         Assert.Empty(result.Messages);
         Assert.False(result.HasErrorOrDataIsNull);
         Assert.False(result.HasError);
         Assert.False(result.HasWarning);
         Assert.False(result.HasMessage);
         Assert.True(result.HasData);
         Assert.True(result.IsValid);
         Assert.Equal(20, result.Data);
      }

      /// <summary>Validate without messages and without data</summary>
      [Fact]
      public void Result_WithoutMessagesAndWithoutData_ShouldRetunDataAndBeValid()
      {
         var result = new Result<string>();

         Assert.Null(result.Message);
         Assert.Null(result.Data);
         Assert.Empty(result.Messages);
         Assert.False(result.HasData);
         Assert.False(result.HasError);
         Assert.False(result.HasWarning);
         Assert.False(result.HasMessage);
         Assert.True(result.HasErrorOrDataIsNull);
         Assert.True(result.IsValid);
      }

      /// <summary>Validate result with error messages</summary>
      [Fact]
      public void Result_WithErrorMessages_ShouldBeInvalid()
      {
         var errorMessage = new ErrorMessage("code1","error1" );

         var result = new Result();
         result.AddMessage(errorMessage);

         Assert.Null(result.Message);
         Assert.Single(result.Messages);
         Assert.False(result.HasWarning);
         Assert.False(result.IsValid);
         Assert.True(result.HasError);
         Assert.True(result.HasMessage);
         Assert.Equal("code1", result.Messages.First().Code);
         Assert.Equal("error1", result.Messages.First().Text);
         Assert.IsType<ErrorMessage>(result.Messages.First());
      }

      /// <summary>Validate result with warning messages</summary>
      [Fact]
      public void Result_WithWarningMessagess_ShouldBeValid()
      {
         var warningMessage = new WarningMessage("code1", "warning1");

         var result = new Result(warningMessage);

         Assert.Null(result.Message);
         Assert.Single(result.Messages);
         Assert.False(result.HasError);
         Assert.True(result.HasWarning);
         Assert.True(result.IsValid);
         Assert.True(result.HasMessage);
         Assert.Equal("code1", result.Messages.First().Code);
         Assert.Equal("warning1", result.Messages.First().Text);
         Assert.IsType<WarningMessage>(result.Messages.First());
      }

      /// <summary>Validate result with information messages</summary>
      [Fact]
      public void Result_WithInformationMessages_ShouldBeValid()
      {
         var infoMessage = new InformationMessage("code1", "info1");

         var result = new Result(infoMessage);

         Assert.Null(result.Message);
         Assert.Single(result.Messages);
         Assert.False(result.HasError);
         Assert.False(result.HasWarning);
         Assert.True(result.IsValid);
         Assert.True(result.HasMessage);
         Assert.Equal("code1", result.Messages.First().Code);
         Assert.Equal("info1", result.Messages.First().Text);
         Assert.IsType<InformationMessage>(result.Messages.First());
      }

      /// <summary>Validate Result with multiple type of messages</summary>
      [Fact]
      public void Result_WithMultipleTypesOfMessages_ShouldBeInvalid()
      {
         var errorMessage = new ErrorMessage("code1", "error1");
         var warningMessage = new WarningMessage("code2", "warning1");
         var infoMessage = new InformationMessage("code3", "information1");

         var result = new Result();
         result.AddMessage(errorMessage);
         result.AddMessage(warningMessage);
         result.AddMessage(infoMessage);

         Assert.Null(result.Message);
         Assert.False(result.IsValid);
         Assert.True(result.HasError);
         Assert.True(result.HasWarning);
         Assert.True(result.HasMessage);
         Assert.Equal(3, result.Messages.Count);
      }
   }
}
