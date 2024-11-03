using ChristmasHamper.Domain.Common;

namespace ChristmasHamper.Domain.Entities;

public class Committee: Entity
{
    public required Organization Organization { get; set; }

    public required string Name { get; set;}

    public int Year { get; set; }

    public required IEnumerable<User> Members { get; set; }
}

