using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for Decimal type extensions
   /// </summary>
   public class DecimalExtensionTests
   {
      /// <summary> Validate if the value is EQUAL zero </summary>
      [Theory]
      [InlineData("0.001", false)]
      [InlineData("-0.0001", false)]
      [InlineData("50", false)]
      [InlineData("-1", false)]
      [InlineData("0", true)]
      public void EqualZero(string value, bool expected)
      {
         var decimalValue = decimal.Parse(value);
         var result = DecimalExtension.EqualZero(decimalValue);
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is GREATER THAN zero </summary>
      [Theory]
      [InlineData("0.001", true)]
      [InlineData("-0.0001", false)]
      [InlineData("50", true)]
      [InlineData("-1", false)]
      [InlineData("0", false)]
      public void GreaterThanZero(string value, bool expected)
      {
         var decimalValue = decimal.Parse(value);
         var result = DecimalExtension.GreaterThanZero(decimalValue);
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is GREATER THAN OR EQUAL zero </summary>
      [Theory]
      [InlineData("0.001", true)]
      [InlineData("-0.0001", false)]
      [InlineData("50", true)]
      [InlineData("-1", false)]
      [InlineData("0", true)]
      public void GreaterThanOrEqualZero(string value, bool expected)
      {
         var decimalValue = decimal.Parse(value);
         var result = DecimalExtension.GreaterThanOrEqualZero(decimalValue);
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is LESS THAN zero </summary>
      [Theory]
      [InlineData("0.001", false)]
      [InlineData("-0.0001", true)]
      [InlineData("50", false)]
      [InlineData("-1", true)]
      [InlineData("0", false)]
      public void LessThanZero(string value, bool expected)
      {
         var decimalValue = decimal.Parse(value);
         var result = DecimalExtension.LessThanZero(decimalValue);
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is LESS THAN OR EQUAL zero </summary>
      [Theory]
      [InlineData("0.001", false)]
      [InlineData("-0.0001", true)]
      [InlineData("50", false)]
      [InlineData("-1", true)]
      [InlineData("0", true)]
      public void LessThanOrEqualZero(string value, bool expected)
      {
         var decimalValue = decimal.Parse(value);
         var result = DecimalExtension.LessThanOrEqualZero(decimalValue);
         Assert.Equal(expected, result);
      }

      /// <summary> Validate when receive a NULL value </summary>
      [Fact]
      public void ValidateNullValues()
      {
         var result = DecimalExtension.EqualZero(null);
         Assert.False(result);

         result = DecimalExtension.GreaterThanZero(null);
         Assert.False(result);

         result = DecimalExtension.GreaterThanOrEqualZero(null);
         Assert.False(result);

         result = DecimalExtension.LessThanZero(null);
         Assert.False(result);

         result = DecimalExtension.LessThanOrEqualZero(null);
         Assert.False(result);
      }

      /// <summary> Check if the value of the decimal is BETWEEN other two values </summary>
      [Fact]
      public void IsBetween()
      {
         var today = DateTime.Now;
         var yesterday = today.AddDays(-1);
         var tomorrow = today.AddDays(1);

         Assert.True(today.IsBetween(yesterday, tomorrow));
         Assert.True(123d.IsBetween(122.9999d, 123.00001d));
         Assert.True(0.0003m.IsBetween(0.00029m, 0.0003m));

         Assert.True("b".IsBetween("a", "c"));
      }
   }
}
