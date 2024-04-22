using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Responses;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, BaseResponse>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    }

    public async Task<BaseResponse> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();

        var validator = new DeleteOrganizationCommandValidator(_organizationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if(validationResult.Errors.Count > 0) 
        {
            response.Success = false;
            response.ValidationErrors = [];

            foreach(var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        else
        {
            var organizationToDelete = await _organizationRepository.GetByIdAsync(request.Id);

            await _organizationRepository.DeleteAsync(organizationToDelete!);
        }

        return response;
    }
}

