using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myce.Extensions.Tests
{
   public class Person
   {
      public string Name { get; set; }
   }

   public static class MockData
   {
      public static List<Person> GetListOfPeople()
      {
         return new List<Person> {
            new Person {Name = "Paul"},
            new Person {Name = "Ringo"},
            new Person {Name = "George "},
            new Person {Name = "John",},
         };
      }

      public static List<Person> GetListOfPeopleWithDuplicateNames()
      {
         return new List<Person> {
            new Person {Name = "Paul"},
            new Person {Name = "Paul",},
            new Person {Name = "John"},
         };
      }
   }

}
