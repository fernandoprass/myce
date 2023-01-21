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

      /// <summary> Check if the object is NULL </summary>
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
   }
}
