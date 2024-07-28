using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;

public class GetOrganizationsListQueryHandler : IRequestHandler<GetOrganizationsListQuery, Result<List<GetOrganizationsListQueryResponse>>>
{
    private readonly IAsyncRepository<Organization> _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationsListQueryHandler(IAsyncRepository<Organization> organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<List<GetOrganizationsListQueryResponse>>> Handle(GetOrganizationsListQuery request, CancellationToken cancellationToken)
    {
        var allOrganizations = await _organizationRepository.ListAllAsync();

        return Result.Ok(_mapper.Map<List<GetOrganizationsListQueryResponse>>(allOrganizations));
    }
}

