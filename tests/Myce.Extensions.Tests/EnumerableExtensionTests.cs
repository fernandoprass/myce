using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for IEnumerable type class extensions
   /// </summary>
   public class EnumerableExtensionTests
   {
      /// <summary> Receive an uninstantiated list and return False </summary>
      [Fact]
      public void HasData_ReceiveUninstantiatedList_ReturnFalse()
      {
         List<int> list = null;
         var result = list.HasData();
         Assert.False(result);
      }

      /// <summary> Receive an empty list and return False </summary>
      [Fact]
      public void HasData_ReceiveEmptyList_ReturnFalse()
      {
         var list = new List<int>();
         var result = list.HasData();
         Assert.False(result);
      }

      /// <summary> Receive a list with elements and return True </summary>
      [Fact]
      public void HasData_ReceiveListWithValues_RetornTrue()
      {
         var list = new List<int> { 1 };
         var result = list.HasData();
         Assert.True(result); ;
      }
   }
}
