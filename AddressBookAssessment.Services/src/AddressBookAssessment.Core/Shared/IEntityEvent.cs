namespace AddressBookAssessment.Core.Shared;

public interface IEntityEvent : IEvent
{
    public string EntityId { get; }
}
