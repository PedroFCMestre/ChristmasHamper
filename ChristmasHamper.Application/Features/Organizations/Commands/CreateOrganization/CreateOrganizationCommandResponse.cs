namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public record CreateOrganizationCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Acronym { get; set; } = default!;
}

