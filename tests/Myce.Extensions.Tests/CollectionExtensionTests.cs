using Xunit;

namespace Myce.Extensions.Tests
{
   /// <summary>
   /// Tests for IEnumerable type class extensions
   /// </summary>
   public class CollectionExtensionTests
   {
      /// <summary> Receive a null element and not add to list </summary>
      [Fact]
      public void AddIfNotNull_ReceiveNullItem_ShouldNotAdd()
      {
         var people = MockData.GetListOfPeople();

         var numberOfElements = people.Count();

         var lastPerson = people.Last();

         Person person = null;

         people.AddIfNotNull(person);

         Assert.Equal(numberOfElements, people.Count());
         Assert.Equal(lastPerson.Name, people.Last().Name);
      }

      /// <summary> Receive a new element and add to list </summary>
      [Fact]
      public void AddIfNotNull_ReceiveAnItem_ShouldAdd()
      {
         var people = MockData.GetListOfPeople();

         var numberOfElements = people.Count();

         var person = new Person { Name = "Billy Preston" };

         people.AddIfNotNull(person);

         Assert.Equal(++numberOfElements, people.Count());
         Assert.Equal(person.Name, people.Last().Name);
      }
   }
}
