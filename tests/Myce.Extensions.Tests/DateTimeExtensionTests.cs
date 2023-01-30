using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for DateTime type class extensions
   /// </summary>
   public class DateTimeExtensionTests
   {
      /// <summary>  Received a date and return the first day of the month </summary>
      [Fact]
      public void FirstDayOfMonth()
      {
         var result = DateTimeExtension.FirstDayOfMonth(DateTime.Today);
         Assert.Equal(DateTime.Today.Year, result.Year);
         Assert.Equal(DateTime.Today.Month, result.Month);
         Assert.Equal(1, result.Day);
      }

      /// <summary>
      /// Validate LastDayOfMonth for Leap and No Leap Years
      /// </summary>
      /// <param name="year"></param>
      /// <param name="month"></param>
      /// <param name="day"></param>
      /// <param name="expectedDay"></param>
      [Theory]
      [InlineData(2020, 01, 01, 31)]
      [InlineData(2021, 04, 30, 30)]
      [InlineData(2022, 07, 12, 31)]
      [InlineData(2023, 09, 28, 30)]
      [InlineData(2024, 12, 31, 31)]
      public void LastDayOfMonth_GeneralValidations(int year, int month, int day, int expectedDay)
      {
         var result = DateTimeExtension.LastDayOfMonth(new DateTime(year, month, day));
         Assert.Equal(year, result.Year);
         Assert.Equal(month, result.Month);
         Assert.Equal(expectedDay, result.Day);
      }

      /// <summary>
      /// Validate LastDayOfMonth for Leap and No Leap Years
      /// </summary>
      /// <param name="year"></param>
      /// <param name="month"></param>
      /// <param name="day"></param>
      /// <param name="expectedDay"></param>
      [Theory]
      [InlineData(2020, 02, 01, 29)]
      [InlineData(2021, 02, 01, 28)]
      [InlineData(2022, 02, 12, 28)]
      [InlineData(2023, 02, 28, 28)]
      [InlineData(2024, 02, 28, 29)]
      public void LastDayOfMonth_LeapYear(int year, int month, int day, int expectedDay)
      {
         var result = DateTimeExtension.LastDayOfMonth(new DateTime(year, month, day));
         Assert.Equal(year, result.Year);
         Assert.Equal(month, result.Month);
         Assert.Equal(expectedDay, result.Day);
      }

      /// <summary>
      /// Validate if a given date is weekend
      /// </summary>
      [Theory]
      [InlineData(2018, 03, 29, false)]
      [InlineData(2018, 06, 07, false)]
      [InlineData(2019, 08, 25, true)]
      [InlineData(2022, 12, 03, true)]
      [InlineData(2023, 01, 29, true)]
      public void IsWeekend(int year, int month, int day, bool expectedResult)
      {
         var date = new DateTime(year, month, day);
         Assert.Equal(expectedResult, date.IsWeekend());
      }

      /// <summary>
      /// Validate if a given date is workday
      /// </summary>
      [Theory]
      [InlineData(2022, 12, 25, false)]
      [InlineData(2023, 01, 01, false)]
      [InlineData(2022, 09, 20, true)]
      [InlineData(2023, 03, 17, true)]
      [InlineData(2024, 07, 04, true)]
      public void IsWorkday(int year, int month, int day, bool expectedResult)
      {
         var date = new DateTime(year, month, day);
         Assert.Equal(expectedResult, date.IsWorkDay());
      }

      /// <summary>
      /// Validate the next workday
      /// </summary>
      [Theory]
      [InlineData(2022, 12, 25, "2022-12-23")]
      [InlineData(2023, 01, 01, "2022-12-30")]
      [InlineData(2022, 09, 20, "2022-09-19")]
      [InlineData(2023, 03, 17, "2023-03-16")]
      [InlineData(2023, 07, 04, "2023-07-03")]
      public void PriorWorkday(int year, int month, int day, string expectedResult)
      {
         var date = new DateTime(year, month, day);
         var piorWorkday = date.PriorWorkday();
         var expectedDay = DateTime.Parse(expectedResult);
         Assert.Equal(expectedDay, piorWorkday);
      }

      /// <summary>
      /// Validate the next workday
      /// </summary>
      [Theory]
      [InlineData(2022, 12, 25, "2022-12-26")]
      [InlineData(2023, 01, 01, "2023-01-02")]
      [InlineData(2022, 09, 20, "2022-09-21")]
      [InlineData(2023, 03, 17, "2023-03-20")]
      [InlineData(2023, 07, 04, "2023-07-05")]
      public void NextWorkday(int year, int month, int day, string expectedResult)
      {
         var date = new DateTime(year, month, day);
         var nextWorkday = date.NextWorkday();
         var expectedDay = DateTime.Parse(expectedResult);
         Assert.Equal(expectedDay, nextWorkday);
      }

      /// <summary>
      /// Validate Yesterday
      /// </summary>
      [Fact]
      public void Yesterday()
      {
         var today = DateTime.Today;
         Assert.Equal(DateTime.Today.AddDays(-1), today.Yesterday());
      }

      /// <summary>
      /// Validate Tomorrow
      /// </summary>
      [Fact]
      public void Tomorrow()
      {
         var today = DateTime.Today;
         Assert.Equal(DateTime.Today.AddDays(1), today.Tomorrow());
      }
   }
}
