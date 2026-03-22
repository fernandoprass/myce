using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleForIfElseTests
   {
      private class Person
      {
         public string Name { get; set; } = string.Empty;
         public int Age { get; set; }
         public bool IsActive { get; set; }
         public string Category { get; set; } = string.Empty;
      }

      [Theory]
      [InlineData(true, "VALID", true)]
      [InlineData(true, "INVALID", false)]
      [InlineData(false, "ANYTHING", true)]
      public void If_MultipleRulesInBlock_ReturnsExpectedValidity(bool isActive, string name, bool expected)
      {
         var person = new Person { IsActive = isActive, Name = name };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .If(x => x.IsActive, rb =>
            {
               rb.IsRequired();
               rb.IsEqualTo("VALID");
            });

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, "Admin", "ADMIN_SECRET", true)]
      [InlineData(true, "Admin", "WRONG", false)]
      [InlineData(true, "Guest", "ANY_NAME", true)]
      [InlineData(false, "Admin", "ANY_NAME", true)]
      public void If_NestedScopes_CleansUpAndReturnsExpectedValidity(bool isActive, string category, string name, bool expected)
      {
         var person = new Person { IsActive = isActive, Category = category, Name = name };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .If(x => x.IsActive, rb =>
            {
               rb.IsRequired();
               rb.If(p => p.Category == "Admin", innerRb =>
               {
                  innerRb.IsEqualTo("ADMIN_SECRET");
               });
            });

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, "IF_VAL", true)]
      [InlineData(true, "WRONG", false)]
      [InlineData(false, "ELSE_VAL", true)]
      [InlineData(false, "WRONG", false)]
      public void IfElse_FunctionalCondition_SwitchesBlocksAndReturnsExpectedValidity(bool condition, string name, bool expected)
      {
         var person = new Person { IsActive = condition, Name = name };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .If(x => x.IsActive,
               ifBlock: rb => rb.IsEqualTo("IF_VAL"),
               elseBlock: rb => rb.IsEqualTo("ELSE_VAL")
            );

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, "START", "REQUIRED", true)]
      [InlineData(true, "WRONG", "REQUIRED", false)]
      [InlineData(false, "ANY", "REQUIRED", true)]
      [InlineData(false, "ANY", "", false)]
      public void If_RulesAfterBlock_AlwaysExecutesAndReturnsExpectedValidity(bool condition, string name, string category, bool expected)
      {
         var person = new Person { IsActive = condition, Name = name, Category = category };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .If(x => x.IsActive, rb => rb.IsEqualTo("START"))
            .IsRequired();

         validator.RuleFor(x => x.Category).IsRequired();

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, false)]
      [InlineData(false, true)]
      public void If_BooleanOverload_ExecutesBasedOnFlagAndReturnsExpectedValidity(bool staticCondition, bool expected)
      {
         var person = new Person { Name = "" };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .If(staticCondition, rb => rb.IsRequired());

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, 25, true)]
      [InlineData(true, 15, false)]
      [InlineData(false, 15, true)]
      public void If_RuleForValueCondition_ScopesCorrectlyAndReturnsExpectedValidity(bool isActive, int testAge, bool expected)
      {
         var person = new Person { IsActive = isActive, Age = testAge };
         var validator = new FluentValidator<Person>();

         validator.RuleForValue(person.Age)
            .If(p => p.IsActive, rb =>
            {
               rb.IsGreaterThanOrEqualTo(18);
            });

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData("Admin", "SUPER_S3CRET", true)]
      [InlineData("Admin", "WRONG", false)]
      [InlineData("User", "ANY", true)]
      [InlineData("User", "", false)]
      public void IfElse_RuleForValueCondition_HandlesBothPathsAndReturnsExpectedValidity(string category, string token, bool expected)
      {
         var person = new Person { Category = category };
         var validator = new FluentValidator<Person>();

         validator.RuleForValue(token)
            .If(p => p.Category == "Admin",
               ifBlock: rb => rb.IsEqualTo("SUPER_S3CRET"),
               elseBlock: rb => rb.IsRequired()
            );

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, "Admin", 10, true)]
      [InlineData(true, "Admin", 3, false)]
      [InlineData(true, "Guest", 3, true)]
      [InlineData(false, "Admin", 3, true)]
      public void If_NestedRuleForValue_MaintainsOnionScopeAndReturnsExpectedValidity(bool isActive, string category, int priority, bool expected)
      {
         var person = new Person { IsActive = isActive, Category = category };
         var validator = new FluentValidator<Person>();

         validator.RuleForValue(priority)
            .If(p => p.IsActive, rb =>
            {
               rb.If(p => p.Category == "Admin", innerRb =>
               {
                  innerRb.IsGreaterThan(5);
               });
            });

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }
   }
}