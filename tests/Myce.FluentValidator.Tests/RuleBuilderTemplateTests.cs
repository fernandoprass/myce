using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderTemplateTests
   {
      private class Person
      {
         public string Name { get; set; } = string.Empty;
         public string Email { get; set; } = string.Empty;
         public bool IsEmployee { get; set; }
         public int AccessLevel { get; set; }
      }

      // Simple template using RuleFor
      private static void BasicNameTemplate<T>(RuleBuilder<T, string> rb) where T : class
         => rb.IsRequired().MaxLength(10);

      // Template mixing RuleFor and RuleForValue
      private static void AccountSecurityTemplate<T>(RuleBuilder<T, string> rb) where T : class
      {
         var minComplexityMet = true;
         rb.IsRequired()
           .MinLength(8)
           .RuleForValue(minComplexityMet).IsTrue();
      }

      // Template with an internal IF
      private static void RoleBasedEmailTemplate<T>(RuleBuilder<T, string> rb) where T : class
      {
         rb.IsRequired()
           .If(x => x is Person p && p.IsEmployee,
               inner => inner.Custom(val => val.EndsWith("@myce.com"), new ErrorMessage("Corporate email required"))
           );
      }

      [Theory]
      [InlineData("ValidName", true)]
      [InlineData("", false)]                   // Fails IsRequired
      [InlineData("NameTooLongForThis", false)] // Fails MaxLength
      public void ApplyTemplate_SimpleTwoRules_ReturnsExpectedValidity(string name, bool expected)
      {
         var person = new Person { Name = name };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name).ApplyTemplate(BasicNameTemplate);

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData("StrongPass123", true)]
      [InlineData("Short", false)] // Fails MinLength(8)
      [InlineData("", false)]      // Fails IsRequired
      public void ApplyTemplate_ThreeRulesWithRuleForValue_ReturnsExpectedValidity(string email, bool expected)
      {
         var person = new Person { Email = email };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Email).ApplyTemplate(AccountSecurityTemplate);

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, "staff@myce.com", true)]   // Employee + Correct Domain = Pass
      [InlineData(true, "staff@gmail.com", false)]  // Employee + Wrong Domain = Fail
      [InlineData(false, "guest@gmail.com", true)]  // Not Employee = Internal IF skipped = Pass
      public void ApplyTemplate_InternalIfLogic_ReturnsExpectedValidity(bool isEmployee, string email, bool expected)
      {
         var person = new Person { IsEmployee = isEmployee, Email = email };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Email).ApplyTemplate(RoleBasedEmailTemplate);

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData(true, "ValidName", true)]   // Condition true + Valid Template = Pass
      [InlineData(true, "WayTooLongName", false)] // Condition true + Invalid Template = Fail
      [InlineData(false, "WayTooLongName", true)]  // Condition false + Invalid Template = Pass (Skipped)
      public void If_ExternalScopeApplyingTemplate_ReturnsExpectedValidity(bool isEmployee, string name, bool expected)
      {
         var person = new Person { IsEmployee = isEmployee, Name = name };
         var validator = new FluentValidator<Person>();

         // 4. Test applying the IF scope to the whole template
         validator.RuleFor(x => x.Name)
            .If(x => x.IsEmployee, rb => rb.ApplyTemplate(BasicNameTemplate));

         var isValid = validator.Validate(person);
         Assert.Equal(expected, isValid);
      }

      [Theory]
      [InlineData("John Smith", "staff@gmail.com", true, 1)]       // Fails Email internal IF
      [InlineData("John Lewis Smith", "staff@gmail.com", true, 2)] // Fails Name template and Email internal IF
      [InlineData("John Smith", "staff@myce.com", true, 0)]        // Passes both templates appling the internal IF
      [InlineData("John Smith", "staff@gmail.com", false, 0)]      // Passes both templates ignoring the internal IF
      public void ApplyTemplate_ChainedTemplates_ReturnsIndividualErrorMessages(string name, string email, bool isEmployee, int expectedErrorCount)
      {
         var person = new Person { Name = name, Email = email, IsEmployee = isEmployee };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name).ApplyTemplate(BasicNameTemplate);
         validator.RuleFor(x => x.Email).ApplyTemplate(RoleBasedEmailTemplate);

         validator.Validate(person);

         Assert.Equal(expectedErrorCount, validator.Messages.Count);
      }

      [Theory]
      [InlineData(false, "", true)]            // Condition False: Empty Name (Required in Template) is IGNORED -> Pass
      [InlineData(false, "TooLongName", true)] // Condition False: Long Name (MaxLength in Template) is IGNORED -> Pass
      [InlineData(true, "", false)]            // Condition True: Template is EXECUTED, Empty Name -> Fail
      [InlineData(true, "Valid", true)]        // Condition True: Template is EXECUTED, Valid Name -> Pass
      public void If_BlockWithTemplate_SkipsAllInternalRulesWhenConditionIsFalse(bool isEmployee, string name, bool expected)
      {
         var person = new Person { IsEmployee = isEmployee, Name = name };
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Name)
            .If(x => x.IsEmployee, rb => rb.ApplyTemplate(BasicNameTemplate));

         var isValid = validator.Validate(person);

         Assert.Equal(expected, isValid);
      }
   }
}