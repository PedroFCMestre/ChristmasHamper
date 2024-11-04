using ChristmasHamper.Domain.Primitives;

namespace ChristmasHamper.Domain.Entities;

public class Committee: AggregateRoot
{
    public int Year { get; private set; }

    public required IEnumerable<User> Members { get; set; }
}

