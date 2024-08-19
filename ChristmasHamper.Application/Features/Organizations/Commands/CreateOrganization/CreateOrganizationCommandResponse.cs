namespace ChristmasHamper.Application.Features.Organizations.Commands.CreateOrganization;

public record CreateOrganizationCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
}

