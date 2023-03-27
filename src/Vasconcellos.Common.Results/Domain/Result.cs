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

        private Result(string code, string message, ErrorStatus errorStatus = ErrorStatus.BadDomain)
        {
            var error = new Error(code, message, errorStatus);
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

        public static Result Fail(string code, string message, ErrorStatus errorStatus = ErrorStatus.BadDomain)
        {
            var result = new Result(code, message, errorStatus);
            return result;
        }

        public static Result FailNotFound(string code, string message)
        {
            var result = new Result(code, message, ErrorStatus.NotFound);
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

        public Result AddError(string code, string message, ErrorStatus errorStatus = ErrorStatus.BadDomain)
        {
            this.AddError(new Error(code, message, errorStatus));
            return this;
        }

        /// <summary>
        /// Recovers the last most serious error.
        /// </summary>
        /// <returns>Error?</returns>
        public Error? GetErrorWithGreaterStatus() => Errors
            .OrderByDescending(x => (int)x.ErrorStatus)
            .ThenByDescending(x => x.GetCreationDate())
            .FirstOrDefault();

        /// <summary>
        /// Recovers the last most serious status.
        /// </summary>
        /// <returns>ErrorStatus?</returns>
        public ErrorStatus? GetGreaterStatus() =>
            GetErrorWithGreaterStatus()?.ErrorStatus;

        /// <summary>
        /// Recovers the last error.
        /// </summary>
        /// <returns>Error?</returns>
        public Error? GetErrorWithLastStatus() => Errors
            .LastOrDefault();


        /// <summary>
        /// Recovers the last status.
        /// </summary>
        /// <returns>ErrorStatus?</returns>
        public ErrorStatus? GetLastStatus() =>
            GetErrorWithLastStatus()?.ErrorStatus;
    }
}

