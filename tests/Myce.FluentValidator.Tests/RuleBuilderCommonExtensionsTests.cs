using Myce.Response.Messages;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

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

      private class Person
      {
         public string Email { get; set; }
         public int Age { get; set; }
         public bool IsActive { get; set; }
         public bool HasPendingTerms { get; set; }
      }

      [Fact]
      public void IsTrue_ShouldPass_WhenValueIsTrue()
      {
         var user = new Person { IsActive = true };
         var validator = new FluentValidator<Person>()
             .RuleFor(x => x.IsActive).IsTrue(new ErrorMessage("User must be active"));

         var result = validator.Validate(user);

         Assert.True(result);
      }

      [Fact]
      public void IsFalse_ShouldPass_WhenValueIsFalse()
      {
         var user = new Person { HasPendingTerms = false };
         var validator = new FluentValidator<Person>()
             .RuleFor(x => x.HasPendingTerms).IsFalse(new ErrorMessage("No pending terms allowed"));

         var result = validator.Validate(user);

         Assert.True(result);
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
      /// Tests the IsNull return the rigth message.
      /// </summary>
      [Fact]
      public void IsNull_WithDifferentMessage_ShouldValidateCorrectly()
      {
         var entity = new TestEntity
         {
            NullableDatetimeValue = DateTime.Now,
            NullableIntValue = 0
         };

         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.NullableDatetimeValue).IsNull()
            .RuleFor(x => x.NullableIntValue).IsNull(new ErrorMessage("not found"));

         var result = validator.Validate(entity);

         Assert.False(result);
         Assert.Equal("'NullableDatetimeValue' is not null.", validator.Messages.First().Show());
         Assert.Equal("not found",validator.Messages.Last().Show());
      }

      /// <summary>
      /// Tests the IsNull return the rigth message.
      /// </summary>
      [Fact]
      public void IsNotNull_WithDifferentMessage_ShouldValidateCorrectly()
      {
         var entity = new TestEntity();

         var validator = new FluentValidator<TestEntity>()
            .RuleFor(x => x.NullableDatetimeValue).IsNotNull()
            .RuleFor(x => x.NullableIntValue).IsNotNull(new ErrorMessage("it shoud be not null"));

         var result = validator.Validate(entity);

         Assert.False(result);
         Assert.Equal("'NullableDatetimeValue' is null.", validator.Messages.First().Show());
         Assert.Equal("it shoud be not null", validator.Messages.Last().Show());
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

      /// <summary>
      /// Tests the Custom validation logic with a simple even number rule.
      /// </summary>
      [Fact]
      public void Custom_ShouldPass_WhenCustomLogicReturnsTrue()
      {
         var person = new Person { Age = 20 };

         Func<int, bool> isEvenRule = age => age % 2 == 0;

         var validator = new FluentValidator<Person>()
             .RuleFor(x => x.Age)
             .Custom(isEvenRule, new ErrorMessage("Age must be an even number."));

         var isValid = validator.Validate(person);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      /// <summary>
      /// Tests the Custom validation logic with a corporate email rule, 
      /// ensuring it fails and returns the correct error message when the logic returns false.
      /// </summary>
      [Fact]
      public void Custom_ShouldFailAndReturnMessage_WhenCustomLogicReturnsFalse()
      {
         var person = new Person { Email = "john@gmail.com" };
         var errorMessage = new ErrorMessage("Only corporate emails are allowed.");

         Func<string, bool> isCorporateEmailRule = email =>
             !string.IsNullOrEmpty(email) && email.EndsWith("@myce.com");

         var validator = new FluentValidator<Person>()
             .RuleFor(x => x.Email)
             .Custom(isCorporateEmailRule, errorMessage);

         var isValid = validator.Validate(person);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
         Assert.Equal("Only corporate emails are allowed.", validator.Messages.First().Show());
      }

      /// <summary>
      /// Tests that the Custom validation can be chained with other rules, and that all rules are evaluated correctly, 
      /// returning the appropriate error messages when validation fails.
      /// </summary>
      [Fact]
      public void Custom_CanBeChainedWithOtherRules()
      {
         var person = new Person { Email = "invalid-email" }; 

         var validator = new FluentValidator<Person>()
             .RuleFor(x => x.Email)
             .IsNotNull()
             .Custom(email => email.Contains("@"), new ErrorMessage("Must contain @ symbol"));

         var isValid = validator.Validate(person);

         Assert.False(isValid);
         Assert.Contains(validator.Messages, m => m.Show() == "Must contain @ symbol");
      }
   }
}