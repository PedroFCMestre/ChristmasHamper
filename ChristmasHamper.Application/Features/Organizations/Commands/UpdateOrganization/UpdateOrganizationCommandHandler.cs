using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Responses;
using ChristmasHamper.Domain.Entities;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler (IOrganizationRepository organizationRepository, IMapper mapper) : IRequestHandler<UpdateOrganizationCommand, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<BaseResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validator = new UpdateOrganizationCommandValidator(_organizationRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if(validatorResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "Organization not updated because of validation errors.";
            response.ValidationErrors = [];

            foreach(var error in validatorResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
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

