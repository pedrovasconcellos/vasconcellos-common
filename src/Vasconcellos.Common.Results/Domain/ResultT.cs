using System.Collections.Generic;
using System.Linq;
using Vasconcellos.Common.Results.Enums;

namespace Vasconcellos.Common.Results.Domain
{
    public class Result<T>
    {
        public T Value { get; private set; } = default!;
        public IList<Error> Errors { get; private set; }
        public bool IsFailure => Errors.Any();
        public bool IsSuccess => !IsFailure;

        private Result(T value)
        {
            Value = value;
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

        public static Result<T> Fail(Error error)
        {
            var result = new Result<T>(error);
            return result;
        }

        public static Result<T> Fail(IList<Error> errors)
        {
            var result = new Result<T>(errors);
            return result;
        }

        public static Result<T> Fail(string code, string message, ErrorType type = ErrorType.BadDomain)
        {
            var result = new Result<T>(code, message, type);
            return result;
        }

        public static Result<T> FailNotFound(string code, string message)
        {
            var result = new Result<T>(code, message, ErrorType.NotFound);
            return result;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public Result<T> AddError(Error error)
        {
            this.Errors.Add(error);
            return this;
        }

        public Result<T> AddError(string code, string message, ErrorType type = ErrorType.BadDomain)
        {
            this.AddError(new Error(code, message, type));
            return this;
        }

        public Error GetError() => Errors
            .OrderByDescending(x => (int)x.Type)
            .FirstOrDefault();
    }
}

