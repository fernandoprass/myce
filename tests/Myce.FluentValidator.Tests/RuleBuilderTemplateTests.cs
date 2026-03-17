using Myce.FluentValidator;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderTemplateTests
   {
      private class Person
      {
         public string Name { get; set; }
         public string Email { get; set; }
         public bool IsEmployee { get; set; }
      }

      /// <summary>
      /// Logic for basic mandatory string fields.
      /// </summary>
      private static void BasicStringTemplate<T>(RuleBuilder<T, string> rb) where T : class
          => rb.IsRequired();

      /// <summary>
      /// Logic for minimum length requirements.
      /// </summary>
      private static void MinLengthTemplate<T>(RuleBuilder<T, string> rb) where T : class
          => rb.Custom(val => val != null && val.Length >= 5, new ErrorMessage("Too short"));

      /// <summary>
      /// Logic for specific corporate domain requirements.
      /// </summary>
      private static void CorporateDomainTemplate<T>(RuleBuilder<T, string> rb) where T : class
          => rb.Custom(val => val != null && val.EndsWith("@myce.com"), new ErrorMessage("Invalid domain"));

      [Fact]
      public void ApplyTemplate_ShouldWorkWithMultipleTemplatesChained()
      {
         var user = new Person { Name = "Jo" }; // Too short for MinLengthTemplate
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
             .ApplyTemplate(BasicStringTemplate).RuleFor(x => x.Name)
             .ApplyTemplate(MinLengthTemplate);

         var isValid = validator.Validate(user);

         Assert.False(isValid);
         Assert.Contains(validator.Messages, m => m.Text == "Too short");
      }

      [Fact]
      public void ApplyTemplate_ShouldFail_WhenAnyTemplateInTheChainFails()
      {
         var user = new Person { Email = "user@gmail.com" };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Email)
             .ApplyTemplate(BasicStringTemplate)
             .ApplyTemplate(CorporateDomainTemplate);

         var isValid = validator.Validate(user);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
         Assert.Equal("Invalid domain", validator.Messages.First().Show());
      }

      [Fact]
      public void ApplyTemplate_ShouldPass_WhenAllTemplatesInTheChainSucceed()
      {
         var user = new Person { Name = "Jonathan", Email = "admin@myce.com" };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
             .ApplyTemplate(BasicStringTemplate)
             .ApplyTemplate(MinLengthTemplate);

         validator.RuleFor(x => x.Email)
             .ApplyTemplate(BasicStringTemplate)
             .ApplyTemplate(CorporateDomainTemplate);

         var isValid = validator.Validate(user);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Fact]
      public void ApplyTemplate_WithRuleForValue_ShouldChainSucceed()
      {
         var user = new Person { Name = "Jonathan", Email = "admin@myce.com" };
         var validator = new FluentValidator<Person>();

         var trueVariable = true;

         validator.RuleFor(x => x.Name)
             .ApplyTemplate(BasicStringTemplate)
             .ApplyTemplate(MinLengthTemplate);

         validator.RuleFor(x => x.Email)
             .ApplyTemplate(BasicStringTemplate)
             .ApplyTemplate(CorporateDomainTemplate)
             .RuleForValue(trueVariable).IsTrue();

         var isValid = validator.Validate(user);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Theory]
      [InlineData("external@gmail.com", false, true)] // Not employee: Corporate template skipped
      [InlineData("staff@myce.com", true, true)]      // Employee: Corporate template passes
      [InlineData("staff@gmail.com", true, false)]     // Employee: Corporate template fails
      [InlineData("", false, false)]                  // Not employee: Basic template (Required) fails
      public void MultipleTemplates_WithConditionalLogic(string email, bool isEmployee, bool expected)
      {
         var person = new Person { Email = email, IsEmployee = isEmployee };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Email)
            .ApplyTemplate(BasicStringTemplate)     // Always applied
            .ApplyTemplate(CorporateDomainTemplate) // Applied...
            .If(x => x.IsEmployee);                 // ...but only if this condition is met

         var isValid = validator.Validate(person);

         Assert.Equal(expected, isValid);
      }
   }
}