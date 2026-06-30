using System;
using System.Collections.Generic;

namespace Myce.Response.Messages
{
   public sealed class MessageLanguage
   {
      private readonly string _cultureCode;

      // Internal cache to look up instances by their string culture code
      private static readonly Dictionary<string, MessageLanguage> _cachedLanguages = new(StringComparer.OrdinalIgnoreCase);

      // Native languages supported out-of-the-box
      public static readonly MessageLanguage English = Register("en-US");
      public static readonly MessageLanguage PortugueseBrazil = Register("pt-BR");

      /// <summary>
      /// Private constructor to control instance creation.
      /// </summary>
      private MessageLanguage(string cultureCode)
      {
         _cultureCode = cultureCode;
      }

      /// <summary>
      /// Allows users to dynamically register additional languages without modifying the library.
      /// </summary>
      /// <param name="cultureCode">The culture code string (e.g., "es-ES", "fr-FR").</param>
      public static MessageLanguage Register(string cultureCode)
      {
         if (string.IsNullOrWhiteSpace(cultureCode))
            throw new ArgumentException("Culture code cannot be null or empty.", nameof(cultureCode));

         // If already registered, return the existing instance to maintain singleton-per-culture behavior
         if (_cachedLanguages.TryGetValue(cultureCode, out var existing))
         {
            return existing;
         }

         var newLanguage = new MessageLanguage(cultureCode);
         _cachedLanguages[cultureCode] = newLanguage;
         return newLanguage;
      }

      /// <summary>
      /// Finds and returns the matching MessageLanguage instance for the given culture code string.
      /// </summary>
      /// <param name="cultureCode">The culture code to search for (e.g., "en-US", "pt-br").</param>
      /// <returns>The matching MessageLanguage instance, or null if it hasn't been registered.</returns>
      public static MessageLanguage? FromCultureCode(string cultureCode)
      {
         if (string.IsNullOrWhiteSpace(cultureCode)) return null;

         // Fallback If user pass only "en", it is converted to "en-US"
         if (cultureCode.Length == 2)
         {
            if (cultureCode.Equals("en", StringComparison.OrdinalIgnoreCase)) cultureCode = "en-US";
            else if (cultureCode.Equals("pt", StringComparison.OrdinalIgnoreCase)) cultureCode = "pt-BR";
         }

         _cachedLanguages.TryGetValue(cultureCode, out var language);
         return language;
      }

      // Implicit conversion to string allows passing MessageLanguage directly into .Show()
      public static implicit operator string(MessageLanguage language) => language?._cultureCode ?? string.Empty;

      public override string ToString() => _cultureCode;
   }
}