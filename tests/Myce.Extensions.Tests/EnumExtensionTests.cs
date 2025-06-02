using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for Enum type class extensions
   /// </summary>
   public class EnumExtensionTests
   {
      public enum ProgrammingLanguage { None, CSharp, Python, JavaScript }

      /// <summary> Validate if it is returning the description </summary>
      [Theory]
      [InlineData(ProgrammingLanguage.None, "None")]
      [InlineData(ProgrammingLanguage.CSharp, "CSharp")]
      [InlineData(ProgrammingLanguage.Python, "Python")]
      [InlineData(ProgrammingLanguage.JavaScript, "JavaScript")]

      public void GetDescription(ProgrammingLanguage programmingLanguage, string expected)
      {
         var result = programmingLanguage.GetDescription();
         Assert.Equal(expected, result);
      }
   }
}
