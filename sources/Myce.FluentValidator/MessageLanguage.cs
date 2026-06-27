using System.ComponentModel;

namespace Myce.FluentValidator;

public sealed class MessageLanguage
{
   private readonly string _cultureCode;

   public static readonly MessageLanguage English = new("en-US");
   public static readonly MessageLanguage PortugueseBrazil = new("pt-BR");

   private MessageLanguage(string cultureCode)
   {
      _cultureCode = cultureCode;
   }

   public static MessageLanguage Register(string cultureCode)
   {
      return new MessageLanguage(cultureCode);
   }

   public static implicit operator string(MessageLanguage language) => language._cultureCode;

   public override string ToString() => _cultureCode;
}
