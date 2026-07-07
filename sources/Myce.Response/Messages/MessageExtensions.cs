using System.Globalization;

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
         => messages.WithLanguage(
            Internationalization.CultureName.Get(language));

      public static IReadOnlyCollection<Message> WithLanguage(
         this IEnumerable<Message> messages,
         CultureInfo culture)
      {
         if (messages == null)
            throw new ArgumentNullException(nameof(messages));
         if (culture == null)
            throw new ArgumentNullException(nameof(culture));

         return messages
            .Select(message => message.WithLanguage(culture))
            .ToList()
            .AsReadOnly();
      }
   }
}
