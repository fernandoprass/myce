using System.Net;

namespace Myce.Response
{
   public static class ResultExtensions
   {
      public static Result<IEnumerable<T>> Merge<T>(this IEnumerable<Result<T>> results)
      {
         return new Result<IEnumerable<T>>(results.Select(x => x.Data), results.SelectMany(x => x.Messages));
      }

      public static bool HasHttpStatusCode(this Result result, HttpStatusCode statusCode)
      {
         return result.Messages.Any(x => x.Code == statusCode.ToString());
      }
   }
}
