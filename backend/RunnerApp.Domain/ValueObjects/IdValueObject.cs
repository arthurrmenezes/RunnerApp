namespace RunnerApp.Domain.ValueObjects;

public readonly struct IdValueObject
{
    public readonly Guid Id { get; }

    private IdValueObject(Guid id)
    {
        Id = id;
    }

    public static IdValueObject Factory(Guid? id = null)
    {
        if (id == Guid.Empty || id == null)
            throw new ArgumentException("ID cannot be empty.", nameof(id));

        return new IdValueObject((Guid)id);
    }

    public Guid GetId()
    {
        return Id;
    }

    public static IdValueObject New()
    {
        return new IdValueObject(Guid.NewGuid());
    }

    public override string ToString() => Id.ToString();

    public static implicit operator Guid(IdValueObject id) => id.Id;
    public static implicit operator IdValueObject(Guid id) => Factory(id);
}
