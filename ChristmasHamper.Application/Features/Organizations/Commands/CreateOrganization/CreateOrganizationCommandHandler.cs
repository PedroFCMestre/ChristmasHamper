using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, CreateOrganizationCommandResponse>
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

    public async Task<CreateOrganizationCommandResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateOrganizationCommandResponse();

        var validator = new CreateOrganizationCommandValidator(_organizationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validationResult.IsValid)
        {
            _logger.LogInformation("Validation erros occured when trying to insert {@Organization}", request.Name);

            response.Success = false;
            response.Message = "Organization not created because of validation errors.";
            response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
        else
        {
            var organization = _mapper.Map<Organization>(request);

            organization = await _organizationRepository.AddAsync(organization);

            response.Id = organization.Id;
        }

        return response;
    }
}

