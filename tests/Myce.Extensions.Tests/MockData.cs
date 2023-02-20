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
      public int Age { get; set; }
   }

   public static class MockData
   {
      public static List<Person> GetListOfPeople()
      {
         return new List<Person> {
            new Person {Name = "Paul", Age = 41},
            new Person {Name = "Ringo", Age = 40},
            new Person {Name = "George ", Age = 37},
            new Person {Name = "John", Age = 37},
         };
      }

      public static List<Person> GetListOfPeopleWithDuplicateNames()
      {
         return new List<Person> {
            new Person {Name = "Paul", Age = 41},
            new Person {Name = "Paul", Age = 18},
            new Person {Name = "John", Age = 27},
         };
      }
   }

}
