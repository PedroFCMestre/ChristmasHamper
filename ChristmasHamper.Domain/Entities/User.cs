using ChristmasHamper.Domain.Primitives;

namespace ChristmasHamper.Domain.Entities;

public class User: Entity
{
    public required string Name { get; set; }

}

