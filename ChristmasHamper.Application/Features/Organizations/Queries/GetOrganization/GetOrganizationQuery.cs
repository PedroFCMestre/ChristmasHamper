using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;

public record GetOrganizationQuery(int Id) : IRequest<OrganizationDto>;

/*public class GetOrganizationQuery : IRequest<OrganizationDto>
{
    public int Id { get; set; }
}*/

