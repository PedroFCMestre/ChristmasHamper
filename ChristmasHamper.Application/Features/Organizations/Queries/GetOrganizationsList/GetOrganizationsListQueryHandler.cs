using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;

public class GetOrganizationsListQueryHandler(IAsyncRepository<Organization> organizationRepository, IMapper mapper) : IRequestHandler<GetOrganizationsListQuery, List<OrganizationDto>>
{
    private readonly IAsyncRepository<Organization> _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<OrganizationDto>> Handle(GetOrganizationsListQuery request, CancellationToken cancellationToken)
    {
        var allOrganizations = await _organizationRepository.ListAllAsync();

        return _mapper.Map<List<OrganizationDto>>(allOrganizations);
    }
}

