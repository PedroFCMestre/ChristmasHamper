using MediatR;

namespace ChristmasHamper.Application.Features.Organizations
{
    public class GetOrganizationsListQuery : IRequest<List<OrganizationDto>>
    {
    }
}
