namespace AddressBookAssessment.Core.Shared;

public interface IEventSource
{
    IEvent[] PublishEvents();
}
