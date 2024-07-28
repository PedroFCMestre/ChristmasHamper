using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public record CreateOrganizationCommand : IRequest<Result<CreateOrganizationCommandResponse>>
{
    public required string Name { get; set; }
    public required string Acronym { get; set; }
}

