using Xunit;

namespace Myce.Extensions.Tests
{
    /// <summary>
    /// Tests for DateTime type extensions
    /// </summary>
    public class StringExtensionTests
    {
      /// <summary>
      ///  Return an empty string if the string is null
      /// </summary>
      /// <param name="value"></param>
      /// <param name="expected"></param>
      [Theory]
      [InlineData("123", "123")]
      [InlineData("a1b2c3", "a1b2c3")]
      [InlineData("", "")]
      [InlineData(null, "")]
      public void EmptyIfIsNull(string value, string expected)
      {
         var result = value.EmptyIfIsNull();
         Assert.Equal(expected, result);
      }

      /// <summary>
      /// Remove letters and simbols, keeping only numbers
      /// </summary>
      /// <param name="value"></param>
      /// <param name="expected"></param>
      [Theory]
        [InlineData("123", "123")]
        [InlineData("a1b2c3", "123")]
        [InlineData("1 2.3,4a5b", "12345")]
        [InlineData(" 1·2-3Ò4Z5?", "12345")]
        [InlineData("abc", "")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void KeepOnlyNumbers(string value, string expected)
        {
            var result = value.KeepOnlyNumbers();
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Remove simbols keeping only numbers and letters
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("123", "123")]
        [InlineData("a1b2c3", "a1b2c3")]
        [InlineData("1 2.3,45_ab", "12345ab")]
        [InlineData(" 1·2-3Ò4Z5?", "1·23Ò4Z5")]
        [InlineData("abc", "abc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void KeepOnlyNumbersAndLetters(string value, string expected)
        {
            var result = value.KeepOnlyNumbersAndLetters();
            Assert.Equal(expected, result);
        }


        /// <summary>
        /// Remove simbols keeping only numbers, letters and spaces
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("123", "123")]
        [InlineData("a1 b2 c3", "a1 b2 c3")]
        [InlineData("1 2.3,45_ab", "1 2345ab")]
        [InlineData(" 1·2-3Ò4 Z5?", " 1·23Ò4 Z5")]
        [InlineData("abc", "abc")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void KeepOnlyNumbersAndLettersAndSpaces(string value, string expected)
        {
            var result = value.KeepOnlyNumbersAndLettersAndSpaces();
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Remove accents
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        [Theory]
        [InlineData("123a", "123a")]
        [InlineData("·1È2Ì3Û˙", "a1e2i3ou")]
        [InlineData("·ÈÌÛ˙ ¡…Õ”⁄ „√ı’ Á«.Ò—", "aeiou AEIOU aAoO cC.nN")]
        [InlineData("aeiou AEIOU", "aeiou AEIOU")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void RemoveAccents(string value, string expected)
        {
            var result = value.RemoveAccents();
            Assert.Equal(expected, result);
            Assert.Equal(expected, result);
        }

    }
}
