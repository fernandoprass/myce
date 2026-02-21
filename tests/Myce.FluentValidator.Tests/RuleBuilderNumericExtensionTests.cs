using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderNumericExtensionTests
   {
      public class NumericEntity
      {
         public int Age { get; set; }
         public int? Score { get; set; }
         public double Price { get; set; }
         public double? Discount { get; set; }
         public decimal Balance { get; set; }
         public decimal? Tax { get; set; }
      }

      [Theory]
      [InlineData(18, 18, true)]  // Equal
      [InlineData(20, 18, true)]  // Greater
      [InlineData(15, 18, false)] // Less
      public void IsGreaterThanOrEqualTo_Int_ShouldValidateCorrectly(int age, int minAge, bool expected)
      {
         var request = new NumericEntity { Age = age };
         var validator = new FluentValidator<NumericEntity>()
             .RuleFor(x => x.Age).IsGreaterThanOrEqualTo(minAge);

         Assert.Equal(expected, validator.Validate(request));
      }

      [Theory]
      [InlineData(null, 50, false)] // Null is invalid in our implementation
      [InlineData(60, 50, true)]   // Valid
      [InlineData(40, 50, false)]  // Invalid
      public void IsGreaterThan_NullableInt_ShouldValidateCorrectly(int? score, int threshold, bool expected)
      {
         var request = new NumericEntity { Score = score };
         var validator = new FluentValidator<NumericEntity>()
             .RuleFor(x => x.Score).IsGreaterThan(threshold);

         Assert.Equal(expected, validator.Validate(request));
      }

      [Theory]
      [InlineData(10.5, 20.0, true)]  // Valid
      [InlineData(20.0, 20.0, false)] // Invalid (not less than)
      public void IsLessThan_Double_ShouldValidateCorrectly(double price, double limit, bool expected)
      {
         var request = new NumericEntity { Price = price };
         var validator = new FluentValidator<NumericEntity>()
             .RuleFor(x => x.Price).IsLessThan(limit);

         Assert.Equal(expected, validator.Validate(request));
      }

      [Theory]
      [InlineData(100.50, 100.50, true)] // Exact decimal match
      [InlineData(99.99, 100.50, false)] // Lower
      public void IsGreaterThanOrEqualTo_Decimal_ShouldHandlePrecision(decimal balance, decimal min, bool expected)
      {
         var request = new NumericEntity { Balance = balance };
         var validator = new FluentValidator<NumericEntity>()
             .RuleFor(x => x.Balance).IsGreaterThanOrEqualTo(min);

         Assert.Equal(expected, validator.Validate(request));
      }

      [Fact]
      public void ChainedNumericRules_ShouldWorkTogether()
      {
         var request = new NumericEntity { Age = 25 };
         var validator = new FluentValidator<NumericEntity>()
             .RuleFor(x => x.Age)
             .IsGreaterThan(18)
             .IsLessThan(30);

         Assert.True(validator.Validate(request));
      }

      [Fact]
      public void ErrorMessage_ShouldContainPropertyNameAndValue()
      {
         var request = new NumericEntity { Age = 10 };
         var validator = new FluentValidator<NumericEntity>()
             .RuleFor(x => x.Age).IsGreaterThan(18);

         validator.Validate(request);
         var message = validator.Messages[0].Show();

         Assert.Contains("'Age' must be greater than 18", message);
      }
   }
}
