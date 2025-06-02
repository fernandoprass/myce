using Myce.Validation.ErrorMessages;
using Xunit;
using ErrorMessage = Myce.Response.Messages.ErrorMessage;

namespace Myce.Validation.Tests
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

         var person = new Person { Code = value};

         var validator = new EntityValidator<Person>()
            .RuleFor(x => x.Code)
            .Contains(collection, errorMessage)
            .Apply();

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

         var validator = new EntityValidator<Person>()
            .RuleFor(x => x.Code)
            .ContainsOnlyNumber(errorMessage)
            .Apply();

         var result = validator.Validate(person);

         Assert.Equal(expectedNumberOfErrors, validator.Messages.Count());
      }

      ///// <summary> Verify ExactNumberOfCharacteres and ExactNumberOfCharacteresIf validators </summary>
      //[Theory]
      //[InlineData("", 1, 1 == 1, 0)]
      //[InlineData(null, 1, 1 == 1, 0)]
      //[InlineData("hello", 4, 1 == 1, 2)]
      //[InlineData("hello", 4, 1 == 2, 1)]
      //[InlineData("hello", 5, 1 == 1, 0)]
      //[InlineData("hello world", 10, 1 == 1, 2)]
      //[InlineData("hello world", 10, 1 == 2, 1)]
      //[InlineData("hello world", 11, 1 == 1, 0)]
      //public void ExactNumberOfCharacteres_and_ExactNumberOfCharacteresIf(string value, int lenght, bool expression, int expectedNumberOfErrors)
      //{
      //   ErrorMessage errorMessage = GetGenericErrorMessage();

      //   var validator = new EntityValidator()
      //      .ExactNumberOfCharacteres(value, lenght, errorMessage)
      //      .ExactNumberOfCharacteresIf(value, lenght, expression, errorMessage);

      //   var result = validator.Validate();

      //   Assert.Equal(expectedNumberOfErrors, result.Messages.Count());
      //}

      //[Fact]
      //public void ContainsOnlyNumber_WithDefaultMessage()
      //{
      //   var validator = new EntityValidator().ContainsOnlyNumber("ab12");

      //   var result = validator.Validate();

      //   Assert.Single(result.Messages);
      //   Assert.IsType<ErrorShouldContainOnlyNumber>(result.Messages.First());
      //}

      ///// <summary> Verify IsMandatory and IsMandatoryIf validators </summary>
      //[Theory]
      //[InlineData("", 1 == 1, 2)]
      //[InlineData("", 1 == 2, 1)]
      //[InlineData(null, 1 == 1, 2)]
      //[InlineData(null, 1 == 2, 1)]
      //[InlineData("hello", 1 == 1, 0)]
      //[InlineData("hello", 1 == 2, 0)]
      //public void IsMandatory_and_IsMandatoryIf(string value, bool expression, int expectedNumberOfErrors)
      //{
      //   ErrorMessage errorMessage = GetGenericErrorMessage();

      //   var validator = new EntityValidator()
      //      .IsMandatory(value, errorMessage)
      //      .IsMandatoryIf(value, expression, errorMessage);

      //   var result = validator.Validate();

      //   Assert.Equal(expectedNumberOfErrors, result.Messages.Count());
      //}

      [Fact]
      public void IsIsRequired_and_IsMandatoryIf_WithDefaultMessage()
      {
         var person = new Person { Code = string.Empty, Name = null };

         var validator = new EntityValidator<Person>()
         .RuleFor(x => x.Code)
            .IsRequired()
            .Apply()
         .RuleFor(x => x.Name)
            .IsRequired()
            .Apply();

         var result = validator.Validate(person);

         Assert.Equal(2, validator.Messages.Count());
         Assert.Equal("Code", validator.Messages.First().Variables.First().Value);
         Assert.IsType<ErrorIsRequired>(validator.Messages.First());

         Assert.Equal("Name", validator.Messages.Last().Variables.First().Value);
         Assert.IsType<ErrorIsRequired>(validator.Messages.Last());
      }

      ///// <summary> Verify IsDate validator </summary>
      //[Theory]
      //[InlineData("", 0)]
      //[InlineData(null, 0)]
      //[InlineData("01/01/2022", 0)]
      //[InlineData("2022-01-01", 0)]
      //[InlineData("31/12/2022", 0)]
      //[InlineData("2022-12-31", 0)]
      //[InlineData("29/02/2020", 0)]
      //[InlineData("2020-02-29", 0)]
      //[InlineData("29/02/2022", 1)]
      //[InlineData("2022-02-29", 1)]
      //[InlineData("7.18", 1)]
      //[InlineData("date", 1)]
      //public void IsValidDate(string value, int expectedNumberOfErrors)
      //{
      //   ErrorMessage errorMessage = GetGenericErrorMessage();

      //   var validator = new EntityValidator()
      //      .IsValidDate(value, errorMessage);

      //   var result = validator.Validate();

      //   Assert.Equal(expectedNumberOfErrors, result.Messages.Count());
      //}

      //[Fact]
      //public void IsValidDate_WithDefaultMessage()
      //{
      //   var validator = new EntityValidator()
      //      .IsValidDate("31/04/2022");

      //   var result = validator.Validate();

      //   Assert.Single(result.Messages);
      //   Assert.IsType<ErrorInvalidDate>(result.Messages.First());
      //}

      ///// <summary> Verify IsValidEmailAddress validator </summary>
      //[Theory]
      //[InlineData("", 0)]
      //[InlineData(null, 0)]
      //[InlineData("hello", 1)]
      //[InlineData("@world.com", 1)]
      //[InlineData("hello@world", 1)]
      //[InlineData("hello@world.c", 1)]
      //[InlineData("hello@world.com", 0)]
      //public void IsValidEmailAddress(string value, int expectedNumberOfErrors)
      //{
      //   ErrorMessage errorMessage = GetGenericErrorMessage();

      //   var validator = new EntityValidator()
      //      .IsValidEmailAddress(value, errorMessage);

      //   var result = validator.Validate();

      //   Assert.Equal(expectedNumberOfErrors, result.Messages.Count());
      //}

      //[Fact]
      //public void IsValidEmail_WithDefaultMessage()
      //{
      //   var validator = new EntityValidator()
      //      .IsValidEmailAddress("a@b");

      //   var result = validator.Validate();

      //   Assert.Single(result.Messages);
      //   Assert.IsType<ErrorInvalidEmail>(result.Messages.First());
      //}

      ///// <summary> Verify MaxLenght and MaxLenghtIf validators </summary>
      //[Theory]
      //[InlineData("", 1, 1 == 1, 0)]
      //[InlineData(null, 1, 1 == 1, 0)]
      //[InlineData("hello", 4, 1 == 1, 2)]
      //[InlineData("hello", 4, 1 == 2, 1)]
      //[InlineData("hey", 5, 1 == 1, 0)]
      //[InlineData("hello", 5, 1 == 1, 0)]
      //public void MaxLenght_and_MaxLenghtIf(string value, int lenght, bool expression, int expectedNumberOfErrors)
      //{
      //   ErrorMessage errorMessage = GetGenericErrorMessage();

      //   var validator = new EntityValidator()
      //      .MaxLenght(value, lenght, errorMessage)
      //      .MaxLenghtIf(value, lenght, expression, errorMessage);

      //   var result = validator.Validate();

      //   Assert.Equal(expectedNumberOfErrors, result.Messages.Count());
      //}

      //[Fact]
      //public void MaxLenght_and_MaxLenghtIf_WithDefaultMessage()
      //{
      //   var validator = new EntityValidator()
      //      .MaxLenght("abcd", 3)
      //      .MaxLenghtIf("abc", 2, true);

      //   var result = validator.Validate();

      //   Assert.Equal(2, result.Messages.Count());
      //   Assert.Equal("abcd", result.Messages.First().Variables.First().Value);
      //   Assert.IsType<ErrorMoreCharactersThanExpected>(result.Messages.First());

      //   Assert.Equal("abc", result.Messages.Last().Variables.First().Value);
      //   Assert.IsType<ErrorMoreCharactersThanExpected>(result.Messages.Last());
      //}

      ///// <summary> Verify MinLenght and MinLenghtIf validators </summary>
      //[Theory]
      //[InlineData("", 1, 1 == 1, 2)]
      //[InlineData(null, 1, 1 == 1, 2)]
      //[InlineData("hello", 6, 1 == 1, 2)]
      //[InlineData("hello", 6, 1 == 2, 1)]
      //[InlineData("hey", 4, 1 == 2, 1)]
      //[InlineData("hello", 5, 1 == 1, 0)]
      //public void MinLenght_and_MinLenghtIf(string value, int lenght, bool expression, int expectedNumberOfErrors)
      //{
      //   ErrorMessage errorMessage = GetGenericErrorMessage();

      //   var validator = new EntityValidator()
      //      .MinLenght(value, lenght, errorMessage)
      //      .MinLenghtIf(value, lenght, expression, errorMessage);

      //   var result = validator.Validate();

      //   Assert.Equal(expectedNumberOfErrors, result.Messages.Count());
      //}

      //[Fact]
      //public void MinLenght_and_MinLenghtIf_WithDefaultMessage()
      //{
      //   var validator = new EntityValidator()
      //      .MinLenght("abcd", 5)
      //      .MinLenghtIf("abc", 4, true);

      //   var result = validator.Validate();

      //   Assert.Equal(2, result.Messages.Count());
      //   Assert.Equal("abcd", result.Messages.First().Variables.First().Value);
      //   Assert.IsType<ErrorFewerCharactersThanExpected>(result.Messages.First());

      //   Assert.Equal("abc", result.Messages.Last().Variables.First().Value);
      //   Assert.IsType<ErrorFewerCharactersThanExpected>(result.Messages.Last());
      //}

      private static ErrorMessage GetGenericErrorMessage()
      {
         return new ErrorMessage { Code = "001", Text = "message" };
      }
   }
}
