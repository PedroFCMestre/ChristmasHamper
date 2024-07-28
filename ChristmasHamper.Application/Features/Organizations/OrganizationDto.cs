﻿namespace ChristmasHamper.Application.Features.Organizations;

public class OrganizationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Acronym { get; set; } = default!;
}
