using FluentResults;

namespace ChristmasHamper.Application.Validation;

public class NotFoundError(string message) : Error(message)
{
}
