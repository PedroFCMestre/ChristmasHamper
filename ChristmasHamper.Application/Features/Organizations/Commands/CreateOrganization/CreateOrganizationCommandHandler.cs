using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Exceptions;
using ChristmasHamper.Domain.Entities;
using FluentValidation.Results;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper) : IRequestHandler<CreateOrganizationCommand, CreateOrganizationCommandResponse>
{
    private readonly IOrganizationRepository _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<CreateOrganizationCommandResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateOrganizationCommandResponse();

        var validator = new CreateOrganizationCommandValidator(_organizationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Organization not created because of validation errors.";
            response.ValidationErrors = [];
            
            foreach(var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
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

