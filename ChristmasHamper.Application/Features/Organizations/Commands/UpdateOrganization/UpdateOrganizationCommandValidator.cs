using ChristmasHamper.Application.Contracts.Persistence;
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
            .Length(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(x => x.Acronym)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .MaximumLength(10).WithMessage("{PropertyName} must not exceed 10 characters.");

        RuleFor(x => x)
            .MustAsync(OrganizationWithNameUnique).WithMessage("An organization with the same name already exists.")
            .MustAsync(OrganizationWithAcronymUnique).WithMessage("An organization with the same acronym already exists.");

    }

    private async Task<bool> OrganizationWithNameUnique(UpdateOrganizationCommand command, CancellationToken token)
    {
        return !(await _organizationRepository.IsNameUnique(command.OrganizationId, command.Name));
    }

    private async Task<bool> OrganizationWithAcronymUnique(UpdateOrganizationCommand command, CancellationToken token)
    {
        return !(await _organizationRepository.IsAcronymUnique(command.OrganizationId, command.Acronym));
    }
}

