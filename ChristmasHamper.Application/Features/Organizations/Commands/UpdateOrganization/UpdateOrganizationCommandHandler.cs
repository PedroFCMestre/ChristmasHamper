using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Responses;
using ChristmasHamper.Application.Validation;
using ChristmasHamper.Domain.Entities;
using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, Result<Unit>>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<Unit>> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {

        var validator = new UpdateOrganizationCommandValidator(_organizationRepository);
        var validatonResult = await validator.ValidateAsync(request, cancellationToken);

        if(!validatonResult.IsValid)
        {
            var errors = validatonResult.Errors.Select(e => e.ErrorMessage);
            return Result.Fail(errors.ConvertToValidationErrorList());
        }
    
        var organizationToUpdate = await _organizationRepository.GetByIdAsync(request.Id);

        if(organizationToUpdate is null)
        {
            return Result.Fail(new NotFoundError("ID provided does not exist."));
        }

        _mapper.Map(request, organizationToUpdate, typeof(UpdateOrganizationCommand), typeof(Organization));
        //_mapper.Map(request, organizationToUpdate);

        await _organizationRepository.UpdateAsync(organizationToUpdate!);
        
        return Result.Ok(Unit.Value);
    }
}
