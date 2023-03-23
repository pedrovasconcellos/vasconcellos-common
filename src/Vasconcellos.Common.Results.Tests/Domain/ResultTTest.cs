using Vasconcellos.Common.Results.Domain;
using Vasconcellos.Common.Results.Enums;

namespace Vasconcellos.Common.Results.Tests.Domain
{
    public class ResultTTest
    {
        private readonly string _codeDefault = "code_default";
        private readonly string _messageDefault = "message_default";

        public ResultTTest()
        {
        }

        [Fact]
        public async Task Should_FailDefault_ReturnErrorBadDomain()
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault);

            var error = result.GetError();
            Assert.Equal(ResultStatus.BadDomain, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task Should_FailNotFound_ReturnErrorNotFound()
        {
            var result = Result<object>.FailNotFound(_codeDefault, _messageDefault);

            var error = result.GetError();
            Assert.Equal(ResultStatus.NotFound, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ResultStatus.Forbidden)]
        [InlineData(ResultStatus.Unauthorized)]
        [InlineData(ResultStatus.Unexpected)]
        [InlineData(ResultStatus.UnprocessableEntity)]
        [InlineData(ResultStatus.BadDomain)]
        public async Task Should_FailDefault_ReturnErrorTypeX(ResultStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, status);

            var error = result.GetError();
            Assert.Equal(status, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task Should_Sucess_ReturnValue()
        {
            var valueDefault = int.MaxValue;
            var result = Result<int>.Success(valueDefault);

            var error = result.GetError();
            Assert.Equal(valueDefault, result.Value);
            Assert.Null(error);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ResultStatus.Forbidden)]
        [InlineData(ResultStatus.Unauthorized)]
        [InlineData(ResultStatus.UnprocessableEntity)]
        [InlineData(ResultStatus.BadDomain)]
        public async Task Should_FailDefault_ReturnErrorTypeUnexpected(ResultStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, status);
            result.AddError(_codeDefault, _messageDefault, ResultStatus.Unexpected);

            var error = result.GetError();
            Assert.Equal(ResultStatus.Unexpected, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }
    }
}

