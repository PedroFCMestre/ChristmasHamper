using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList
{
    public class GetOrganizationsListQuery : IRequest<List<OrganizationDto>>
    {
    }
}
