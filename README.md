# Vasconcellos Common

## Description
### Vasconcellos.Common is a library for using common functions.
#### License: MIT License
#### Copyright (c) 2023 Pedro Vasconcellos
##### Author: Pedro Henrique Vasconcellos
##### Sponsor: https://vasconcellos.solutions

## Vasconcellos.Common.Result is a library to facilitate the return of function results with error messages handling.
- Nuget: https://www.nuget.org/packages/Vasconcellos.Common.Results
- Nuget .NET CLI: dotnet add package Vasconcellos.Common.Results

Example of using the Vasconcellos.Common.Result [Service].
```csharp
public async Task<Result<Guid>> ExecuteAsync(EmailCommand command)
{
    if (command == null)
        return Result<Guid>.Fail("COMMAND_IS_NULL","Request is null", ErrorType.BadDomain);
    try
    {
        var entity = new EmailEntity(command);
        if (entity.IsFailure)
            return Result<Guid>.Fail(entity.ErrorCode, entity.ErrorMessage, ErrorType.BadDomain);

        var result = await _triggerService.Executesync(entity);
        if (result.IsSuccess)
            return Result<Guid>.Success(result.Value);

        return Result<Guid>.Fail(result.Errors);
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error: {ex.Message}", ex);
        return Result<Guid>.Fail("ERROR_EXCEPTION", ex.Message, ErrorType.Unexpected);
    }
}
```
Example of using the Vasconcellos.Common.Result [Controller].
```csharp
    [HttpPost(Name = "post-example-1")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Create)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> PostExample1([FromBody] EmailCommand command)
    {
        var result = await _service.ExecuteAsync(command);

        if (result.IsSuccess)
            return this.StatusCode((int)HttpStatusCode.Create, result.Value);

        //It is recommended to use [method: GetGreaterStatus] to recover the last most serious error.
        return this.StatusCode((int)result.GetGreaterStatus(), result.Errors);
    }

    [HttpPost(Name = "post-example-2")]
    [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Create)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> PostExample2([FromBody] EmailCommand command)
    {
        var result = await _service.ExecuteAsync(command);

        if (result.IsSuccess)
            return this.StatusCode((int)HttpStatusCode.Create, result.Value);

        //The difference is here [method: GetLastStatus].
        return this.StatusCode((int)result.GetLastStatus(), result.Errors); 
    }
```


## Sponsor
[![Vasconcellos Solutions](https://vasconcellos.solutions/assets/open-source/images/company/vasconcellos-solutions-small-icon.jpg)](https://www.vasconcellos.solutions)
### Vasconcellos IT Solutions