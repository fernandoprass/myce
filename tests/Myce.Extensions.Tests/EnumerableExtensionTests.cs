using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for IEnumerable type class extensions
   /// </summary>
   public class EnumerableExtensionTests
   {
      /// <summary> Receive a list and a positive chunk size, should split </summary>
      [Fact]
      public void Chunk_ReceiveAListAndAPositiveChunkSize_ShouldSplit()
      {
         var list = new List<int> { 1, 2, 3, 4, 5 };
         var result = list.Chunk(2);
        
         // should return { { 1, 2 }, { 3, 4}, { 5 } }

         Assert.NotNull(result);
         Assert.Equal(3, result.Count());
         Assert.Equal(5, result.Last().First()); 
      }

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
         int[] array =  { 1, 2, 3 };
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

      /// <summary> Receive a list of strings with duplicates elements and return True </summary>
      [Fact]
      public void ContainsDuplicates_ReceiveListOfStringsWithDuplicates_ReturnTrue()
      {
         var list = new List<string> { "a", "b", "a" };
         var result = list.ContainsDuplicates();
         Assert.True(result);
      }

      /// <summary> Receive an empty array and return an empty array </summary>
      [Fact]
      public void GetDuplicates_ReceiveEmptyArray_ReturnEmptyArray()
      {
         int[] array = { };
         var result = array.GetDuplicates();
         Assert.Empty(result);
      }

      /// <summary> Receive a array of integer without duplicates elements return an empty array </summary>
      [Fact]
      public void GetDuplicates_ReceiveArrayOfDoubblesWithoutDuplicates_ReturnFalse()
      {
         double[] array = { 1.6, 2.1, 3, 5, 16, 21 };
         var result = array.GetDuplicates();
         Assert.Empty(result);
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

      /// <summary> Receive a list of Person with duplicates names and return a list with no duplicates name </summary>
      [Fact]
      public void DistinctBy_ReceiveListPeopleWithDuplicatesNames_ReturnTrue()
      {
         var list = MockData.GetListOfPeopleWithDuplicateNames();

         var result = list.DistinctBy(x => x.Name);
         Assert.Equal(2, result.Count());
         Assert.Equal("Paul", result.First().Name);
         Assert.Equal("John", result.Last().Name);
      }
   }
}
