using ChristmasHamper.Application.Contracts.Persistence;
using ChristmasHamper.Application.Features.Organizations.Commands.DeleteOrganization;
using FluentValidation;

namespace ChristmasHamper.Application.Features.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public UpdateOrganizationCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException();

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(x => x.Acronym)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

        RuleFor(x => x)
            .MustAsync(OrganizationValidId).WithMessage("ID provided does not exists.")
            .MustAsync(OrganizationWithNameUnique).WithMessage("An organization with the same name already exists.")
            .MustAsync(OrganizationWithAcronymUnique).WithMessage("An organization with the same acronym already exists.");

    }

    private async Task<bool> OrganizationValidId(UpdateOrganizationCommand command, CancellationToken token)
    {
        return await _organizationRepository.ExistsByIdAsync(command.Id);
    }

    private async Task<bool> OrganizationWithNameUnique(UpdateOrganizationCommand command, CancellationToken token)
    {
        return await _organizationRepository.IsNameUnique(command.Id, command.Name);
    }

    private async Task<bool> OrganizationWithAcronymUnique(UpdateOrganizationCommand command, CancellationToken token)
    {
        return await _organizationRepository.IsAcronymUnique(command.Id, command.Acronym);
    }
}

