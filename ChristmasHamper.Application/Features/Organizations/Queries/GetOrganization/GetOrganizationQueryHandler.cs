using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Validation;
using ChristmasHamper.Domain.Entities;
using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;

public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, Result<GetOrganizationQueryResponse>>
{
    private readonly IAsyncRepository<Organization> _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationQueryHandler(IAsyncRepository<Organization> organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<GetOrganizationQueryResponse>> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if(organization is null)
        {
            return Result.Fail(new NotFoundError("Organization ID not found."));
        }

        return Result.Ok(_mapper.Map<GetOrganizationQueryResponse>(organization));
    }
}

