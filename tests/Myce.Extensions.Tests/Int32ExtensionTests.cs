using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for Int32 type extensions
   /// </summary>
   public class Int32ExtensionTests
   {
      /// <summary> Validate if the value is EQUAL zero </summary>
      [Theory]
      [InlineData(-1, false)]
      [InlineData(1, false)]
      [InlineData(int.MinValue, false)]
      [InlineData(int.MaxValue, false)]
      [InlineData(0, true)]
      public void EqualZero(int value, bool expected)
      {
         var result = value.EqualZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is GREATER THAN zero </summary>
      [Theory]
      [InlineData(-1, false)]
      [InlineData(1, true)]
      [InlineData(int.MinValue, false)]
      [InlineData(int.MaxValue, true)]
      [InlineData(0, false)]
      public void GreaterThanZero(int value, bool expected)
      {
         var result = value.GreaterThanZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is GREATER THAN OR EQUAL zero </summary>
      [Theory]
      [InlineData(-1, false)]
      [InlineData(1, true)]
      [InlineData(int.MinValue, false)]
      [InlineData(int.MaxValue, true)]
      [InlineData(0, true)]
      public void GreaterThanOrEqualZero(int value, bool expected)
      {
         var result = value.GreaterThanOrEqualZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is LESS THAN zero </summary>
      [Theory]
      [InlineData(-1, true)]
      [InlineData(1, false)]
      [InlineData(int.MinValue, true)]
      [InlineData(int.MaxValue, false)]
      [InlineData(0, false)]
      public void LessThanZero(int value, bool expected)
      {
         var result = value.LessThanZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate if the value is LESS THAN OR EQUAL zero </summary>
      [Theory]
      [InlineData(-1, true)]
      [InlineData(1, false)]
      [InlineData(int.MinValue, true)]
      [InlineData(int.MaxValue, false)]
      [InlineData(0, true)]
      public void LessThanOrEqualZero(int value, bool expected)
      {
         var result = value.LessThanOrEqualZero();
         Assert.Equal(expected, result);
      }

      /// <summary> Validate when receive a NULL value </summary>
      [Fact]
      public void ValidateNullValues()
      {
         var result = Int32Extension.EqualZero(null);
         Assert.False(result);

         result = Int32Extension.GreaterThanZero(null);
         Assert.False(result);

         result = Int32Extension.GreaterThanOrEqualZero(null);
         Assert.False(result);

         result = Int32Extension.LessThanZero(null);
         Assert.False(result);

         result = Int32Extension.LessThanOrEqualZero(null);
         Assert.False(result);
      }
   }
}
