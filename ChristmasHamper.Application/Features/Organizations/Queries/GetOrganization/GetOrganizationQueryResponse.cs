namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganization;

public record GetOrganizationQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Acronym { get; set; } = default!;
}
