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
   [Fact]
   public void Variables_ShouldExposeOnlyValuesForCurrentLanguage()
   {
      var templates = new Dictionary<string, string> { { "en-US", "Hello {name}" } };

      var message = new TestMessage(MessageType.Information, "GREETING", templates);

      message.AddVariable("en-US", "name", "John Doe");
      message.AddVariable("pt-BR", "name", "Fulano");

      Assert.NotEmpty(message.Variables);
      Assert.Contains(message.Variables, v => v.Name == "name" && v.Value == "John Doe");
      Assert.DoesNotContain(message.Variables, v => v.Name == "name" && v.Value == "Fulano");
   }

   [Fact]
   public void AddVariableTranslation_ShouldAddAndResolveIndividualVariableTranslations()
   {
      var message = new TestMessage(MessageType.Error);
      message.AddTextTranslation("en-US", "The field {fieldName} is invalid.");
      message.AddTextTranslation("pt-BR", "O campo {fieldName} é inválido.");

      message.AddVariable("en-US", "fieldName", "Email");
      message.AddVariable("pt-BR", "fieldName", "E-mail");

      Assert.Equal("The field Email is invalid.", message.Show("en-US"));
      Assert.Equal("O campo E-mail é inválido.", message.Show("pt-BR"));
   }

   [Fact]
   public void AddVariableTranslation_FirstTranslationExists_ShouldPopulateLegacyVariablesCollection()
   {
      var message = new TestMessage(MessageType.Information);

      message.AddVariable("pt-BR", "status", "Ativa");
      Assert.Single(message.Variables);

      // Second translation should not duplicate legacy collection
      message.AddVariable("pt-BR", "status", "Ativo");

      // Ensures only one item was added to the fallback list
      Assert.Single(message.Variables); 
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

      // 1. Mixing legacy fixed value variables
      var message = new TestMessage(MessageType.Information, "AUDIT_LOG", templates);
      message.AddVariable("en-US", "fieldName", "Birth Date");
      message.AddVariable("en-US", "user", "Admin");
      message.AddVariable("en-US", "value", "2026-01-01");

      message.AddVariable("pt-BR", "user", "Administrador");
      message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");
      message.AddVariable("pt-BR", "value", "01/01/2026");

      //before translation message should be in default language (en-US)
      Assert.Equal("User {user} set {fieldName} to {value}.", message.Text);
      Assert.Equal("User Admin set Birth Date to 2026-01-01.", message.Show());

      message.TranslateTo("pt-BR");

      //after translation message should be in the new language (pt-BR)
      Assert.Equal("Usuário {user} alterou {fieldName} para {value}.", message.Text);
      Assert.Equal("Usuário Administrador alterou Data de Nascimento para 01/01/2026.", message.Show());
   }

   [Fact]
   public void AddTextTranslation_ShouldAddAndResolveIndividualTemplates()
   {
      var message = new TestMessage(MessageType.Error);

      message.AddTextTranslation("en-US", "Static text in English.");
      message.AddTextTranslation("pt-BR", "Texto estático em Português.");

      Assert.Equal("Static text in English.", message.Show("en-US"));
      Assert.Equal("Texto estático em Português.", message.Show("pt-BR"));
   }

   [Fact]
   public void AddTextTranslation_WhenFirstLanguageIsNotEnglish_ShouldUpdateLanguage()
   {
      var templates = new Dictionary<string, string>
      {
         { "pt-BR", "Mensagem em português." },
         { "en-US", "Message in English" },
      };

      var message = new TestMessage(MessageType.Warning, "CODE", templates);

      Assert.Equal("pt-BR", message.Language);
   }

   [Fact]
   public void AddTextTranslation_WhenAddExistingLanguage_ShouldUpdateText()
   {
      var templates = new Dictionary<string, string>
      {
         { "pt-BR", "Mensagem em português." },
         { "en-US", "Message in English" },
      };

      var message = new TestMessage(MessageType.Warning, "CODE", templates);
      message.AddTextTranslation("pt-BR", "Mensagem atualizada em português.");

      Assert.Equal("pt-BR", message.Language);
      Assert.Equal("Mensagem atualizada em português.", message.Text);
      Assert.Equal("Mensagem atualizada em português.", message.Show());
   }

   [Theory]
   // Testing template placeholder syntax {var} and [var] for exact language matches
   [InlineData("en-US", "The field Birth Date must be today.")]
   [InlineData("pt-BR", "O campo Data de Nascimento deve ser a data de hoje.")]
   public void Show_WithExactLanguageMatch_ShouldResolveCorrectTemplateAndVariables(string language, string expectedOutput)
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "The field {fieldName} must be today." },
         { "pt-BR", "O campo [fieldName] deve ser a data de hoje." }
      };

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariable("en-US", "fieldName", "Birth Date");
      message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");

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

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariable("en-US", "fieldName", "Birth Date");
      message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");

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

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");

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

      var message = new TestMessage(MessageType.Error, "DATETIME_IS_TODAY", templates);
      message.AddVariable("en-US", "fieldName", "Birth Date");
      message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");

      var result = message.Show(); // Executing standard legacy parameterless method

      Assert.Equal("The field Birth Date must be today.", result);
   }

   [Fact]
   public void Show_WithMixingFixedAndMultilingualVariables_ShouldParseBothCorrectly()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "User {user} set {fieldName} to {value}." },
         { "pt-BR", "Usuário {user} alterou {fieldName} para {value}." }
      };

      // 1. Mixing legacy fixed value variables
      var message = new TestMessage(MessageType.Information, "AUDIT_LOG", templates);
      message.AddVariable("fieldName", "Birth Date");
      message.AddVariable("user", "Admin");
      message.AddVariable("value", "2026-01-01");

      message.AddVariable("pt-BR", "user", "Administrador");
      message.AddVariable("pt-BR", "fieldName", "Data de Nascimento");
      message.AddVariable("pt-BR", "value", "01/01/2026");

      string resultEn = message.Show("en-US");
      string resultPt = message.Show("pt-BR");

      Assert.Equal("User Admin set Birth Date to 2026-01-01.", resultEn);
      Assert.Equal("Usuário Administrador alterou Data de Nascimento para 01/01/2026.", resultPt);
   }

   [Fact]
   public void TranslateTo_WithMissingLanguage_ShouldKeepLanguageAndTextConsistent()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "The field {fieldName} is required." },
         { "pt-BR", "O campo {fieldName} é obrigatório." }
      };

      var message = new TestMessage(MessageType.Error, "REQUIRED", templates);
      message.AddVariable("en-US", "fieldName", "Name");

      message.TranslateTo("fr-FR");

      Assert.Equal("en-US", message.Language);
      Assert.Equal("The field {fieldName} is required.", message.Text);
      Assert.Equal("The field Name is required.", message.Show());
   }

   [Fact]
   public void Show_WithPartiallyTranslatedVariables_ShouldFallbackPerVariable()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "{fieldName} must be greater than {minimum}." },
         { "pt-BR", "{fieldName} deve ser maior que {minimum}." }
      };

      var message = new TestMessage(MessageType.Error, "MINIMUM", templates);
      message.AddVariable("en-US", "fieldName", "Age");
      message.AddVariable("en-US", "minimum", "18");
      message.AddVariable("pt-BR", "fieldName", "Idade");

      Assert.Equal("Idade deve ser maior que 18.", message.Show("pt-BR"));
   }

   [Fact]
   public void Show_ShouldMatchTemplateAndVariablesIgnoringCultureCodeCasing()
   {
      var templates = new Dictionary<string, string>
      {
         { "pt-BR", "O campo {fieldName} é obrigatório." }
      };

      var message = new TestMessage(MessageType.Error, "REQUIRED", templates);
      message.AddVariable("PT-br", "fieldName", "Nome");

      Assert.Equal("O campo Nome é obrigatório.", message.Show("pt-br"));
   }

   [Fact]
   public void Constructor_ShouldCopyTranslationsOwnedByCaller()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "Original text." }
      };

      var message = new TestMessage(MessageType.Information, "CODE", templates);
      templates["en-US"] = "Changed externally.";
      templates["pt-BR"] = "Adicionada externamente.";

      Assert.Equal("Original text.", message.Show("en-US"));
      Assert.Equal("Original text.", message.Show("pt-BR"));
   }

   [Fact]
   public void Serialization_ShouldIncludeOnlyEffectiveVariablesForCurrentLanguage()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "{fieldName} must be greater than {minimum}." },
         { "pt-BR", "{fieldName} deve ser maior que {minimum}." }
      };

      var message = new TestMessage(MessageType.Error, "MINIMUM", templates);
      message.AddVariable("en-US", "fieldName", "Age");
      message.AddVariable("en-US", "minimum", "18");
      message.AddVariable("pt-BR", "fieldName", "Idade");
      message.TranslateTo("pt-BR");

      using var document = System.Text.Json.JsonDocument.Parse(
         System.Text.Json.JsonSerializer.Serialize(message));
      var variables = document.RootElement.GetProperty("Variables");

      Assert.Equal(2, variables.GetArrayLength());
      Assert.Contains(
         variables.EnumerateArray(),
         variable => variable.GetProperty("Name").GetString() == "fieldName" &&
                     variable.GetProperty("Language").GetString() == "pt-BR");
      Assert.Contains(
         variables.EnumerateArray(),
         variable => variable.GetProperty("Name").GetString() == "minimum" &&
                     variable.GetProperty("Language").GetString() == "en-US");
   }

   [Fact]
   public void WithLanguage_ShouldTranslateCopyWithoutMutatingOriginalMessage()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "The field {fieldName} is required." },
         { "pt-BR", "O campo {fieldName} é obrigatório." }
      };

      var original = new TestMessage(MessageType.Error, "REQUIRED", templates);
      original.AddVariable("en-US", "fieldName", "Name");
      original.AddVariable("pt-BR", "fieldName", "Nome");

      var translated = original.WithLanguage("pt-BR");
      translated.Variables.Single().Value = "Nome alterado";

      Assert.NotSame(original, translated);
      Assert.Equal("en-US", original.Language);
      Assert.Equal("The field Name is required.", original.Show());
      Assert.Equal("pt-BR", translated.Language);
      Assert.Equal("O campo Nome alterado é obrigatório.", translated.Show());
      Assert.Equal("Name", original.Variables.Single().Value);
   }

   [Fact]
   public void EnumerableWithLanguage_ShouldCloneEveryMessage()
   {
      var templates = new Dictionary<string, string>
      {
         { "en-US", "Required." },
         { "pt-BR", "Obrigatório." }
      };

      var originals = new Message[]
      {
         new ErrorMessage("REQUIRED", templates),
         new WarningMessage("WARNING", templates)
      };

      var translated = originals.WithLanguage("pt-BR");

      Assert.All(translated, message => Assert.Equal("pt-BR", message.Language));
      Assert.Equal("en-US", originals[0].Language);
      Assert.Equal("en-US", originals[1].Language);
      Assert.NotSame(originals[0], translated.ElementAt(0));
      Assert.NotSame(originals[1], translated.ElementAt(1));
   }
}
