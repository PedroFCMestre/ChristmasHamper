using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations;

public class GetOrganizationsListQueryHandler : IRequestHandler<GetOrganizationsListQuery, List<OrganizationDto>>
{
    private readonly IAsyncRepository<Organization> _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationsListQueryHandler(IAsyncRepository<Organization> organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

    public async Task<List<OrganizationDto>> Handle(GetOrganizationsListQuery request, CancellationToken cancellationToken)
    {
        var allOrganizations = await _organizationRepository.ListAllAsync();

        return _mapper.Map<List<OrganizationDto>>(allOrganizations);
    }
}

