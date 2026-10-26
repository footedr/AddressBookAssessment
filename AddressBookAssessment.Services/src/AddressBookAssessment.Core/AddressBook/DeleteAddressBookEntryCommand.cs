using AddressBookAssessment.Core.AddressBook.Specifications;
using AddressBookAssessment.Core.Shared;
using Mediator;
using System.Security.Claims;

namespace AddressBookAssessment.Core.AddressBook;

public record DeleteAddressBookEntryCommand : ICommand<AddressBookEntry>
{
	public required ClaimsPrincipal User { get; set; }
	public required Guid Id { get; set; }
}

public class DeleteAddressBookEntryCommandHandler(IWorkspace workspace, TimeProvider timeProvider)
	: ICommandHandler<DeleteAddressBookEntryCommand, AddressBookEntry>
{
	public async ValueTask<AddressBookEntry> Handle(DeleteAddressBookEntryCommand command, CancellationToken cancellationToken)
	{
		command.User.AssertIsAdmin();

		var results = await workspace.Load(new AddressBookEntryByIdSpecification(command.Id), cancellationToken);
		var entry = results.FirstOrDefault();

		if (entry is null || entry.DeletedAt is not null)
		{
			throw new KeyNotFoundException($"Address book entry with id: {command.Id} was not found.");
		}

		entry.Delete(timeProvider.GetUtcNow());

		return entry;
	}
}