using AddressBookAssessment.Core.AddressBook.Specifications;
using AddressBookAssessment.Core.Shared;
using Mediator;
using System.Security.Claims;

namespace AddressBookAssessment.Core.AddressBook;

public record ListAddressBookEntriesCommand : IQuery<IReadOnlyList<AddressBookEntry>>
{
	public required ClaimsPrincipal User { get; set; }
}

public class ListAddressBookEntriesQueryHandler(IReadModel readModel)
	: IQueryHandler<ListAddressBookEntriesCommand, IReadOnlyList<AddressBookEntry>>
{
	public async ValueTask<IReadOnlyList<AddressBookEntry>> Handle(ListAddressBookEntriesCommand query, CancellationToken cancellationToken)
	{
		return await readModel.Search(new AllAddressBookEntriesSpecification(), cancellationToken);
	}
}
