using System;
using Vasconcellos.Common.Results.Enums;

namespace Vasconcellos.Common.Results.Domain
{
    public class Error
    {
        public Error(string code, string message, ErrorStatus errorStatus = ErrorStatus.BadDomain)
        {
            Code = code;
            Message = message;
            ErrorStatus = errorStatus;
            Created = DateTime.UtcNow;
        }

        public string Code { get; }
        public string Message { get; }
        public ErrorStatus ErrorStatus { get; }

        private DateTime Created { get; }
        public DateTime GetCreationDate() => Created;
    }
}

