using Xunit;

namespace Myce.FluentValidator.Tests
{
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
