using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for Guid type class extensions
   /// </summary>
   public class GuidExtensionTests
   {
      /// <summary> Received a null value and return TRUE </summary>
      [Fact]
      public void IsEmpty_ReceivedNull_ReturnTrue()
      {
         var result = GuidExtension.IsEmpty(null);
         Assert.True(result);
      }

      /// <summary> Received a guid and return FALSE </summary>
      [Fact]
      public void IsEmpty_ReceivedGuid_ReturnFalse() {
         var result = GuidExtension.IsEmpty(Guid.NewGuid());
         Assert.False(result);
      }

      /// <summary> Received an empty guid and return TRUE </summary>
      [Fact]
      public void IsEmpty_ReceivedEmptyGuid_ReturnTrue() {
         var result = GuidExtension.IsEmpty(Guid.Empty);
         Assert.True(result);
      }

      /// <summary> Received a null value and return FALSE </summary>
      [Fact]
      public void IsNotEmpty_ReceivedNull_ReturnFalse() {
         var result = GuidExtension.IsNotEmpty(null);
         Assert.False(result);
      }

      /// <summary> Received an empty guid and return FALSE </summary>
      [Fact]
      public void IsNotEmpty_ReceivedEmptyGuid_ReturnFalse() {
         var result = GuidExtension.IsNotEmpty(Guid.Empty);
         Assert.False(result);
      }

      /// <summary> Received a guid and return TRUE </summary>
      [Fact]
      public void IsNotEmpty_ReceivedGuid_ReturnTrue() {
         var result = GuidExtension.IsNotEmpty(Guid.NewGuid());
         Assert.True(result);
      }

   }
}
