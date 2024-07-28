using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Validation;
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
        var validator = new CreateOrganizationCommandValidator(_organizationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            _logger.LogInformation("Validation erros occured when trying to insert {@Organization}", request.Name);

            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return Result.Fail(errors.ConvertToValidationErrorList());
        }
        
        var organization = _mapper.Map<Organization>(request);
        organization = await _organizationRepository.AddAsync(organization);

        var response = _mapper.Map<CreateOrganizationCommandResponse>(organization);
        return Result.Ok(response);
    }
}

