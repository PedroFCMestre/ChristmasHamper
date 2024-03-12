using ChristmasHamper.Application.Responses;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommand : IRequest<BaseResponse>
{
    public int OrganizationId { get; set; }
    public required string Name { get; set; }
    public required string Acronym { get; set; }
}

