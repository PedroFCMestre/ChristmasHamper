using ChristmasHamper.Application.Validation;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasHamper.API.Extensions;

public static class ResultExtensions
{
    public static ActionResult HandleError<T>(this Result<T> result)
    {
        var firstError = result.Errors.FirstOrDefault();

        if (firstError is NotFoundError)
        {
            return HandleNotFoundError(result);
        }
        else if (firstError is ValidationError)
        {
            return HandleValidationError(result);
        }

        return new BadRequestObjectResult(new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Unexpected Error",
            Detail = result.Errors.FirstOrDefault()?.Message ?? string.Empty
        });
    }

    private static NotFoundObjectResult HandleNotFoundError<T>(Result<T> result)
    {
        var problem = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Detail = result.Errors.FirstOrDefault()?.Message ?? string.Empty
        };

        return new NotFoundObjectResult(problem);
    }

    private static BadRequestObjectResult HandleValidationError<T>(Result<T> result)
    {
        var errors = new Dictionary<string, string[]> { { "ValidationErrors", result.Errors.Select(e => e.Message).ToArray() } };

        var problem = new ValidationProblemDetails(errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Validation error"
        };

        return new BadRequestObjectResult(problem);
    }
}
