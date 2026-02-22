using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleForTests
   {
      private class Person
      {
         public string Code { get; set; }
         public string Name { get; set; }
         public double? Salary { get; set; }
         public int Age { get; set; }
         public bool IsSingle { get; set; }
      }

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

         validator
            .RuleFor(x => x.Code)
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
               .IsEqualTo("John Smith")
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

      private static ErrorMessage GetGenericErrorMessage()
      {
         return new ErrorMessage { Code = "001", Text = "message" };
      }
   }
}
