using Xunit;

namespace Myce.FluentValidator.Tests
{
   public class RuleBuilderCollectionTests
   {
      private class Order
      {
         public List<string> Items { get; set; } = new();
         public string Status { get; set; }
         public int Priority { get; set; }
      }

      [Fact]
      public void HasItems_ShouldFail_WhenListIsEmpty()
      {
         var order = new Order { Items = new List<string>() };
         var validator = new FluentValidator<Order>()
             .RuleFor(x => x.Items).HasItems<Order, List<string>, string>();

         Assert.False(validator.Validate(order));
      }

      [Fact]
      public void MaxItems_ShouldFail_WhenExceedingLimit()
      {
         var order = new Order { Items = new List<string> { "Item1", "Item2", "Item3" } };
         var validator = new FluentValidator<Order>()
             .RuleFor(x => x.Items).MaxNumberOfItems<Order, List<string>, string>(2);

         Assert.False(validator.Validate(order));
      }

      [Theory]
      [InlineData("Pending", true)]
      [InlineData("Shipped", true)]
      [InlineData("Cancelled", false)]
      public void IsIn_ShouldValidateAgainstAllowedValues(string status, bool expected)
      {
         var order = new Order { Status = status };
         var allowedStatuses = new[] { "Pending", "Shipped" };

         var validator = new FluentValidator<Order>()
             .RuleFor(x => x.Status).IsIn(allowedStatuses);

         Assert.Equal(expected, validator.Validate(order));
      }

      [Theory]
      [InlineData("Active", true)]
      [InlineData("Draft", true)]
      [InlineData("Deleted", false)]
      public void IsIn_String_ShouldValidateCorrectly(string status, bool expected)
      {
         var order = new Order { Status = status };
         var allowed = new[] { "Active", "Draft" };

         var validator = new FluentValidator<Order>()
               .RuleFor(x => x.Status).IsIn(allowed);

         Assert.Equal(expected, validator.Validate(order));
      }

      [Theory]
      [InlineData(1, true)]
      [InlineData(5, false)]
      public void IsIn_Integer_ShouldValidateCorrectly(int priority, bool expected)
      {
         var order = new Order { Priority = priority };
         var allowedPriorities = new List<int> { 1, 2, 3 };

         var validator = new FluentValidator<Order>()
               .RuleFor(x => x.Priority).IsIn(allowedPriorities);

         Assert.Equal(expected, validator.Validate(order));
      }
   }
}