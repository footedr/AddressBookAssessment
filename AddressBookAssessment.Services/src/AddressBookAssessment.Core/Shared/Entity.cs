namespace AddressBookAssessment.Core.Shared;

public abstract class Entity<TId> : IEntity<TId>, IEventSource
{
    public required TId Id { get; init; }

    private readonly List<IEvent> _events = [];

    protected void RecordEvent(IEvent @event)
    {
        _events.Add(@event);
    }
    public IEvent[] PublishEvents()
    {
        var events = _events.ToArray();
        _events.Clear();
        return events;
    }
}
