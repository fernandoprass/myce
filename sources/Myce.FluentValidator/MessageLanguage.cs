using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;

namespace Myce.FluentValidator;

public sealed class MessageLanguage
{
   private readonly string _cultureCode;

   private static readonly ConcurrentDictionary<string, MessageLanguage> _cachedLanguages =
      new(StringComparer.OrdinalIgnoreCase);

   public static readonly MessageLanguage English = Register("en-US");
   public static readonly MessageLanguage PortugueseBrazil = Register("pt-BR");

   private MessageLanguage(string cultureCode)
   {
      _cultureCode = cultureCode;
   }

   /// <summary>
   /// Validates, normalizes, and registers a culture using its canonical CultureInfo name.
   /// Registering the same culture more than once returns the cached instance.
   /// </summary>
   public static MessageLanguage Register(string cultureCode)
   {
      if (string.IsNullOrWhiteSpace(cultureCode))
         throw new ArgumentException("Culture code cannot be null or empty.", nameof(cultureCode));

      string normalizedCultureCode = NormalizeCultureCode(cultureCode);

      return _cachedLanguages.GetOrAdd(
         normalizedCultureCode,
         code => new MessageLanguage(code));
   }

   /// <summary>
   /// Finds a registered language by culture code. Invalid or unregistered cultures return null.
   /// </summary>
   public static MessageLanguage? FromCultureCode(string cultureCode)
   {
      if (string.IsNullOrWhiteSpace(cultureCode)) return null;

      cultureCode = cultureCode.Trim();

      if (cultureCode.Equals("en", StringComparison.OrdinalIgnoreCase))
         cultureCode = "en-US";
      else if (cultureCode.Equals("pt", StringComparison.OrdinalIgnoreCase))
         cultureCode = "pt-BR";

      try
      {
         string normalizedCultureCode = NormalizeCultureCode(cultureCode);
         _cachedLanguages.TryGetValue(normalizedCultureCode, out var language);
         return language;
      }
      catch (CultureNotFoundException)
      {
         return null;
      }
   }

   private static string NormalizeCultureCode(string cultureCode)
   {
      string requestedCultureCode = cultureCode.Trim();

      var culture = CultureInfo
         .GetCultures(CultureTypes.AllCultures)
         .FirstOrDefault(candidate => candidate.Name.Equals(
            requestedCultureCode,
            StringComparison.OrdinalIgnoreCase));

      if (culture == null)
         throw new CultureNotFoundException(
            $"Culture '{cultureCode}' is not supported.");

      return CultureInfo.GetCultureInfo(culture.Name).Name;
   }

   public static implicit operator string(MessageLanguage language)
      => language?._cultureCode ?? string.Empty;

   public override string ToString() => _cultureCode;
}
