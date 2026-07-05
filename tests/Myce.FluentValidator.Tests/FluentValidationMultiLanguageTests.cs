using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class FluentValidationMultiLanguageTests
   {
      private enum PersonStatus
      {
         Active = 1,
         Inactive = 2
      }

      private class Person
      {
         public string Name { get; set; } = string.Empty;
         public int Age { get; set; }
         public DateTime BirthDate { get; set; }
         public PersonStatus Status { get; set; }
         public string Code { get; set; } = string.Empty;
      }

      [Fact]
      public void DefaultLanguage_ShouldReturnMessagesInEnglish()
      {
         var validator = CreateValidator(MessageLanguage.English);
         var person = CreateInvalidPerson();

         validator.Validate(person);

         var messages = validator.Messages.Select(message => message.Show()).ToArray();

         Assert.Equal(
         [
            "'Name' has fewer characters than expected (5).",
            "'Age' must be greater than 18.",
            "'BirthDate' must be today.",
            "'Status' has an invalid value for 'PersonStatus'.",
            "'Code' must be equal to 'EXPECTED'."
         ], messages);
      }

      [Fact]
      public void PortugueseBrazilLanguage_ShouldReturnMessagesInPortuguese()
      {
         var validator = CreateValidator(MessageLanguage.PortugueseBrazil);
         var person = CreateInvalidPerson();

         validator.Validate(person);

         var messages = validator.Messages.Select(message => message.Show()).ToArray();

         Assert.Equal(
         [
            "'Name' possui menos caracteres que o esperado (5).",
            "'Age' deve ser maior que 18.",
            "'BirthDate' deve ser hoje.",
            "'Status' possui um valor inválido para 'PersonStatus'.",
            "'Code' deve ser igual a 'EXPECTED'."
         ], messages);
      }

      private static FluentValidator<Person> CreateValidator(MessageLanguage language)
      {
         var validator = new FluentValidator<Person>(language)
               .RuleFor(person => person.Name).MinLength(5)
               .RuleFor(person => person.Age).IsGreaterThan(18)
               .RuleFor(person => person.BirthDate).IsToday()
               .RuleFor(person => person.Status).IsInEnum()
               .RuleFor(person => person.Code).IsEqualTo("EXPECTED");
         return validator;
      }

      private static Person CreateInvalidPerson()
         => new()
         {
            Name = "Ana",
            Age = 17,
            BirthDate = DateTime.Today.AddDays(-1),
            Status = (PersonStatus)99,
            Code = "ACTUAL"
         };
   }
}
