namespace ChristmasHamper.Domain.Primitives;

public abstract record ValueObject<T> where T : ValueObject<T>;
