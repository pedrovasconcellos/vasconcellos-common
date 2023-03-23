using System.Collections.Generic;
using System.Linq;
using Vasconcellos.Common.Results.Enums;

namespace Vasconcellos.Common.Results.Domain
{
    public class Result
    {
        public IList<Error> Errors { get; private set; }
        public bool IsFailure => Errors.Any();
        public bool IsSuccess => !IsFailure;

        private Result()
        {
            Errors = new List<Error>();
        }

        private Result(IList<Error> errors)
        {
            Errors = errors;
        }

        private Result(Error error)
        {
            Errors = new List<Error> { error };
        }

        private Result(string code, string message, ErrorType type = ErrorType.BadDomain)
        {
            var error = new Error(code, message, type);
            Errors = new List<Error> { error };
        }

        public static Result Fail(Error error)
        {
            var result = new Result(error);
            return result;
        }

        public static Result Fail(IList<Error> errors)
        {
            var result = new Result(errors);
            return result;
        }

        public static Result Fail(string code, string message, ErrorType type = ErrorType.BadDomain)
        {
            var result = new Result(code, message, type);
            return result;
        }

        public static Result FailNotFound(string code, string message)
        {
            var result = new Result(code, message, ErrorType.NotFound);
            return result;
        }

        public static Result Success()
        {
            return new Result();
        }

        public Result AddError(Error error)
        {
            this.Errors.Add(error);
            return this;
        }

        public Result AddError(string code, string message, ErrorType type = ErrorType.BadDomain)
        {
            this.AddError(new Error(code, message, type));
            return this;
        }

        public Error GetError() => Errors
            .OrderByDescending(x => (int)x.Type)
            .FirstOrDefault();
    }
}

