using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class DateErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";

      public const string IsTodayErrorCode = nameof(IsTodayErrorCode);
      public const string IsYesterdayErrorCode = nameof(IsYesterdayErrorCode);
      public const string IsTomorrowErrorCode = nameof(IsTomorrowErrorCode);
      public const string IsInTheFutureErrorCode = nameof(IsInTheFutureErrorCode);
      public const string IsInThePastErrorCode = nameof(IsInThePastErrorCode);
      public const string IsWeekendErrorCode = nameof(IsWeekendErrorCode);
      public const string IsWeekdayErrorCode = nameof(IsWeekdayErrorCode);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            IsTodayErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be today." },
               { "pt-BR", "'{fieldName}' deve ser hoje." }
            }
         },
         {
            IsYesterdayErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be yesterday." },
               { "pt-BR", "'{fieldName}' deve ser ontem." }
            }
         },
         {
            IsTomorrowErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be tomorrow." },
               { "pt-BR", "'{fieldName}' deve ser amanhã." }
            }
         },
         {
            IsInTheFutureErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be a future date." },
               { "pt-BR", "'{fieldName}' deve ser uma data futura." }
            }
         },
         {
            IsInThePastErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be a past date." },
               { "pt-BR", "'{fieldName}' deve ser uma data passada." }
            }
         },
         {
            IsWeekendErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be a weekend date." },
               { "pt-BR", "'{fieldName}' deve ser uma data de fim de semana." }
            }
         },
         {
            IsWeekdayErrorCode, new()
            {
               { "en-US", "'{fieldName}' must be a weekday." },
               { "pt-BR", "'{fieldName}' deve ser um dia útil." }
            }
         }
      };

      public static ErrorMessage IsToday(string fieldName)
         => Create(IsTodayErrorCode, fieldName);

      public static ErrorMessage IsYesterday(string fieldName)
         => Create(IsYesterdayErrorCode, fieldName);

      public static ErrorMessage IsTomorrow(string fieldName)
         => Create(IsTomorrowErrorCode, fieldName);

      public static ErrorMessage IsInTheFuture(string fieldName)
         => Create(IsInTheFutureErrorCode, fieldName);

      public static ErrorMessage IsInThePast(string fieldName)
         => Create(IsInThePastErrorCode, fieldName);

      public static ErrorMessage IsWeekend(string fieldName)
         => Create(IsWeekendErrorCode, fieldName);

      public static ErrorMessage IsWeekday(string fieldName)
         => Create(IsWeekdayErrorCode, fieldName);

      private static ErrorMessage Create(string code, string fieldName)
      {
         var error = new ErrorMessage(code, _localTranslations[code]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}
