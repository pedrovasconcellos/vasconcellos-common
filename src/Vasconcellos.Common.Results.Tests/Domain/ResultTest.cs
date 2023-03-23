using Vasconcellos.Common.Results.Domain;
using Vasconcellos.Common.Results.Enums;

namespace Vasconcellos.Common.Results.Tests.Domain
{
    public class ResultTest
    {
        private readonly string _codeDefault = "code_default";
        private readonly string _messageDefault = "message_default";

        public ResultTest()
        {
        }

        [Fact]
        public async Task Should_FailDefault_ReturnErrorBadDomain()
        {
            var result = Result.Fail(_codeDefault, _messageDefault);

            var error = result.GetError();
            Assert.Equal(ErrorType.BadDomain, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task Should_FailNotFound_ReturnErrorNotFound()
        {
            var result = Result.FailNotFound(_codeDefault, _messageDefault);

            var error = result.GetError();
            Assert.Equal(ErrorType.NotFound, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorType.Forbidden)]
        [InlineData(ErrorType.Unauthorized)]
        [InlineData(ErrorType.Unexpected)]
        [InlineData(ErrorType.UnprocessableEntity)]
        [InlineData(ErrorType.BadDomain)]
        public async Task Should_FailDefault_ReturnErrorTypeX(ErrorType status)
        {
            var result = Result.Fail(_codeDefault, _messageDefault, status);

            var error = result.GetError();
            Assert.Equal(status, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task Should_Sucess_ReturnValue()
        {
            var result = Result.Success();

            var error = result.GetError();
            Assert.NotNull(result);
            Assert.Null(error);

            await Task.CompletedTask;
        }

        [Theory]
        [InlineData(ErrorType.Forbidden)]
        [InlineData(ErrorType.Unauthorized)]
        [InlineData(ErrorType.UnprocessableEntity)]
        [InlineData(ErrorType.BadDomain)]
        public async Task Should_FailDefault_ReturnErrorTypeUnexpected(ErrorType status)
        {
            var result = Result.Fail(_codeDefault, _messageDefault, status);
            result.AddError(_codeDefault, _messageDefault, ErrorType.Unexpected);

            var error = result.GetError();
            Assert.Equal(ErrorType.Unexpected, error.Type);
            Assert.Equal(_codeDefault, error.Code);
            Assert.Equal(_messageDefault, error.Message);

            await Task.CompletedTask;
        }
    }
}

