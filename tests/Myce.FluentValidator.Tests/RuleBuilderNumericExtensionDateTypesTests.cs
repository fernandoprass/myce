using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderNumericExtensionDateTypesTests
   {
      private class EventRequest
      {
         public DateTime StartDate { get; set; }
         public DateTime? EndDate { get; set; }
      }

      [Fact]
      public void IsBetween_ShouldValidateDateRange()
      {
         var minDate = new DateTime(2024, 01, 01);
         var maxDate = new DateTime(2024, 12, 31);
         var request = new EventRequest { StartDate = new DateTime(2024, 06, 15) };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.StartDate).IsBetween(minDate, maxDate);

         var isValid = validator.Validate(request);

         Assert.True(isValid);
      }

      [Fact]
      public void IsGreaterThan_ShouldFail_WhenDateIsOlder()
      {
         var today = DateTime.Today;
         var yesterday = today.AddDays(-1);
         var request = new EventRequest { StartDate = yesterday };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.StartDate).IsGreaterThan(today);

         var isValid = validator.Validate(request);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
      }

      [Fact]
      public void IsLessThanOrEqualTo_ShouldWorkWithNullableDates()
      {
         var deadline = new DateTime(2025, 01, 01);
         var request = new EventRequest { EndDate = new DateTime(2025, 02, 01) };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.EndDate).IsLessThanOrEqualTo(deadline);

         var isValid = validator.Validate(request);

         Assert.False(isValid);
      }

      [Fact]
      public void DateValidation_ShouldPass_WhenNullableDateIsNull()
      {
         var request = new EventRequest { EndDate = null };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.EndDate).IsNull();

         var isValid = validator.Validate(request);

         Assert.True(isValid);
      }
   }
}
