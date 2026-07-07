using System.Collections.Concurrent;
using System.Globalization;

namespace Myce.Response.Messages.Internationalization;

internal static class CultureName
{
   private static readonly ConcurrentDictionary<string, CultureInfo> _cache =
      new(StringComparer.OrdinalIgnoreCase);

   internal static CultureInfo Get(string? language)
   {
      if (string.IsNullOrWhiteSpace(language))
         throw new ArgumentException(
            "Language cannot be null or empty.",
            nameof(language));

      string requestedLanguage = language!.Trim();

      return _cache.GetOrAdd(requestedLanguage, requested =>
      {
         var culture = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .FirstOrDefault(candidate => candidate.Name.Equals(
               requested,
               StringComparison.OrdinalIgnoreCase));

         if (culture == null)
            throw new CultureNotFoundException(
               $"Culture '{language}' is not supported.");

         return CultureInfo.GetCultureInfo(culture.Name);
      });
   }

   internal static CultureInfo Get(CultureInfo? culture)
   {
      if (culture == null)
         throw new ArgumentNullException(nameof(culture));

      return Get(culture.Name);
   }
}
