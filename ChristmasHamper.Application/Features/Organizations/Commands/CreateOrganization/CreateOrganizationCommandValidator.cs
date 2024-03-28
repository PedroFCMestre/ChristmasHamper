using ChristmasHamper.Application.Contracts.Persistence;
using FluentValidation;

namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommandValidator : AbstractValidator<CreateOrganizationCommand>
{
    private readonly IOrganizationRepository _organizationRepository;

    public CreateOrganizationCommandValidator(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository ?? throw new ArgumentNullException(nameof(organizationRepository));

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(x => x.Acronym)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

        RuleFor(x => x)
            .MustAsync(OrganizationWithUniqueName).WithMessage("An organization with the same name already exists.")
            .MustAsync(OrganizationWithUniqueAcronym).WithMessage("An organization with the same acronym already exists.");
    }

    private async Task<bool> OrganizationWithUniqueName(CreateOrganizationCommand command, CancellationToken token)
    {
        return await _organizationRepository.IsNameUnique(command.Name); ;
    }

    private async Task<bool> OrganizationWithUniqueAcronym(CreateOrganizationCommand command, CancellationToken token)
    {
        return await _organizationRepository.IsAcronymUnique(command.Acronym);
    }
}

