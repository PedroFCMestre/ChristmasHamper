﻿namespace ChristmasHamper.Application.Features.Organizations.Queries.GetOrganizationsList;

public record GetOrganizationsListQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
}
