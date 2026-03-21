using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleForTests
   {
      private enum Gender { Female, Male }
      private class Person
      {
         public string Code { get; set; }
         public string Name { get; set; }
         public double? Salary { get; set; }
         public int Age { get; set; }
         public bool IsActive { get; set; }
         public DateTime BirthDate { get; set; }
         public Gender Gender { get; set; }
      }

      /// <summary>
      /// Mix multiple rules on different properties and ensure that all validations are executed and the correct error messages are generated.
      /// </summary>
      [Fact]
      public void RuleFor_MixingMultipleRules_ShouldValidate()
      {
         var person = new Person { 
            Code = "123A", 
            Name = "John Smith", 
            Gender = Gender.Male,
            Age = 17, 
            IsActive = true,
            Salary = 0, 
            BirthDate = new DateTime(1978, 02, 22) 
         };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
               .IsRequired()
               .ContainsOnlyNumber()
            .RuleFor(x => x.Name)
               .IsRequired()
               .IsEqualTo("John Smith")
            .RuleFor(x => x.Gender).IsInEnum()
            .RuleFor(x => x.Age)
               .IsGreaterThanOrEqualTo(18)
               .IsLessThanOrEqualTo(65)
             .RuleFor(x => x.Salary)
               .IsGreaterThanOrEqualTo(500).If(x => x.Age >= 18)
             .RuleFor(x => x.BirthDate).IsInThePast();

         var result = validator.Validate(person);

         var errorMessageCode = validator.Messages.First();
         Assert.Equal("Code", errorMessageCode.Variables.First().Value);
         Assert.IsType<ShouldContainOnlyNumberError>(errorMessageCode);

         var errorMessageAge = validator.Messages.Last();
         Assert.Contains("Age", errorMessageAge.ToString());      
         Assert.IsType<IsGreaterThanOrEqualToError>(errorMessageAge);

         Assert.Equal(2, validator.Messages.Count());
      }

      /// <summary>
      /// Tests that 'If' only affects the rule immediately preceding it.
      /// Subsequent rules in the chain must always execute.
      /// </summary>
      [Theory]
      [InlineData(15, "John", true)]    // Age < 18: IsRequired(If) skipped, others pass
      [InlineData(20, "John", true)]    // Age >= 18: IsRequired passes, others pass
      [InlineData(20, "", false)]      // Age >= 18: IsRequired fails
      [InlineData(15, "J", false)]     // Age < 18: IsRequired skipped, but MinLength(3) fails
      public void If_ShouldOnlyAffectThePrecedingRule(int age, string name, bool expected)
      {
         var person = new Person { Age = age, Name = name };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .IsRequired().If(x => x.Age >= 18) // Conditional requirement
            .MinLength(3)                     // Always mandatory
            .IsAlpha();                       // Always mandatory

         Assert.Equal(expected, validator.Validate(person));
      }

      /// <summary>
      /// Tests 'If' functionality when using RuleForValue to validate external data
      /// based on the state of the Person instance.
      /// </summary>
      [Theory]
      [InlineData("GOLD_MEMBER", true, true)]   // Active: Code check runs and passes
      [InlineData("SHORT", true, false)]        // Active: Code check runs and fails
      [InlineData("SHORT", false, true)]       // Inactive: Code check is skipped
      public void RuleForValue_WithIf_ShouldValidateCondition(string externalCode, bool isActive, bool expected)
      {
         var person = new Person { IsActive = isActive };
         var validator = new FluentValidator<Person>();

         // Validates an external string based on the Person's IsActive status
         validator.RuleForValue(externalCode)
            .MinLength(10).If(x => x.IsActive);

         Assert.Equal(expected, validator.Validate(person));
      }

      /// <summary>
      /// Tests multiple independent 'If' conditions applied to different rules 
      /// within the same property chain.
      /// </summary>
      [Theory]
      [InlineData("John", 20, false, true)]  // Age >= 18 (Name required), but Name is "John" (Valid)
      [InlineData("", 10, true, false)]  // Age < 18 (Name not required), but IsActive is true (IsAlpha fails on "123")
      [InlineData("John Smith", 20, true, true)]  // Both conditions true: IsRequired passes, but IsAlpha fails
      public void Multiple_Independent_If_Conditions(string name, int age, bool isActive, bool expected)
      {
         var person = new Person
         {
            Name = name,
            Age = age,
            IsActive = isActive
         };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .IsRequired().If(x => x.Age >= 18)
            .IsAlpha().If(x => x.IsActive);

         Assert.Equal(expected, validator.Validate(person));
      }

      /// <summary>
      /// Tests 'If' combined with a custom logic rule (AddRule) and date properties.
      /// </summary>
      [Fact]
      public void If_WithCustomRuleAndDates_ShouldWork()
      {
         var person = new Person
         {
            IsActive = false,
            BirthDate = DateTime.Now.AddYears(1) // Future date (invalid for birth)
         };

         var validator = new FluentValidator<Person>();

         // Custom rule: BirthDate must be in the past, but only IF the person is Active
         validator.RuleFor(x => x.BirthDate)
            .IsInThePast(new ErrorMessage("Invalid birth date"))
            .If(x => x.IsActive);

         // Should pass because IsActive is false, so the past date check is ignored
         Assert.True(validator.Validate(person));

         // Now make it fail
         person.IsActive = true;
         Assert.False(validator.Validate(person));
      }
   }
}
