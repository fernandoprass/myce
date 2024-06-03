using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for IEnumerable type class extensions
   /// </summary>
   public class EnumerableExtensionTests
   {
      #region ContainsDuplicates
      /// <summary> Receive an empty array and return False </summary>
      [Fact]
      public void ContainsDuplicates_ReceiveEmptyArray_ReturnFalse()
      {
         int[] array = { };
         var result = array.ContainsDuplicates();
         Assert.False(result);
      }

      /// <summary> Receive a array of integer without duplicates elements and return False </summary>
      [Fact]
      public void ContainsDuplicates_ReceiveArrayOfIntegersWithoutDuplicates_ReturnFalse()
      {
         int[] array = { 1, 2, 3 };

         var a = array.ToList();

         var result = array.ContainsDuplicates();
         Assert.False(result);
      }

      /// <summary> Receive a null list and return False </summary>
      [Fact]
      public void ContainsDuplicates_ReceiveNullList_ReturnFalse()
      {
         List<double> list = null;
         var result = list.ContainsDuplicates();
         Assert.False(result);
      }

      /// <summary> Receive a list of strings with duplicates elements and return the duplicates </summary>
      [Fact]
      public void ContainsDuplicates_ReceiveListOfStringsWithDuplicates_ReturnTheDuplicates()
      {
         var list = new List<string> { "a", "b", "a", "AB", "AB", "B", "a" };
         var result = list.GetDuplicates();
         Assert.NotEmpty(result);
         Assert.Equal(2, result.Count());
      }

      /// <summary> Receive a list of strings with duplicates elements and return True </summary>
      [Fact]
      public void ContainsDuplicates_ReceiveListOfStringsWithDuplicates_ReturnTrue()
      {
         var list = new List<string> { "a", "b", "a" };
         var result = list.ContainsDuplicates();
         Assert.True(result);
      }

      #endregion

      /// <summary> Receive a list and a positive chunk size and split it</summary>
      [Fact]
      public void Chunk()
      {
         var list = new List<int> { 1, 2, 3, 4, 5 };
         var result = list.Chunk(2);
        
         // should return { { 1, 2 }, { 3, 4}, { 5 } }

         Assert.NotNull(result);
         Assert.Equal(3, result.Count());
         Assert.Equal(5, result.Last().First()); 
      }

      /// <summary> Receive a list of Person with duplicates names and return a list with no duplicates name </summary>
      [Fact]
      public void DistinctBy()
      {
         var list = MockData.GetListOfPeopleWithDuplicateNames();

         var result = list.DistinctRowsBy(x => x.Name);
         Assert.Equal(2, result.Count());
         Assert.Equal("Paul", result.First().Name);
         Assert.Equal("John", result.Last().Name);
      }

      /// <summary> Loops through an array of integers and returns the total </summary>
      [Fact]
      public void ForEach()
      {
         int[] array = { 1, 2, 3 };
         int i = 0;

         array.ForEach(x => i += x);

         Assert.Equal(6, i);
      }

      /// <summary> Receive a array of integer without duplicates elements return an empty array </summary>
      [Fact]
      public void GetDuplicates_ReceiveArrayOfDoubblesWithoutDuplicates_ReturnFalse()
      {
         double[] array = { 1.6, 2.1, 3, 5, 16, 21 };
         var result = array.GetDuplicates();
         Assert.Empty(result);
      }

      /// <summary> Receive an empty array and return an empty array </summary>
      [Fact]
      public void GetDuplicates_ReceiveEmptyArray_ReturnEmptyArray()
      {
         int[] array = { };
         var result = array.GetDuplicates();
         Assert.Empty(result);
      }

      /// <summary> Validate if the enumerable is Null or Empty </summary>
      [Theory]
      [InlineData(new int[] {1}, false)]
      [InlineData(new int[] {1, 2}, false)]
      [InlineData(new int[] {}, true)]
      [InlineData(null, true)]

      public void IsNullOrEmpty(IEnumerable<int> enumerable, bool expected)
      {
         var result = enumerable.IsNullOrEmpty();
         Assert.Equal(expected, result);
      }

      #region HasData
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
      public void HasData_ReceiveListWithValues_ReturnTrue()
      {
         var list = new List<int> { 1 };
         var result = list.HasData();
         Assert.True(result); ;
      }
      #endregion
   }
}
