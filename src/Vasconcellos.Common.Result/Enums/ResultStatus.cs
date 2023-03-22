namespace Vasconcellos.Common.Result.Enums
{
	public enum ResultStatus : int
    {
        InvalidStatus = 0,
        Ok = 200,
        BadDomain = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        UnprocessableEntity = 422,
        Unexpected = 500
    }
}

