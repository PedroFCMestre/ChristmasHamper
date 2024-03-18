using ChristmasHamper.Application.Contracts.Persistence;
using FluentValidation;

namespace ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;

public class DeleteOrganizationCommandValidator : AbstractValidator<DeleteOrganizationCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public DeleteOrganizationCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));

        RuleFor(x => x)
            .MustAsync(OrganizationValidId).WithMessage("ID provided does not exists.");
    }

    private async Task<bool> OrganizationValidId(DeleteOrganizationCommand command, CancellationToken token)
    {
        return await _organizationRepository.ExistsByIdAsync(command.Id);
    }
}

