using Myce.FluentValidator.ErrorMessages;
using Myce.Response.Messages;
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
      public void All_EveryItemMatchesCondition_ReturnsTrue()
      {
         // All items start with "SKU-"
         var order = new Order { Items = new List<string> { "SKU-001", "SKU-002", "SKU-099" } };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items)
            .All<Order, List<string>, string>(
               item => item.StartsWith("SKU-"),
               new ErrorMessage("All items must be valid SKUs")
            );

         var isValid = validator.Validate(order);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Fact]
      public void All_OneItemDoesNotMatchCondition_ReturnsFalseAndAddsMessage()
      {
         // One item is invalid (doesn't start with "SKU-")
         var order = new Order { Items = new List<string> { "SKU-001", "INVALID", "SKU-002" } };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items)
            .All<Order, List<string>, string>(
               item => item.StartsWith("SKU-"),
               new ErrorMessage("Invalid item found in the list")
            );

         var isValid = validator.Validate(order);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
         Assert.Equal("Invalid item found in the list", validator.Messages.First().Text);
      }

      [Fact]
      public void All_NullCollection_ReturnsTrueByDefault()
      {
         var order = new Order { Items = null! };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items)
            .All<Order, List<string>, string>(
               item => item.Length > 0,
               new ErrorMessage("Should not be hit")
            );

         var isValid = validator.Validate(order);

         // By convention, All returns true for empty or null collections 
         // unless combined with HasItems()
         Assert.True(isValid);
      }

      [Fact]
      public void Any_AtLeastOneItemMatchesCondition_ReturnsTrue()
      {
         // All items start with "SKU-"
         var order = new Order { Items = new List<string> { "ABC-001", "SKU-002", "XYZ-099" } };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items)
            .Any<Order, List<string>, string>(
               item => item.StartsWith("SKU-"),
               new ErrorMessage("At least one item must be a valid SKU")
            );

         var isValid = validator.Validate(order);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Fact]
      public void Any_NullCollection_ReturnsFalse()
      {
         var order = new Order { Items = null! };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items)
            .Any<Order, List<string>, string>(
               item => item.Length > 0,
               new ErrorMessage("Should fail")
            );

         var isValid = validator.Validate(order);

         Assert.False(isValid);
      }

      [Fact]
      public void Count_CollectionWithLessThanLimitItems_ReturnsFalse()
      {
         var order = new Order { Items = ["A", "B", "C"] };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items).Count(count => count == 4);

         var isValid = validator.Validate(order);

         Assert.False(isValid);
         Assert.IsType<InvalidNumberOfItemsError>(validator.Messages.First());
      }

      [Fact]
      public void HasItems_ShouldFail_WhenListIsEmpty()
      {
         var order = new Order { Items = new List<string>() };
         var validator = new FluentValidator<Order>()
             .RuleFor(x => x.Items).HasItems();

         Assert.False(validator.Validate(order));
      }

      [Fact]
      public void HasNoDuplicates_CollectionWithDuplicateItems_ReturnsFalse()
      {
         var order = new Order { Items = new List<string> { "A", "B", "A" } };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items).HasNoDuplicates(new ErrorMessage("Duplicates found"));

         var isValid = validator.Validate(order);

         Assert.False(isValid);
         Assert.Single(validator.Messages);
         Assert.Equal("Duplicates found", validator.Messages.First().Text);
      }

      [Fact]
      public void HasNoDuplicates_CollectionWithUniqueItems_ReturnsTrue()
      {
         var order = new Order { Items = ["A", "B", "C"] };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items).HasNoDuplicates();

         var isValid = validator.Validate(order);

         Assert.True(isValid);
         Assert.Empty(validator.Messages);
      }

      [Fact]
      public void HasNoDuplicates_NullCollection_ReturnsTrue()
      {
         var order = new Order { Items = null! };
         var validator = new FluentValidator<Order>();

         validator.RuleFor(x => x.Items).HasNoDuplicates();

         var isValid = validator.Validate(order);

         Assert.True(isValid); // Null by convention has no duplicates
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

      [Fact]
      public void Validate_MultipleRulesWithStop_ShouldHaltOnlyAffectedChain()
      {
         // Scenario: 
         // 1. Items is empty (should fail HasItems and STOP, ignoring HasNoDuplicates)
         // 2. Status is empty (should fail IsRequired)
         var order = new Order
         {
            Items = ["A", "B", "C"],    // Duplicate
            Status = ""                 // Empty string
         };

         var validator = new FluentValidator<Order>();

         // Chain 1: Collection validation with Stop
         validator.RuleFor(x => x.Items)
            .Count(count => count > 3, new ErrorMessage("Invalid number of items")).Stop()
            .HasNoDuplicates(new ErrorMessage("Items must be unique"));

         // Chain 2: String validation
         validator.RuleFor(x => x.Status)
            .IsRequired(new ErrorMessage("Status is required"));

         var isValid = validator.Validate(order);

         Assert.False(isValid);

         // Expect exactly 2 messages:
         // - "Order must have more than three items" (from Chain 1)
         // - "Status is required" (from Chain 2)
         // "Items must be unique" should NOT be present because of .Stop()
         Assert.Equal(2, validator.Messages.Count);

         Assert.Contains(validator.Messages, m => m.Text == "Invalid number of items");
         Assert.Contains(validator.Messages, m => m.Text == "Status is required");

         // Verify that the rule after .Stop() was indeed skipped
         Assert.DoesNotContain(validator.Messages, m => m.Text == "Items must be unique");
      }
   }
}