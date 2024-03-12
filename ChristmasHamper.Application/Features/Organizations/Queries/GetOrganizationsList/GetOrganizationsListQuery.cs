using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;

public record GetOrganizationsListQuery : IRequest<List<OrganizationDto>>;

/*public class GetOrganizationsListQuery : IRequest<List<OrganizationDto>>
{
}
*/
