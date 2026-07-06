using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;

namespace Myce.FluentValidator;

public sealed class MessageLanguage
{
   public CultureInfo Culture { get; }

   private static readonly ConcurrentDictionary<string, MessageLanguage> _cachedLanguages =
      new(StringComparer.OrdinalIgnoreCase);

   public static readonly MessageLanguage English = Register("en-US");
   public static readonly MessageLanguage PortugueseBrazil = Register("pt-BR");

   private MessageLanguage(CultureInfo culture)
   {
      Culture = culture;
   }

   /// <summary>
   /// Validates, normalizes, and registers a culture using its canonical CultureInfo name.
   /// Registering the same culture more than once returns the cached instance.
   /// </summary>
   public static MessageLanguage Register(string cultureCode)
   {
      if (string.IsNullOrWhiteSpace(cultureCode))
         throw new ArgumentException("Culture code cannot be null or empty.", nameof(cultureCode));

      return Register(GetCulture(cultureCode));
   }

   public static MessageLanguage Register(CultureInfo culture)
   {
      if (culture == null)
         throw new ArgumentNullException(nameof(culture));

      CultureInfo normalizedCulture = GetCulture(culture.Name);
      return _cachedLanguages.GetOrAdd(
         normalizedCulture.Name,
         _ => new MessageLanguage(normalizedCulture));
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
         return FromCulture(GetCulture(cultureCode));
      }
      catch (CultureNotFoundException)
      {
         return null;
      }
   }

   public static MessageLanguage? FromCulture(CultureInfo culture)
   {
      if (culture == null)
         return null;

      try
      {
         CultureInfo normalizedCulture = GetCulture(culture.Name);
         _cachedLanguages.TryGetValue(normalizedCulture.Name, out var language);
         return language;
      }
      catch (CultureNotFoundException)
      {
         return null;
      }
   }

   private static CultureInfo GetCulture(string cultureCode)
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

      return CultureInfo.GetCultureInfo(culture.Name);
   }

   public static implicit operator string(MessageLanguage language)
      => language?.Culture.Name ?? string.Empty;

   public override string ToString() => Culture.Name;
}
