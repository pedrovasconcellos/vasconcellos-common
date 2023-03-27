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

            var error = result.GetErrorWithGreaterStatus();
            Assert.NotNull(error);
            Assert.Equal(ErrorStatus.BadDomain, error.ErrorStatus);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task Should_FailNotFound_ReturnErrorNotFound()
        {
            var result = Result<object>.FailNotFound(_codeDefault, _messageDefault);

            var error = result.GetErrorWithGreaterStatus();
            Assert.NotNull(error);
            Assert.Equal(ErrorStatus.NotFound, error.ErrorStatus);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorStatus.Forbidden)]
        [InlineData(ErrorStatus.Unauthorized)]
        [InlineData(ErrorStatus.Unexpected)]
        [InlineData(ErrorStatus.UnprocessableEntity)]
        [InlineData(ErrorStatus.BadDomain)]
        public async Task Should_FailDefault_ReturnErrorStatusX(ErrorStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, status);

            var error = result.GetErrorWithGreaterStatus();
            Assert.NotNull(error);
            Assert.Equal(status, error.ErrorStatus);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorStatus.Forbidden)]
        [InlineData(ErrorStatus.Unauthorized)]
        [InlineData(ErrorStatus.UnprocessableEntity)]
        [InlineData(ErrorStatus.BadDomain)]
        public async Task Should_GetErrorWithGreaterStatus_ReturnErrorStatusUnexpected(ErrorStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, status);
            result.AddError(_codeDefault, _messageDefault, ErrorStatus.Unexpected);

            var error = result.GetErrorWithGreaterStatus();
            Assert.NotNull(error);
            Assert.Equal(ErrorStatus.Unexpected, error.ErrorStatus);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorStatus.Forbidden)]
        [InlineData(ErrorStatus.Unauthorized)]
        [InlineData(ErrorStatus.UnprocessableEntity)]
        [InlineData(ErrorStatus.BadDomain)]
        public async Task Should_GetGreaterStatus_ReturnErrorStatusUnexpected(ErrorStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, status);
            result.AddError(_codeDefault, _messageDefault, ErrorStatus.Unexpected);

            var errorStatus = result.GetGreaterStatus();
            Assert.NotNull(errorStatus);
            Assert.Equal(ErrorStatus.Unexpected, errorStatus);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorStatus.Forbidden)]
        [InlineData(ErrorStatus.Unauthorized)]
        [InlineData(ErrorStatus.UnprocessableEntity)]
        [InlineData(ErrorStatus.BadDomain)]
        public async Task Should_GetErrorWithLastStatus_ReturnErrorStatusUnexpected(ErrorStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, ErrorStatus.Unexpected);
            result.AddError(_codeDefault, _messageDefault, status);

            var error = result.GetErrorWithLastStatus();
            Assert.NotNull(error);
            Assert.Equal(status, error.ErrorStatus);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorStatus.Forbidden)]
        [InlineData(ErrorStatus.Unauthorized)]
        [InlineData(ErrorStatus.UnprocessableEntity)]
        [InlineData(ErrorStatus.BadDomain)]
        public async Task Should_GetLastStatus_ReturnErrorStatusUnexpected(ErrorStatus status)
        {
            var result = Result<object>.Fail(_codeDefault, _messageDefault, ErrorStatus.Unexpected);
            result.AddError(_codeDefault, _messageDefault, status);

            var errorStatus = result.GetLastStatus();
            Assert.NotNull(errorStatus);
            Assert.Equal(status, errorStatus);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task Should_Success_ReturnValue()
        {
            var valueDefault = int.MaxValue;
            var result = Result<int>.Success(valueDefault);

            var error = result.GetErrorWithGreaterStatus();
            Assert.Equal(valueDefault, result.Value);
            Assert.Null(error);

            await Task.CompletedTask;
        }
    }
}

