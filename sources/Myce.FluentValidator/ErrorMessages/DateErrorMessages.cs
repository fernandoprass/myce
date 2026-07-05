using System.Collections.Generic;
using Myce.Response.Messages;

namespace Myce.FluentValidator.ErrorMessages
{
   public class DateErrorMessages : ErrorMessage
   {
      private const string FieldName = "fieldName";

      public const string IsTodayError = nameof(IsTodayError);
      public const string IsYesterdayError = nameof(IsYesterdayError);
      public const string IsTomorrowError = nameof(IsTomorrowError);
      public const string IsInTheFutureError = nameof(IsInTheFutureError);
      public const string IsInThePastError = nameof(IsInThePastError);
      public const string IsWeekendError = nameof(IsWeekendError);
      public const string IsWeekdayError = nameof(IsWeekdayError);

      // Centralized dictionary structured as: [Code] -> [Language] -> [Template Text]
      private static readonly Dictionary<string, Dictionary<string, string>> _localTranslations = new()
      {
         {
            IsTodayError, new()
            {
               { "en-US", "'{fieldName}' must be today." },
               { "pt-BR", "'{fieldName}' deve ser hoje." }
            }
         },
         {
            IsYesterdayError, new()
            {
               { "en-US", "'{fieldName}' must be yesterday." },
               { "pt-BR", "'{fieldName}' deve ser ontem." }
            }
         },
         {
            IsTomorrowError, new()
            {
               { "en-US", "'{fieldName}' must be tomorrow." },
               { "pt-BR", "'{fieldName}' deve ser amanhã." }
            }
         },
         {
            IsInTheFutureError, new()
            {
               { "en-US", "'{fieldName}' must be a future date." },
               { "pt-BR", "'{fieldName}' deve ser uma data futura." }
            }
         },
         {
            IsInThePastError, new()
            {
               { "en-US", "'{fieldName}' must be a past date." },
               { "pt-BR", "'{fieldName}' deve ser uma data passada." }
            }
         },
         {
            IsWeekendError, new()
            {
               { "en-US", "'{fieldName}' must be a weekend date." },
               { "pt-BR", "'{fieldName}' deve ser uma data de fim de semana." }
            }
         },
         {
            IsWeekdayError, new()
            {
               { "en-US", "'{fieldName}' must be a weekday." },
               { "pt-BR", "'{fieldName}' deve ser um dia útil." }
            }
         }
      };

      public static ErrorMessage IsToday(string fieldName)
         => Create(IsTodayError, fieldName);

      public static ErrorMessage IsYesterday(string fieldName)
         => Create(IsYesterdayError, fieldName);

      public static ErrorMessage IsTomorrow(string fieldName)
         => Create(IsTomorrowError, fieldName);

      public static ErrorMessage IsInTheFuture(string fieldName)
         => Create(IsInTheFutureError, fieldName);

      public static ErrorMessage IsInThePast(string fieldName)
         => Create(IsInThePastError, fieldName);

      public static ErrorMessage IsWeekend(string fieldName)
         => Create(IsWeekendError, fieldName);

      public static ErrorMessage IsWeekday(string fieldName)
         => Create(IsWeekdayError, fieldName);

      private static ErrorMessage Create(string code, string fieldName)
      {
         var error = new ErrorMessage(code, _localTranslations[code]);
         error.AddVariable(FieldName, fieldName);
         return error;
      }
   }
}
