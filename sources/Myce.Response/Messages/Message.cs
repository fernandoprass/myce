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

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message..</param>
      public Message(MessageType type)
      {
         Type = type;
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text)
      {
         Type = type;
         Code = code;
         Text = text;
      }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type and text.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      public Message(MessageType type, string text) : this(type, string.Empty, text) { }

      /// <summary>
      /// Initializes a new instance of the Message class with the specified message type, code, text, and a collection
      /// of variables.
      /// </summary>
      /// <param name="type">The type of the message. Determines the category or severity of the message.</param>
      /// <param name="code">The code that uniquely identifies the message. Cannot be null.</param>
      /// <param name="text">The text content of the message. Cannot be null.</param>
      /// <param name="variables">A collection of variables to associate with the message. Each variable provides 
      /// additional context or data for the message. Cannot be null.</param>
      public Message(MessageType type, string code, string text, IEnumerable<Variable> variables) : this(type, code, text)
      {
         _variables.AddRange(variables);
      }

      /// <summary>
      /// Returns a string that represents the current object, including the code and text values.
      /// </summary>
      /// <returns>A string containing the code and text of the object in the format "Code: {Code}, Text: {Text}".</returns>
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
