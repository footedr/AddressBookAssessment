using AddressBookAssessment.Core.AddressBook.Specifications;
using AddressBookAssessment.Core.Shared;
using Mediator;
using System.Data;
using System.Security.Claims;

namespace AddressBookAssessment.Core.AddressBook;

public record CreateAddressBookEntryCommand : ICommand<AddressBookEntry>
{
	public required ClaimsPrincipal User { get; set; }
	public required AddressBookEntryRequest AddressBookEntryRequest { get; set; }
}

public class CreateAddressBookEntryCommandHandler(IWorkspace workspace, IReadModel readModel, TimeProvider timeProvider)
	: ICommandHandler<CreateAddressBookEntryCommand, AddressBookEntry>
{
	public async ValueTask<AddressBookEntry> Handle(CreateAddressBookEntryCommand command, CancellationToken cancellationToken)
	{
		command.User.AssertIsAdmin();

		var doesItExist = await readModel.Search(new AddressBookEntryByNameSpecification(command.AddressBookEntryRequest.Name), cancellationToken);

		if (doesItExist.Any())
		{
			throw new DuplicateNameException($"Address book entry with name: {command.AddressBookEntryRequest.Name} already exists.");
		}

		var now = timeProvider.GetUtcNow();
		var entry = AddressBookEntry.Create(command.AddressBookEntryRequest, now);

		workspace.Add(entry);

		return entry;
	}
}
