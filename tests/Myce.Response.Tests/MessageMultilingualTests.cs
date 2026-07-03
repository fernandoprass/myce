using Xunit;

namespace Myce.Response.Messages.Tests;

/// <summary>
/// Concrete test double to instantiate the abstract Message class.
/// </summary>
public class TestMessage : Message
{
   public TestMessage(MessageType type, string code, Dictionary<string, string> localizedTemplates)
      : base(type, code, localizedTemplates) { }

   public TestMessage(MessageType type)
      : base(type) { }
}

public class MessageMultilingualTests
{
   [Theory]
   // Testing template placeholder syntax {var} and [var] for exact language matches
   [InlineData("en-US", "The field Birth Date must be today.")]
   [InlineData("pt-BR", "O campo Data de Nascimento deve ser a data de hoje.")]
   public void Show_WithExactLanguageMatch_ShouldResolveCorrectTemplateAndVariables(string language, string expectedOutput)
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "The field {fieldName} must be today." },
         { "pt-BR", "O campo {fieldName} deve ser a data de hoje." }
      };

      var variableTranslations = new Dictionary<string, string>
      {
         { "en-US", "Birth Date" },
         { "pt-BR", "Data de Nascimento" }
      };

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariableTranslation("fieldName", variableTranslations);

      var result = message.Show(language);

      Assert.Equal(expectedOutput, result);
   }

   [Theory]
   // Testing fallbacks when an unsupported language is requested
   [InlineData("fr-FR", "The field Birth Date must be today.")] // Should fall back to en-US (DefaultLanguageKey)
   [InlineData("es-ES", "The field Birth Date must be today.")] // Should fall back to en-US (DefaultLanguageKey)
   public void Show_WithMissingLanguage_ShouldFallbackToDefaultLanguage(string missingLanguage, string expectedOutput)
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "The field {fieldName} must be today." },
         { "pt-BR", "O campo {fieldName} deve ser a data de hoje." }
      };

      var variableTranslations = new Dictionary<string, string>
      {
         { "en-US", "Birth Date" },
         { "pt-BR", "Data de Nascimento" }
      };

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariableTranslation("fieldName", variableTranslations);

      var result = message.Show(missingLanguage);

      Assert.Equal(expectedOutput, result);
   }

   [Fact]
   public void Show_WithMissingLanguageAndNoDefault_ShouldFallbackToFirstAvailableLanguage()
   {
      // Providing a dictionary without 'en-US' to test total fallback behavior
      var templates = new Dictionary<string, string>
      {
         { "pt-BR", "O campo {fieldName} deve ser a data de hoje." }
      };

      var variableTranslations = new Dictionary<string, string>
      {
         { "pt-BR", "Data de Nascimento" }
      };

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariableTranslation("fieldName", variableTranslations);

      var result = message.Show("fr-FR"); // French requested, but only pt-BR exists

      Assert.Equal("O campo Data de Nascimento deve ser a data de hoje.", result);
   }

   [Fact]
   public void Show_WithoutParameters_ShouldResolveToDefaultLanguage()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "The field {fieldName} must be today." },
         { "pt-BR", "O campo {fieldName} deve ser a data de hoje." }
      };

      var variableTranslations = new Dictionary<string, string>
      {
         { "en-US", "Birth Date" },
         { "pt-BR", "Data de Nascimento" }
      };

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariableTranslation("fieldName", variableTranslations);

      var result = message.Show(); // Executing standard legacy parameterless method

      Assert.Equal("The field Birth Date must be today.", result);
   }

   [Fact]
   public void AddVariableMultilingual_ShouldPopulateLegacyVariablesCollectionForBackwardCompatibility()
   {
      var templates = new Dictionary<string, string> { { "en-US", "Hello {name}" } };
      var variableTranslations = new Dictionary<string, string>
      {
         { "en-US", "John Doe" },
         { "pt-BR", "Fulano" }
      };

      var message = new TestMessage(MessageType.Information, "GREETING", templates);

      message.AddVariableTranslation("name", variableTranslations);

      Assert.NotEmpty(message.Variables);
      Assert.Contains(message.Variables, v => v.Name == "name" && (v.Value == "John Doe" || v.Value == "Fulano"));
   }

   [Fact]
   public void Show_WithMixingFixedAndMultilingualVariables_ShouldParseBothCorrectly()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "User {user} set {fieldName} to {value}." },
         { "pt-BR", "Usuário {user} alterou {fieldName} para {value}." }
      };

      var variableTranslations = new Dictionary<string, string>
      {
         { "en-US", "Birth Date" },
         { "pt-BR", "Data de Nascimento" }
      };

      var message = new TestMessage(MessageType.Information, "AUDIT_LOG", templates);

      // 1. Mixing legacy fixed value variables
      message.AddVariable("user", "Admin");
      message.AddVariable("value", "2026-01-01");

      // 2. Adding the localizable field variable
      message.AddVariableTranslation("fieldName", variableTranslations);

      string resultEn = message.Show("en-US");
      string resultPt = message.Show("pt-BR");

      Assert.Equal("User Admin set Birth Date to 2026-01-01.", resultEn);
      Assert.Equal("Usuário Admin alterou Data de Nascimento para 2026-01-01.", resultPt);
   }

   [Fact]
   public void AddTextTranslation_ShouldAddAndResolveIndividualTemplates()
   {
      var message = new TestMessage(MessageType.Error);
      message.Code = "INCREMENTAL_TEST";

      // Act - Adding translations individually
      message.AddTextTranslation("en-US", "Static text in English.");
      message.AddTextTranslation("pt-BR", "Texto estático em Português.");

      // Assert
      Assert.Equal("Static text in English.", message.Show("en-US"));
      Assert.Equal("Texto estático em Português.", message.Show("pt-BR"));
   }

   [Fact]
   public void AddVariableTranslation_ShouldAddAndResolveIndividualVariableTranslations()
   {
      var message = new TestMessage(MessageType.Error);
      message.Code = "VAR_TEST";
      message.AddTextTranslation("en-US", "The field {fieldName} is invalid.");
      message.AddTextTranslation("pt-BR", "O campo {fieldName} é inválido.");

      message.AddVariableTranslation("fieldName", "en-US", "Email");
      message.AddVariableTranslation("fieldName", "pt-BR", "E-mail");

      Assert.Equal("The field Email is invalid.", message.Show("en-US"));
      Assert.Equal("O campo E-mail é inválido.", message.Show("pt-BR"));
   }

   [Fact]
   public void AddVariableTranslation_FirstTranslation_ShouldPopulateLegacyVariablesCollection()
   {
      var message = new TestMessage(MessageType.Information);

      message.AddVariableTranslation("status", "pt-BR", "Ativo");
      message.AddVariableTranslation("status", "en-US", "Active"); // Second translation should not duplicate legacy collection

      Assert.Single(message.Variables); // Ensures only one item was added to the fallback list
      Assert.Contains(message.Variables, v => v.Name == "status" && v.Value == "Ativo");
   }

   [Fact]
   public void TranslateTo_WhenInformExistingLanguage_ShouldTranslateCorrectly()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "User {user} set {fieldName} to {value}." },
         { "pt-BR", "Usuário {user} alterou {fieldName} para {value}." }
      };

      var variableTranslations = new Dictionary<string, string>
      {
         { "en-US", "Birth Date" },
         { "pt-BR", "Data de Nascimento" }
      };

      var message = new TestMessage(MessageType.Information, "AUDIT_LOG", templates);

      // 1. Mixing legacy fixed value variables
      message.AddVariable("user", "Admin");
      message.AddVariable("value", "2026-01-01");

      // 2. Adding the localizable field variable
      message.AddVariableTranslation("fieldName", variableTranslations);

      //before translation message should be in default language (en-US)
      Assert.Equal("User {user} set {fieldName} to {value}.", message.Text);

      message.TranslateTo("pt-BR");

      Assert.Equal("Usuário {user} alterou {fieldName} para {value}.", message.Text);
   }
}