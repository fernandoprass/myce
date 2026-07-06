using Xunit;

namespace Myce.FluentValidator.Tests;

public class MessageLanguageTests
{
   [Fact]
   public void NativeLanguages_ShouldBeCorrectlyInitialized()
   {
      string englishCode = MessageLanguage.English;
      string portugueseCode = MessageLanguage.PortugueseBrazil;

      Assert.Equal("en-US", englishCode);
      Assert.Equal("pt-BR", portugueseCode);
   }

   [Theory]
   [InlineData("en-US")]
   [InlineData("EN-US")]
   [InlineData("en-us")]
   public void FromCultureCode_WithValidEnglishStrings_ShouldReturnNativeEnglishInstance(string cultureCode)
   {
      var result = MessageLanguage.FromCultureCode(cultureCode);

      Assert.NotNull(result);
      Assert.Same(MessageLanguage.English, result);
   }

   [Theory]
   [InlineData("pt-BR")]
   [InlineData("PT-BR")]
   [InlineData("pt-br")]
   public void FromCultureCode_WithValidPortugueseStrings_ShouldReturnNativePortugueseInstance(string cultureCode)
   {
      var result = MessageLanguage.FromCultureCode(cultureCode);

      Assert.NotNull(result);
      Assert.Same(MessageLanguage.PortugueseBrazil, result);
   }

   [Theory]
   [InlineData(null)]
   [InlineData("")]
   [InlineData("   ")]
   [InlineData("de-DE")] // Not registered
   [InlineData("not_a_valid_culture!")]
   public void FromCultureCode_WithInvalidOrUnregisteredStrings_ShouldReturnNull(string? cultureCode)
   {
      var result = MessageLanguage.FromCultureCode(cultureCode!);

      Assert.Null(result);
   }

   [Fact]
   public void Register_WithNewCulture_ShouldCreateAndCacheInstance()
   {
      string frenchCode = "fr-FR";

      var registeredFrench = MessageLanguage.Register(frenchCode);
      var retrievedFrench = MessageLanguage.FromCultureCode("FR-fr"); // Testing case-insensitivity on retrieval

      Assert.NotNull(registeredFrench);
      Assert.NotNull(retrievedFrench);
      Assert.Same(registeredFrench, retrievedFrench);
      Assert.Equal("fr-FR", (string)registeredFrench);
   }

   [Fact]
   public void Register_WithExistingCulture_ShouldReturnSameInstanceWithoutDuplicating()
   {
      string cultureCode = "es-ES";

      var firstRegistration = MessageLanguage.Register(cultureCode);
      var secondRegistration = MessageLanguage.Register("ES-es"); // Registering again with different casing

      Assert.Same(firstRegistration, secondRegistration);
   }

   [Fact]
   public void Register_ShouldNormalizeCultureCodeUsingCultureInfo()
   {
      var language = MessageLanguage.Register("  IT-it  ");

      Assert.Equal("it-IT", language.ToString());
      Assert.Same(language, MessageLanguage.FromCultureCode("it-it"));
   }

   [Fact]
   public void Register_WhenCalledConcurrently_ShouldReturnOneSharedInstance()
   {
      var languages =
         new System.Collections.Concurrent.ConcurrentBag<MessageLanguage>();

      Parallel.For(
         0,
         100,
         _ => languages.Add(MessageLanguage.Register("nl-NL")));

      var first = languages.First();
      Assert.All(languages, language => Assert.Same(first, language));
   }

   [Theory]
   [InlineData(null)]
   [InlineData("")]
   [InlineData("   ")]
   public void Register_WithNullOrEmptyCulture_ShouldThrowArgumentException(string? invalidCode)
   {
      Assert.Throws<ArgumentException>(() => MessageLanguage.Register(invalidCode!));
   }

   [Fact]
   public void Register_WithUnsupportedCulture_ShouldThrowCultureNotFoundException()
   {
      Assert.Throws<System.Globalization.CultureNotFoundException>(
         () => MessageLanguage.Register("not_a_valid_culture!"));
   }

   [Fact]
   public void ImplicitStringConversion_ShouldAllowDirectStringOperations()
   {
      MessageLanguage language = MessageLanguage.English;

      string result = language; // Implicit conversion triggered here

      Assert.Equal("en-US", result);
   }

   [Fact]
   public void ToString_ShouldReturnCultureCodeString()
   {
      MessageLanguage language = MessageLanguage.PortugueseBrazil;

      string result = language.ToString();

      Assert.Equal("pt-BR", result);
   }

   [Theory]
   [InlineData("en")]
   [InlineData("EN")]
   public void FromCultureCode_WithTwoLetterEnglish_ShouldFallbackToNativeEnglishInstance(string shortCultureCode)
   {
      var result = MessageLanguage.FromCultureCode(shortCultureCode);

      Assert.NotNull(result);
      Assert.Same(MessageLanguage.English, result);
   }

   [Theory]
   [InlineData("pt")]
   [InlineData("PT")]
   public void FromCultureCode_WithTwoLetterPortuguese_ShouldFallbackToNativePortugueseInstance(string shortCultureCode)
   {
      var result = MessageLanguage.FromCultureCode(shortCultureCode);

      Assert.NotNull(result);
      Assert.Same(MessageLanguage.PortugueseBrazil, result);
   }
}
