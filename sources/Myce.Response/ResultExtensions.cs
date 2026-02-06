using System.Net;

namespace Myce.Response
{
   public static class ResultExtensions
   {
      public static Result<IEnumerable<T>> Merge<T>(this IEnumerable<Result<T>> results)
      {
         var resultList = results.ToList();
         var data = resultList.Select(x => x.Data);
         var messages = resultList.SelectMany(x => x.Messages);

         return new Result<IEnumerable<T>>(data, messages);
      }

      public static bool HasHttpStatusCode(this Result result, HttpStatusCode statusCode)
      {
         return result.Messages.Any(x => x.Code == statusCode.ToString());
      }
   }
}
