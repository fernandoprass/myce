using Myce.FluentValidator;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   internal class TestEntity
   {
      public int IntValue { get; set; }
      public double DoubleValue { get; set; }
      public string StringValue { get; set; }
      public DateTime DateValue { get; set; }

      // Properties to compare between fields 
      public int OtherInt { get; set; }
      public string OtherString { get; set; }
   }

   public class EqualityTests
   {
      [Theory]
      [InlineData(10, 10, true)]
      [InlineData(10, 20, false)]

      [InlineData("MYCE", "MYCE", true)]
      [InlineData("MYCE", "Easy", false)]
      [InlineData(null, null, true)]
      public void IsEqualTo_FixedValue_ShouldValidateCorrectly(object value, object expected, bool shouldBeValid)
      {
         var entity = new TestEntity { StringValue = value?.ToString(), IntValue = value is int i ? i : 0 };

         var validator = new FluentValidator<TestEntity>();

         if (value is int)
            validator.RuleFor(x => x.IntValue).IsEqualTo((int)expected);
         else
            validator.RuleFor(x => x.StringValue).IsEqualTo((string)expected);

         Assert.Equal(shouldBeValid, validator.Validate(entity));
      }

      [Theory]
      [InlineData(10.5, 10.5, true)]
      [InlineData(10.5, 10.6, false)]
      public void IsEqualTo_DoubleValue_ShouldValidateCorrectly(double val, double target, bool isValid)
      {
         var entity = new TestEntity { DoubleValue = val };
         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.DoubleValue).IsEqualTo(target);

         Assert.Equal(isValid, validator.Validate(entity));
      }

      [Fact]
      public void IsEqualTo_Dates_ShouldValidateCorrectly()
      {
         var date = new DateTime(2024, 1, 1);
         var entity = new TestEntity { DateValue = date };

         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.DateValue).IsEqualTo(date);

         Assert.True(validator.Validate(entity));
      }

      [Theory]
      [InlineData(100, 100, true)]
      [InlineData(100, 200, false)]
      [InlineData(0, 0, true)]
      public void IsEqualTo_PropertyComparison_ShouldValidateCorrectly(int valA, int valB, bool isValid)
      {
         var entity = new TestEntity { IntValue = valA, OtherInt = valB };

         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.IntValue).IsEqualTo(x => x.OtherInt);

         Assert.Equal(isValid, validator.Validate(entity));
      }

      public class IsNotEqualToTests
      {
         [Theory]
         [InlineData(100, 200, true)]   // 100 != 200 (Valid)
         [InlineData(100, 100, false)]  // 100 != 100 (Invalid)
         [InlineData("A", "B", true)]   // "A" != "B" (Valid)
         [InlineData("A", "A", false)]  // "A" != "A" (Invalid)
         public void IsNotEqualTo_FixedValue_ShouldValidateCorrectly(object val, object target, bool expected)
         {
            var entity = new TestEntity
            {
               IntValue = val is int i ? i : 0,
               StringValue = val as string
            };
            var validator = new FluentValidator<TestEntity>();

            if (val is int)
               validator.RuleFor(x => x.IntValue).IsNotEqualTo((int)target);
            else
               validator.RuleFor(x => x.StringValue).IsNotEqualTo((string)target);

            Assert.Equal(expected, validator.Validate(entity));
         }

         [Theory]
         [InlineData(10, 20, true)]  // Property A (10) != Property B (20) -> Success
         [InlineData(10, 10, false)] // Property A (10) != Property B (10) -> Failure
         public void IsNotEqualTo_PropertyComparison_ShouldValidateCorrectly(int valA, int valB, bool expected)
         {
            var entity = new TestEntity { IntValue = valA, OtherInt = valB };
            var validator = new FluentValidator<TestEntity>()
                .RuleFor(x => x.IntValue).IsNotEqualTo(x => x.OtherInt);

            Assert.Equal(expected, validator.Validate(entity));
         }
      }
   }
}