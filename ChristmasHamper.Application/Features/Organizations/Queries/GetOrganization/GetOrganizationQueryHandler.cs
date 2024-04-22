using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;

public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, OrganizationDto>
{
    private readonly IAsyncRepository<Organization> _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationQueryHandler(IAsyncRepository<Organization> organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<OrganizationDto> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        return _mapper.Map<OrganizationDto>(organization);
    }
}

