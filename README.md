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

Example of using the Vasconcellos.Common.Result.
```csharp
public async Task<Result<Guid>> ExecuteAsync(EmailCommand command)
{
    if (command == null)
        return Result<Guid>.Fail("COMMAND_IS_NULL","Request is null", ErrorType.BadDomain);
    try
    {
        var entity = new EmailEntity(command);
        if (entity.IsFailure)
            return Result<Guid>.Fail(entity.ErrorCode, entity.ErrorMessage, ErrorType.UnprocessableEntity);

        var resultID = await _triggerService.Executesync(entity);
        
        return Result<Guid>.Success(resultID);
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error: {ex.Message}", ex);
        return Result<Guid>.Fail("ERROR_EXCEPTION", ex.Message, ErrorType.Unexpected);
    }
}
```

## Sponsor
[![Vasconcellos Solutions](https://vasconcellos.solutions/assets/open-source/images/company/vasconcellos-solutions-small-icon.jpg)](https://www.vasconcellos.solutions)
### Vasconcellos IT Solutions