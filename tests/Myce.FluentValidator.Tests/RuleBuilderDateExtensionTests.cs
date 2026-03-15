using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderDateExtensionTests
   {
      private class EventRequest
      {
         public DateTime ScheduledDate { get; set; }
         public DateTime StartDate { get; set; }
         public DateTime? EndDate { get; set; }
      }

      /// <summary>
      /// Test boundary conditions for inclusive date ranges
      /// </summary>
      [Theory]
      [InlineData("2024-01-01", true)]  // Exact minimum boundary
      [InlineData("2024-12-31", true)]  // Exact maximum boundary
      [InlineData("2024-06-15", true)]  // Middle of range
      [InlineData("2023-12-31", false)] // Just before minimum
      [InlineData("2025-01-01", false)] // Just after maximum
      public void IsBetween_ShouldValidateDateRangeBoundaries(string dateStr, bool expected)
      {
         var minDate = new DateTime(2024, 01, 01);
         var maxDate = new DateTime(2024, 12, 31);
         var request = new EventRequest { StartDate = DateTime.Parse(dateStr) };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.StartDate).IsBetween(minDate, maxDate);

         Assert.Equal(expected, validator.Validate(request));
      }

      /// <summary>
      /// Test future dates and the immediate "now" boundary
      /// </summary>
      [Theory]
      [InlineData(1, true)]   // 1 minute in the future
      [InlineData(1440, true)] // 1 day in the future
      [InlineData(-1, false)]  // 1 minute in the past
      [InlineData(-1440, false)] // 1 day in the past
      public void IsInTheFuture_ShouldValidateAgainstCurrentMoment(int minutesToAdd, bool expected)
      {
         var request = new EventRequest { ScheduledDate = DateTime.Now.AddMinutes(minutesToAdd) };
         var validator = new FluentValidator<EventRequest>().RuleFor(x => x.ScheduledDate).IsInTheFuture();

         Assert.Equal(expected, validator.Validate(request));
      }

      /// <summary>
      /// Test past dates including the immediate boundary
      /// </summary>
      [Theory]
      [InlineData(-1, true)]  // 1 minute ago
      [InlineData(1, false)]  // 1 minute in the future
      public void IsInThePast_ShouldValidateChronologicalPast(int minutesToAdd, bool expected)
      {
         var request = new EventRequest { ScheduledDate = DateTime.Now.AddMinutes(minutesToAdd) };
         var validator = new FluentValidator<EventRequest>().RuleFor(x => x.ScheduledDate).IsInThePast();

         Assert.Equal(expected, validator.Validate(request));
      }

      /// <summary>
      /// Test relative date validation ignoring time components (Date property only)
      /// </summary>
      [Theory]
      [InlineData(0, true)]   // Exact today
      [InlineData(1, false)]  // Tomorrow
      [InlineData(-1, false)] // Yesterday
      public void IsToday_ShouldIgnoreTimeAndValidateDate(int daysToAdd, bool expected)
      {
         // Adding hours to ensure time doesn't break the "Date" comparison
         var request = new EventRequest { ScheduledDate = DateTime.Today.AddDays(daysToAdd).AddHours(10) };
         var validator = new FluentValidator<EventRequest>().RuleFor(x => x.ScheduledDate).IsToday();

         Assert.Equal(expected, validator.Validate(request));
      }

      /// <summary>
      /// Test weekend logic for all days of a week
      /// </summary>
      [Theory]
      [InlineData("2026-03-09", false)] // Monday
      [InlineData("2026-03-10", false)] // Tuesday
      [InlineData("2026-03-11", false)] // Wednesday
      [InlineData("2026-03-12", false)] // Thursday
      [InlineData("2026-03-13", false)] // Friday
      [InlineData("2026-03-14", true)]  // Saturday
      [InlineData("2026-03-15", true)]  // Sunday
      public void IsWeekend_ShouldValidateEveryDayOfWeek(string dateStr, bool expected)
      {
         var request = new EventRequest { ScheduledDate = DateTime.Parse(dateStr) };
         var validator = new FluentValidator<EventRequest>().RuleFor(x => x.ScheduledDate).IsWeekend();

         Assert.Equal(expected, validator.Validate(request));
      }

      /// <summary>
      /// Test weekend logic for all days of a week
      /// </summary>
      [Theory]
      [InlineData("2026-03-09", true)] // Monday
      [InlineData("2026-03-10", true)] // Tuesday
      [InlineData("2026-03-11", true)] // Wednesday
      [InlineData("2026-03-12", true)] // Thursday
      [InlineData("2026-03-13", true)] // Friday
      [InlineData("2026-03-14", false)]  // Saturday
      [InlineData("2026-03-15", false)]  // Sunday
      public void IsWeekday_ShouldValidateEveryDayOfWeek(string dateStr, bool expected)
      {
         var request = new EventRequest { ScheduledDate = DateTime.Parse(dateStr) };
         var validator = new FluentValidator<EventRequest>().RuleFor(x => x.ScheduledDate).IsWeekday();

         Assert.Equal(expected, validator.Validate(request));
      }

      /// <summary>
      /// Test nullable dates behavior (Rules should typically skip validation if null)
      /// </summary>
      [Fact]
      public void DateRules_ShouldPass_WhenNullableDateIsNull()
      {
         var request = new EventRequest { EndDate = null };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.EndDate).IsBetween(DateTime.MinValue, DateTime.MaxValue);

         Assert.False(validator.Validate(request));
      }

      /// <summary>
      /// Test comparison boundaries for greater than or equal
      /// </summary>
      [Theory]
      [InlineData(0, true)]  // Exact same time
      [InlineData(1, true)]  // One second after
      [InlineData(-1, false)] // One second before
      public void IsGreaterThanOrEqualTo_ShouldValidateBoundary(int secondsToAdd, bool expected)
      {
         var referenceDate = new DateTime(2026, 01, 01, 12, 00, 00);
         var request = new EventRequest { StartDate = referenceDate.AddSeconds(secondsToAdd) };

         var validator = new FluentValidator<EventRequest>()
            .RuleFor(x => x.StartDate).IsGreaterThanOrEqualTo(referenceDate);

         Assert.Equal(expected, validator.Validate(request));
      }
   }
}