using AutoMapper;
using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Responses;
using ChristmasHamper.Application.Validation;
using FluentResults;
using MediatR;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, Result<Unit>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));
    }

    public async Task<Result<Unit>> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organizationToDelete = await _organizationRepository.GetByIdAsync(request.Id);

        if (organizationToDelete is null)
        {
            return Result.Fail(new NotFoundError("ID provided does not exist."));
        }

        await _organizationRepository.DeleteAsync(organizationToDelete!);
        
        return Result.Ok(Unit.Value);
    }
}
