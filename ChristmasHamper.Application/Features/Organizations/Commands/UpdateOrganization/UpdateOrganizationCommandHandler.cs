﻿using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Responses;
using ChristmasHamper.Domain.Entities;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<BaseResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validator = new UpdateOrganizationCommandValidator(_organizationRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validatorResult.IsValid)
        {
            response.Success = false;
            response.Message = "Organization not updated because of validation errors.";
            response.ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
        }
        else
        {
            var organizationToUpdate = await _organizationRepository.GetByIdAsync(request.Id);

            _mapper.Map(request, organizationToUpdate, typeof(UpdateOrganizationCommand), typeof(Organization));
            //_mapper.Map(request, organizationToUpdate);

            await _organizationRepository.UpdateAsync(organizationToUpdate!);
        }

        return response;
    }
}

