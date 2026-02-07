using Xunit;

namespace Myce.Response.Messages.Tests
{
   /// <summary>
   /// Tests for Result class
   /// </summary>
   public class MessageTests
   {
      /// <summary>Validate Result with messages wtih variables</summary>
      [Theory]
      [InlineData("code1", "the {var1} and the {var2}", "the value1 and the value2")]
      [InlineData("code1", "the [var1] and the [var2]", "the value1 and the value2")]

      public void Show_MessageWitVariables_ShouldParse(string code, string text, string expected)
      {
         var message = new InformationMessage(code, text);
         message.AddVariable("var1", "value1");
         message.AddVariable("var2", "value2");

         Assert.Equal(code, message.Code);
         Assert.Equal(text, message.Text);
         Assert.NotEmpty(message.Variables);

         var result = message.Show();
         Assert.Equal(expected, result);
      }

      /// <summary>
      /// Verifies that the Show method of ErrorMessage returns the message text when no variables are provided.
      /// </summary>
      /// <remarks>This test ensures that the ErrorMessage instance correctly exposes its Code and Text
      /// properties and that the Variables collection is empty when no variables are supplied. It also confirms that
      /// calling Show returns the original message text without variable substitution.</remarks>
      [Fact]
      public void Show_MessageWitoutVariables_ShouldShowText()
      {
         string code = "code1";
         string text = "the text";
         var message = new ErrorMessage(code, text);

         Assert.Equal(code, message.Code);
         Assert.Equal(text, message.Text);
         Assert.Empty(message.Variables);

         var result = message.Show();
         Assert.Equal(text, result);
      }

      [Fact]
      public void Title_ShouldReturnExplicitValue_WhenSetManually()
      {
         var expectedTitle = "Manual Operation Title";
         var result = new Result { Title = expectedTitle };
         result.AddMessage(new InformationMessage("Background details"));

         var title = result.Title;

         Assert.Equal(expectedTitle, title);
      }

      [Fact]
      public void Title_ShouldReturnFirstMessageText_WhenTitleIsNull()
      {
         var result = new Result();
         var firstMessageText = "First error occurred";
         result.AddMessage(new ErrorMessage(firstMessageText));
         result.AddMessage(new ErrorMessage("Second error occurred"));

         var title = result.Title;

         Assert.Equal(firstMessageText, title);
      }

      [Fact]
      public void Title_ShouldReturnNull_WhenTitleIsNullAndMessagesAreEmpty()
      {
         var result = new Result();

         Assert.Null(result.Title);
      }

      [Fact]
      public void Title_ShouldBeOverridden_EvenIfMessagesExist()
      {
         var result = new Result();
         result.AddMessage(new ErrorMessage("Message text that should be ignored by getter"));

         var manualTitle = "Important Override";

         result.Title = manualTitle;
         var title = result.Title;

         Assert.Equal(manualTitle, title);
      }
   }
}
