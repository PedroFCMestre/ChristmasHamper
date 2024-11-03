using ChristmasHamper.Domain.Common;

namespace ChristmasHamper.Domain.Entities;

public class User: Entity
{
    public required string Name { get; set; }

    public required Organization Organization { get; set; }
}

