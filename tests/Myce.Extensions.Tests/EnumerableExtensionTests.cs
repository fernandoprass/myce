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

      /// <summary> Receive a list of Person with duplicates names and return a list with no duplicates name </summary>
      [Fact]
      public void DistinctBy_ReceiveListPeopleWithDuplicatesNames_ReturnTrue()
      {
         var list = new List<Person> { 
            new Person {Name = "Paul", Age = 41},
            new Person {Name = "Paul", Age = 18},
            new Person {Name = "John", Age = 27},

         };
         var result = list.DistinctBy(x => x.Name);
         Assert.Equal(2, result.Count());
         Assert.Equal("Paul", result.First().Name);
         Assert.Equal("John", result.Last().Name);
      }
   }

   public class Person
   {
      public string Name { get; set; }
      public int Age { get; set; }
   }
}
