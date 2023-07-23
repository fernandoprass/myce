using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for Object type extensions
   /// </summary>
   public class ObjectExtensionTests
   {
      /// <summary> Check if the object is NULL </summary>
      [Fact]
      public void IsNull()
      {
         object obj = null;
         var result = ObjectExtensions.IsNull(obj);
         Assert.True(result);

         obj = new object();
         result = ObjectExtensions.IsNull(obj);
         Assert.False(result);
      }

      /// <summary> Check if the object is NOT NULL </summary>
      [Fact]
      public void IsNotNull()
      {
         object obj = null;
         var result = ObjectExtensions.IsNotNull(obj);
         Assert.False(result);

         obj = new object();
         result = ObjectExtensions.IsNotNull(obj);
         Assert.True(result); ;
      }

      /// <summary> Check if the value of the object is BETWEEN other two values </summary>
      [Fact]
      public void IsBetween()
      {
         var today = DateTime.Now;
         var yesterday = today.AddDays(-1);
         var tomorrow = today.AddDays(1);

         Assert.True(today.IsBetween(yesterday, tomorrow));

         Assert.True(0.IsBetween(-1, 1));
         Assert.True(10.5.IsBetween(10.49, 10.51));
         Assert.True(123d.IsBetween(122.9999d, 123.00001d));
         Assert.True(0.0003m.IsBetween(0.00029m, 0.0003m));

         Assert.True("b".IsBetween("a", "c"));
      }
   }
}
