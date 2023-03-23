namespace Vasconcellos.Common.Results.Enums
{
	public enum ErrorType : int
    {
        InvalidStatus = 0,
        BadDomain = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        UnprocessableEntity = 422,
        Unexpected = 500
    }
}

