namespace Myce.Response.Messages
{
   public static class MessageExtensions
   {
      /// <summary>
      /// Creates independent copies of the messages translated to the requested language.
      /// The source messages are not modified.
      /// </summary>
      public static IReadOnlyCollection<Message> WithLanguage(
         this IEnumerable<Message> messages,
         string language)
      {
         return messages == null
            ? throw new ArgumentNullException(nameof(messages))
            : (IReadOnlyCollection<Message>)messages
            .Select(message => message.WithLanguage(language))
            .ToList()
            .AsReadOnly();
      }
   }
}
