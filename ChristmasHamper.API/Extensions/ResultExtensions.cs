using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ChristmasHamper.API.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails HandleNotFoundError(this Result result)
    {
        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "Resource not found",
            Status = StatusCodes.Status404NotFound,
            Detail = result.Errors.FirstOrDefault()?.Message ?? string.Empty
        };

        return details;
    }

    public static ValidationProblemDetails HandleValidationError<T>(this Result<T> result)
    {
        var errors = new Dictionary<string, string[]> { { "ValidationErrors", result.Errors.Select(e => e.Message).ToArray() } };

        var details = new ValidationProblemDetails(errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Validation error"
        };

        return details;
    }
}
