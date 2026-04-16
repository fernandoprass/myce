using Myce.Response.Messages;
using Xunit;

namespace Myce.Response.Tests;

public class ResultExtensionsTests
{
   [Fact]
   public void Merge_ShouldReturnFailure_WhenAtLeastOneResultHasError()
   {
      var results = new List<Result<int>>
        {
            Result<int>.Success(10),
            Result<int>.Failure(new ErrorMessage("Error")),
            Result<int>.Success(30)
        };

      var merged = results.Merge();

      Assert.False(merged.IsSuccess);
      Assert.Contains(merged.Messages, m => m.Type == MessageType.Error);
      Assert.Equal(3, merged.Data.Count()); 
   }

   [Fact]
   public void Merge_ShouldReturnSuccess_WhenAllResultsAreValid()
   {
      var results = new List<Result<string>> { Result<string>.Success("A"), Result<string>.Success("B") };

      var merged = results.Merge();

      Assert.True(merged.IsSuccess);
      Assert.Empty(merged.Messages.Where(x => x.Type == MessageType.Error));
   }
}