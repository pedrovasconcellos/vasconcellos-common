using System.Collections.Generic;
using System.Linq;
using Vasconcellos.Common.Result.Enums;

namespace Vasconcellos.Common.Result.Domain
{
    public class Result<T>
    {
        public T Value { get; private set; }
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

        private Result(string code, string message, ResultStatus type = ResultStatus.BadDomain)
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

        public static Result<T> Fail(string code, string message, ResultStatus type = ResultStatus.BadDomain)
        {
            var result = new Result<T>(code, message, type);
            return result;
        }

        public static Result<T> FailNotFound(string code, string message)
        {
            var result = new Result<T>(code, message, ResultStatus.NotFound);
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

        public Result<T> AddError(string code, string message, ResultStatus type = ResultStatus.BadDomain)
        {
            this.Errors.Add(new Error(code, message, type));
            return this;
        }
    }
}

