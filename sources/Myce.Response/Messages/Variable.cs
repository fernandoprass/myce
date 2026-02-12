namespace Myce.Response.Messages
{
   public class Variable {
      public string Name { get; set; } = string.Empty;
      public string Value { get; set; } = string.Empty;

      public Variable() { }

      public Variable(string name, string value) {
         Name = name;
         Value = value;
      }

      public override string ToString() {
         return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
      }
   }
}
