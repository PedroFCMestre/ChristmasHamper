using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;

public class GetOrganizationQuery : IRequest<OrganizationDto>
{
    public int Id { get; set; }
}

