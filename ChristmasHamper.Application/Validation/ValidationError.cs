using FluentResults;

namespace ChristmasHamper.Application.Validation;

public class ValidationError(string message) : Error(message)
{
}

public static class ValidationErrorList
{
    public static IEnumerable<IError> ConvertToValidationErrorList(this IEnumerable<string> errors)
    {
        var validationErrors = new List<IError>();

        foreach (var error in errors)
        {
            validationErrors.Add(new ValidationError(error));
        }

        return validationErrors;
    }
}
