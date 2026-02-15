using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class StringExtensionValidationTests
   {
      private class Person
      {
         public string Code { get; set; }
         public string Email { get; set; }
         public string DateOfBirth { get; set; }
      }

      /// <summary> Verify Contains Only Numbers validator </summary>
      [Theory]
      [InlineData("", 0)]
      [InlineData(null, 0)]
      [InlineData("0123", 0)]
      [InlineData(" 123", 1)]
      [InlineData("a123", 1)]
      [InlineData("1.23", 1)]
      [InlineData("1,23", 1)]
      [InlineData("abc", 1)]
      public void ContainsOnlyNumber(string value, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();

         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
            .ContainsOnlyNumber(errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void ContainsOnlyNumber_WithDefaultMessage()
      {
         var person = new Person { Code = "ab12" };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
               .ContainsOnlyNumber();

         var result = validator.Validate(person);

         Assert.Single(validator.Messages);
         Assert.IsType<ErrorShouldContainOnlyNumber>(validator.Messages.First());
      }

      /// <summary> Verify ExactNumberOfCharacters and ExactNumberOfCharactersIf validators </summary>
      [Theory]
      [InlineData("", 1, 1 == 1, 0)]
      [InlineData(null, 1, 1 == 1, 0)]
      [InlineData("hello", 4, 1 == 1, 2)]
      [InlineData("hello", 4, 1 == 2, 1)]
      [InlineData("hello", 5, 1 == 1, 0)]
      [InlineData("hello world", 10, 1 == 1, 2)]
      [InlineData("hello world", 10, 1 == 2, 1)]
      [InlineData("hello world", 11, 1 == 1, 0)]
      public void ExactNumberOfCharacters_and_ExactNumberOfCharactersIf(string value, int length, bool expression, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
            .ExactNumberOfCharacters(length, errorMessage)
            .ExactNumberOfCharactersIf(length, expression, errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      /// <summary> Verify MaxLength and MaxLengthIf validators </summary>
      [Theory]
      [InlineData("", 1, 1 == 1, 0)]
      [InlineData(null, 1, 1 == 1, 0)]
      [InlineData("hello", 4, 1 == 1, 2)]
      [InlineData("hello", 4, 1 == 2, 1)]
      [InlineData("hey", 5, 1 == 1, 0)]
      [InlineData("hello", 5, 1 == 1, 0)]
      public void MaxLength_and_MaxLengthIf(string value, int length, bool expression, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
            .MaxLength(length, errorMessage)
            .MaxLengthIf(length, expression, errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void MaxLength_and_MaxLengthIf_WithDefaultMessage()
      {
         var person1 = new Person { Code = "abcd" };

         var validator1 = new FluentValidator<Person>();

         validator1.RuleFor(x => x.Code).MaxLength(3);

         validator1.Validate(person1);
         Assert.Single(validator1.Messages);
         Assert.IsType<ErrorMoreCharactersThanExpected>(validator1.Messages.First());

         var person2 = new Person { Code = "abc" };

         var validator2 = new FluentValidator<Person>();

         validator2.RuleFor(x => x.Code).MaxLengthIf(2, true);

         validator2.Validate(person2);
         Assert.Single(validator2.Messages);
         Assert.IsType<ErrorMoreCharactersThanExpected>(validator2.Messages.First());
      }

      /// <summary> Verify MinLength and MinLengthIf validators </summary>
      [Theory]
      [InlineData("", 1, 1 == 1, 2)]
      [InlineData(null, 1, 1 == 1, 2)] // Wait, MinLength for null/empty? Usually passes unless Required?
                                       // My Implementation: value != null && value.Length >= minLength.
                                       // If null/empty -> returns FALSE (invalid).
                                       // So error expected. Correct.
      [InlineData("hello", 6, 1 == 1, 2)]
      [InlineData("hello", 6, 1 == 2, 1)]
      [InlineData("hey", 4, 1 == 2, 1)] // MinLength 4. "hey" len 3. Expected fails MinLength. 
                                        // But 1==2 -> False. MinLengthIf(false) -> PASS.
                                        // MinLength(4) -> FAILS (1 error).
                                        // Total errors: 1. Correct.
      [InlineData("hello", 5, 1 == 1, 0)]
      public void MinLength_and_MinLengthIf(string value, int length, bool expression, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>()
            .RuleFor(x => x.Code)
            .MinLength(length, errorMessage)
            .MinLengthIf(length, expression, errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void MinLength_and_MinLengthIf_WithDefaultMessage()
      {
         var person1 = new Person { Code = "abcd" };
         var validator1 = new FluentValidator<Person>()
            .RuleFor(x => x.Code)
            .MinLength(5);

         validator1.Validate(person1);
         Assert.Single(validator1.Messages);
         Assert.IsType<ErrorFewerCharactersThanExpected>(validator1.Messages.First());

         var person2 = new Person { Code = "abc" };
         var validator2 = new FluentValidator<Person>()
            .RuleFor(x => x.Code)
            .MinLengthIf(4, true);

         validator2.Validate(person2);
         Assert.Single(validator2.Messages);
         Assert.IsType<ErrorFewerCharactersThanExpected>(validator2.Messages.First());
      }

      /// <summary> Verify IsDate validator </summary>
      [Theory]
      [InlineData("", 1)]
      [InlineData(null, 1)]
      [InlineData("7.18", 1)]
      [InlineData("date", 1)]
      [InlineData("01012022", 1)]
      [InlineData("01/01/2022", 0)]
      [InlineData("2022-1-1", 0)]
      [InlineData("31/12/2022", 0)]
      [InlineData("2022-12-31", 0)]
      [InlineData("29/02/2020", 0)]
      [InlineData("2020-02-29", 0)]
      [InlineData("29/02/2022", 1)]
      [InlineData("2022-02-29", 1)]
      public void IsValidDate(string value, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { DateOfBirth = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.DateOfBirth).IsValidDate(errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void IsValidDate_WithDefaultMessage()
      {
         var person = new Person { DateOfBirth = "31/04/2022" };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.DateOfBirth).IsValidDate();

         var result = validator.Validate(person);

         Assert.Single(validator.Messages);
         Assert.IsType<ErrorInvalidDate>(validator.Messages.First());
      }

      /// <summary> Verify IsValidEmailAddress validator </summary>
      [Theory]
      [InlineData("", 1)]
      [InlineData(null, 1)]
      [InlineData("hello", 1)] 
      [InlineData("@world.com", 1)]
      [InlineData("hello@world", 1)] 
      [InlineData("hello@world.c", 1)]
      [InlineData("hello@world.co", 0)]
      [InlineData("hello@w.co", 0)]
      public void IsValidEmailAddress(string value, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Email = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Email)
            .IsValidEmailAddress(errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void IsValidEmail_WithDefaultMessage()
      {
         var person = new Person { Email = "a@b" };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Email)
            .IsValidEmailAddress();

         var result = validator.Validate(person);

         Assert.Single(validator.Messages);
         Assert.IsType<ErrorInvalidEmail>(validator.Messages.First());
      }

      private static ErrorMessage GetGenericErrorMessage()
      {
         return new ErrorMessage { Code = "001", Text = "message" };
      }
   }
}
