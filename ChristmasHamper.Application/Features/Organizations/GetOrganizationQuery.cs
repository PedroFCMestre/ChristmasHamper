using MediatR;

namespace ChristmasHamper.Application.Features.Organizations;

public class GetOrganizationQuery : IRequest<OrganizationDto>
{
    public int Id { get; set; }
}

