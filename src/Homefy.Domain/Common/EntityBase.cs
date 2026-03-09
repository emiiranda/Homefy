namespace Homefy.Domain.Common;

public abstract class EntityBase
{
    public Guid Id { get; protected set; }

    protected EntityBase()
    {
        Id = Guid.NewGuid();
    }

    protected EntityBase(Guid id)
    {
        Id = id == Guid.Empty
            ? throw new ArgumentException("Entity id cannot be empty.", nameof(id))
            : id;
    }
}