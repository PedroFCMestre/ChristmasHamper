using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Result<CreateOrganizationCommandResponse>>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateOrganizationCommandHandler> _logger;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper, ILogger<CreateOrganizationCommandHandler> logger)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<CreateOrganizationCommandResponse>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateOrganizationCommandResponse();

        var validator = new CreateOrganizationCommandValidator(_organizationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            _logger.LogInformation("Validation erros occured when trying to insert {@Organization}", request.Name);

            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        

        var organization = _mapper.Map<Organization>(request);

        organization = await _organizationRepository.AddAsync(organization);

        response.Id = organization.Id;        

        return Result.Ok(response);
    }
}

