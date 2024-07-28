using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;

public record UpdateOrganizationCommand : IRequest<Result<Unit>>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Acronym { get; set; }
}

