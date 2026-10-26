using AddressBookAssessment.Core.AddressBook.Specifications;
using AddressBookAssessment.Core.Shared;
using Mediator;
using System.Data;
using System.Security.Claims;

namespace AddressBookAssessment.Core.AddressBook;

public record UpdateAddressBookEntryCommand : ICommand<AddressBookEntry>
{
	public required ClaimsPrincipal User { get; set; }
	public required AddressBookEntryRequest AddressBookEntryRequest { get; set; }
	public required Guid Id { get; set; }
}

public class UpdateAddressBookEntryCommandHandler(IWorkspace workspace, IReadModel readModel, TimeProvider timeProvider)
	: ICommandHandler<UpdateAddressBookEntryCommand, AddressBookEntry>
{
	public async ValueTask<AddressBookEntry> Handle(UpdateAddressBookEntryCommand command, CancellationToken cancellationToken)
	{
		command.User.AssertIsAdmin();

		var results = await workspace.Load(new AddressBookEntryByIdSpecification(command.Id), cancellationToken);
		var entry = results.FirstOrDefault();

		if (entry is null || entry.DeletedAt is not null)
		{
			throw new KeyNotFoundException($"Address book entry with id: {command.Id} was not found.");
		}

		var sameName = await readModel.Search(new AddressBookEntryByNameSpecification(command.AddressBookEntryRequest.Name), cancellationToken);

		if (sameName.Any(other => other.Id != command.Id))
		{
			throw new DuplicateNameException($"Address book entry with name: {command.AddressBookEntryRequest.Name} already exists.");
		}

		entry.Update(command.AddressBookEntryRequest, timeProvider.GetUtcNow());

		return entry;
	}
}