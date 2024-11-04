using ChristmasHamper.Domain.Primitives;

namespace ChristmasHamper.Domain.Entities;

public class Organization: Entity
{
    public required string Name { get; set;}
    public required string Acronym { get; set;}
}

