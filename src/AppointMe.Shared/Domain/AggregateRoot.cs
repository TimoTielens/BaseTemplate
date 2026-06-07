namespace AppointMe.Shared.Domain;

public abstract record AggregateRoot
{
    public void Raise(IDomainEvent @event)
    {
        Events.Add(@event);
    }

    public readonly List<IDomainEvent> Events = [];
}
