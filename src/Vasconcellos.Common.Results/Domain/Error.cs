using Vasconcellos.Common.Results.Enums;

namespace Vasconcellos.Common.Results.Domain
{
    public class Error
    {
        public Error(string code, string message, ResultStatus type = ResultStatus.BadDomain)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public string Code { get; }
        public string Message { get; }
        public ResultStatus Type { get; }
    }
}

