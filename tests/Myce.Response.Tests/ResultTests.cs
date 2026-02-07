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

         Assert.Equal(message, result.Title);
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

         Assert.Null(result.Title);
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

         Assert.Null(result.Title);
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

         var result = Result.Failure(errorMessage);

         Assert.Single(result.Messages);
         Assert.False(result.HasWarning);
         Assert.False(result.IsValid);
         Assert.True(result.HasError);
         Assert.True(result.HasMessage);
         Assert.Equal(result.Messages.First().Text, result.Title);
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

         Assert.Single(result.Messages);
         Assert.False(result.HasError);
         Assert.True(result.HasWarning);
         Assert.True(result.IsValid);
         Assert.True(result.HasMessage);
         Assert.Equal(result.Messages.First().Text, result.Title);
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

         Assert.Single(result.Messages);
         Assert.False(result.HasError);
         Assert.False(result.HasWarning);
         Assert.True(result.IsValid);
         Assert.True(result.HasMessage);
         Assert.Equal(result.Messages.First().Text, result.Title);
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

         Assert.False(result.IsValid);
         Assert.True(result.HasError);
         Assert.True(result.HasWarning);
         Assert.True(result.HasMessage);
         Assert.Equal(result.Messages.First().Text, result.Title);
         Assert.Equal(3, result.Messages.Count);
      }
      /// <summary>Validate if MapData preserves messages </summary>
      [Fact]
      public void ToResult_ShouldMapDataAndPreserveMessages()
      {
         var initialData = 10;
         var resultInt = Result<int>.Success(initialData);
         resultInt.AddMessage(new InformationMessage("MSG01", "Initial message"));
         resultInt.AddMessage(new WarningMessage("MSG02", "Warning message"));

         string MapIntToString(int value) => $"Value is {value}";

         var resultString = resultInt.ToResult(MapIntToString);

         Assert.Equal("Value is 10", resultString.Data);

         Assert.Equal(resultInt.Messages.Count, resultString.Messages.Count);
         Assert.Equal(resultInt.Messages.First().Code, resultString.Messages.First().Code);
         Assert.Equal(resultInt.Messages.Last().Type, resultString.Messages.Last().Type);
      }

      /// <summary> Validade if return default data when source is null </summary>
      [Fact]
      public void ToResult_ShouldReturnDefaultData_WhenSourceDataIsNull()
      {
         var resultNull = new Result<object>((object)null);
         resultNull.AddMessage(new ErrorMessage("ERR01", "Error occurred"));

         // Mapping object (null) to int
         var resultInt = resultNull.ToResult(obj => 100);

         // Since Data is null, the map function is not executed and returns default(int)
         Assert.False(resultInt.IsValid);
         Assert.Equal(0, resultInt.Data);         
         Assert.Equal("ERR01", resultInt.Messages.First().Code);
      }
   }
}
