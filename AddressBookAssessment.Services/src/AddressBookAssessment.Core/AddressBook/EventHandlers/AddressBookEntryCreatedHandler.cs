using Mediator;
using Microsoft.Extensions.Logging;

namespace AddressBookAssessment.Core.AddressBook.EventHandlers;

public class AddressBookEntryCreatedHandler(ILogger<AddressBookEntryCreatedHandler> logger) : INotificationHandler<AddressBookEntryCreated>
{
	public ValueTask Handle(AddressBookEntryCreated notification, CancellationToken cancellationToken)
	{
		logger.LogInformation($"AddressBook entry# {notification.Id}, with name: {notification.Name} created.");

		return ValueTask.CompletedTask;
	}
}
