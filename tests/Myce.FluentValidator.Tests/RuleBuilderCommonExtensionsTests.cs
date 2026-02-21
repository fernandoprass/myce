using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderCommonExtensionsTests
   {
      private class TestEntity
      {
         public string? StringValue { get; set; }
         public int? NullableIntValue { get; set; }
         public DateTime? NullableDatetimeValue { get; set; }
         public TestEntity SubObject { get; set; }
      }

      /// <summary>
      /// Tests the IsNull validation logic.
      /// </summary>
      [Fact]
      public void IsNull_NullableValues_ShouldValidateCorrectly()
      {
         var entity = new TestEntity();

         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.NullableDatetimeValue).IsNull()
            .RuleFor(x => x.NullableIntValue).IsNull()
            .RuleFor(x => x.StringValue).IsNull()
            .RuleFor(x => x.SubObject).IsNull();

         Assert.True(validator.Validate(entity));
      }

      /// <summary>
      /// Tests the IsNotNull validation logic.
      /// </summary>
      [Fact]
      public void IsNotNull_NullableValues_ShouldValidateCorrectly()
      {
         var entity = new TestEntity { 
            NullableIntValue = 0,
            NullableDatetimeValue = DateTime.Now,
            StringValue = string.Empty,
            SubObject = new TestEntity()
         };

         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.NullableDatetimeValue).IsNotNull()
            .RuleFor(x => x.NullableIntValue).IsNotNull()
            .RuleFor(x => x.StringValue).IsNotNull()
            .RuleFor(x => x.SubObject).IsNotNull();

         Assert.True(validator.Validate(entity));
      }
   }
}