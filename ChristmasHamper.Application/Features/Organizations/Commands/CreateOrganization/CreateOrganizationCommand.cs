using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommand : IRequest<CreateOrganizationCommandResponse>
{
    public required string Name { get; set; }
    public required string Acronym { get; set; }
}

