using System.Text;

namespace Myce.Response.Messages
{
   public abstract class Message
   {
      private readonly List<Variable> _variables = new List<Variable>();
      public MessageType Type { get; protected set; }
      public string Code { get; set; }
      public string Text { get; set; }
      public IReadOnlyCollection<Variable> Variables => _variables.AsReadOnly();

      public Message(MessageType type)
      {
         Type = type;
      }

      public Message(MessageType type, string code, string text)
      {
         Type = type;
         Code = code;
         Text = text;
      }

      public Message(MessageType type, string text) : this(type, string.Empty, text) { }

      public Message(MessageType type, string code, string text, IEnumerable<Variable> variables) : this(type, code, text)
      {
         _variables.AddRange(variables);
      }

      public override string ToString()
      {
         return $"{nameof(Code)}: {Code}, {nameof(Text)}: {Text}";
      }

      /// <summary>
      /// Add new variable to the massage
      /// </summary>
      /// <param name="name">The variable name</param>
      /// <param name="value">The variable value</param>
      public void AddVariable(string name, string value)
      {
         var variable = new Variable { Name = name, Value = value };
         _variables.Add(variable);
      }


      /// <summary>
      /// Show the Text value. If any variable is used, the parse is done
      /// There are two ways to use variables
      /// 2. using {}
      /// 3. using []
      /// </summary>
      public string Show()
      {
         if (string.IsNullOrWhiteSpace(Text))
            return string.Empty;

         if (_variables == null || !_variables.Any())
            return Text;
         
         var builder = new StringBuilder(Text);

         foreach (var variable in _variables)
         {
            if (string.IsNullOrEmpty(variable.Name)) continue;

            string valueToReplace = variable.Value ?? string.Empty;

            builder.Replace("{" + variable.Name + "}", valueToReplace);
            builder.Replace("[" + variable.Name + "]", valueToReplace);
         }

         return builder.ToString();
      }
   }
}
