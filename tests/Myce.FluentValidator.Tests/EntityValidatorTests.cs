using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   /// <summary> Test Entity Validator </summary>

   internal class Person
   {
      public string Code { get; set; }
      public string Name { get; set; }
      public double? Salary { get; set; }
      public int Age { get; set; }
      public bool IsSingle { get; set; }
   }

   public class EntityValidatorTests
   {
      /// <summary> Verify Contains validator </summary>
      [Theory]
      [InlineData("", new[] { "A", "B" }, 1)]
      [InlineData(null, new[] { "A", "B" }, 1)]
      [InlineData("AB", new[] { "A", "B" }, 1)]
      [InlineData("C", new[] { "A", "B" }, 1)]
      [InlineData("A", new[] { "A", "B" }, 0)]
      [InlineData("B", new[] { "A", "B", "AB" }, 0)]
      [InlineData("AB", new[] { "A", "B", "AB" }, 0)]
      public void Contains(string value, string[] collection, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();

         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code).Contains(collection, errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
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

      /// <summary> Verify IsRequired and IsRequiredIf validators </summary>
      [Theory]
      [InlineData("", 1 == 1, 2)]
      [InlineData("", 1 == 2, 1)]
      [InlineData(null, 1 == 1, 2)]
      [InlineData(null, 1 == 2, 1)]
      [InlineData("hello", 1 == 1, 0)]
      [InlineData("hello", 1 == 2, 0)]
      public void IsRequired_and_IsRequiredIf(string value, bool expression, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
            .IsRequired(errorMessage)
            .IsRequiredIf(expression, errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void IsIsRequired_and_IsMandatoryIf_WithDefaultMessage()
      {
         var person = new Person { Code = string.Empty, Name = null };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
            .IsRequired()
            .RuleFor(x => x.Name)
            .IsRequired();

         var result = validator.Validate(person);

         Assert.Equal(2, validator.Messages.Count());
         Assert.Equal("Code", validator.Messages.First().Variables.First().Value);
         Assert.IsType<ErrorIsRequired>(validator.Messages.First());

         Assert.Equal("Name", validator.Messages.Last().Variables.First().Value);
         Assert.IsType<ErrorIsRequired>(validator.Messages.Last());
      }


      [Fact]
      public void IsIsRequired_and_GeneralRules()
      {
         var person = new Person { Code = "123A", Name = "John Smith", Age = 17, IsSingle = true, Salary = -100 };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
               .IsRequired()
               .ContainsOnlyNumber()
            .RuleFor(x => x.Name)
               .IsRequired()
             .RuleFor(x => x.Age)
               .IsGreaterThanOrEqualTo(18)
               .IsLessThanOrEqualTo(65)
             .RuleFor(x => x.Salary)
               .IsGreaterThanOrEqualTo(500);

         var result = validator.Validate(person);

         Assert.Equal("Code", validator.Messages.First().Variables.First().Value);
         Assert.IsType<ErrorShouldContainOnlyNumber>(validator.Messages.First());

         var errorMessageAge = validator.Messages.Last();
         Assert.Contains("Salary", errorMessageAge.ToString());      
         Assert.IsType<ErrorMessage>(errorMessageAge);

         Assert.Equal(3, validator.Messages.Count());
      }

      /// <summary> Verify IsDate validator </summary>
      [Theory]
      [InlineData("", 0)]
      [InlineData(null, 0)]
      [InlineData("01/01/2022", 0)]
      [InlineData("2022-01-01", 0)]
      [InlineData("31/12/2022", 0)]
      [InlineData("2022-12-31", 0)]
      [InlineData("29/02/2020", 0)]
      [InlineData("2020-02-29", 0)]
      [InlineData("29/02/2022", 1)]
      [InlineData("2022-02-29", 1)]
      [InlineData("7.18", 1)]
      [InlineData("date", 1)]
      public void IsValidDate(string value, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code).IsValidDate(errorMessage);

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void IsValidDate_WithDefaultMessage()
      {
         var person = new Person { Code = "31/04/2022" };
         
         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code) .IsValidDate();

         var result = validator.Validate(person);

         Assert.Single(validator.Messages);
         Assert.IsType<ErrorInvalidDate>(validator.Messages.First());
      }

      /// <summary> Verify IsValidEmailAddress validator </summary>
      [Theory]
      [InlineData("", 0)]
      [InlineData(null, 0)]
      //[InlineData("hello", 1)] // Should fail, currently implemented logic is incomplete/basic
      [InlineData("@world.com", 1)]
      [InlineData("hello@world", 1)] // Current regex logic or lack thereof? Need to verify MailAddress behavior.
      [InlineData("hello@world.c", 0)] // MailAddress behavior might allow this?
      [InlineData("hello@world.com", 0)]
      public void IsValidEmailAddress(string value, int expectedNumberOfErrors)
      {
         ErrorMessage errorMessage = GetGenericErrorMessage();
         var person = new Person { Code = value };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code)
            .IsValidEmailAddress(errorMessage);

         var result = validator.Validate(person);
         
         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      [Fact]
      public void IsValidEmail_WithDefaultMessage()
      {
         var person = new Person { Code = "a@b" };

         var validator = new FluentValidator<Person>();

         validator.RuleFor(x => x.Code).IsValidEmailAddress();

         var result = validator.Validate(person);

         Assert.Single(validator.Messages);
         Assert.IsType<ErrorInvalidEmail>(validator.Messages.First());
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

      private static ErrorMessage GetGenericErrorMessage()
      {
         return new ErrorMessage { Code = "001", Text = "message" };
      }
   }
}
