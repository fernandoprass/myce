using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for double type extensions
   /// </summary>
   public class DoubleExtensionTests
   {
      /// <summary> Validate if the value is EQUAL zero </summary>
      [Theory]
      [InlineData(0.001, false)]
      [InlineData(-0.0001, false)]
      [InlineData(50, false)]
      [InlineData(-1, false)]
      [InlineData(0, true)]
      public void EqualZero(double value, bool expected)
      {
         var result = value.EqualZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is GREATER THAN zero </summary>
      [Theory]
      [InlineData(0.001, true)]
      [InlineData(-0.0001, false)]
      [InlineData(50, true)]
      [InlineData(-1, false)]
      [InlineData(0, false)]
      public void GreaterThanZero(double value, bool expected)
      {
         var result = value.GreaterThanZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is GREATER THAN OR EQUAL zero </summary>
      [Theory]
      [InlineData(0.001, true)]
      [InlineData(-0.0001, false)]
      [InlineData(50, true)]
      [InlineData(-1, false)]
      [InlineData(0, true)]
      public void GreaterThanOrEqualZero(double value, bool expected)
      {
         var result = value.GreaterThanOrEqualZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is LESS THAN zero </summary>
      [Theory]
      [InlineData(0.001, false)]
      [InlineData(-0.0001, true)]
      [InlineData(50, false)]
      [InlineData(-1, true)]
      [InlineData(0, false)]
      public void LessThanZero(double value, bool expected)
      {
         var result = value.LessThanZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is LESS THAN OR EQUAL zero </summary>
      [Theory]
      [InlineData(0.001, false)]
      [InlineData(-0.0001, true)]
      [InlineData(50, false)]
      [InlineData(-1, true)]
      [InlineData(0, true)]
      public void LessThanOrEqualZero(double value, bool expected)
      {
         var result = value.LessThanOrEqualZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate when receive a NULL value </summary>
      [Fact]
      public void ValidateNullValues()
      {
         var result = DoubleExtension.EqualZero(null);
         Assert.False(result);

         result = DoubleExtension.GreaterThanZero(null);
         Assert.False(result);

         result = DoubleExtension.GreaterThanOrEqualZero(null);
         Assert.False(result);

         result = DoubleExtension.LessThanZero(null);
         Assert.False(result);

         result = DoubleExtension.LessThanOrEqualZero(null);
         Assert.False(result);
      }
   }
}
