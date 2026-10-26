// looks an entry up by its ID rather than by the name,
// just incase someone updates their name mkaes an unexpected change that would require an ID search instead of by name.
// Using it for Update and Delete
using AddressBookAssessment.Core.Shared;

namespace AddressBookAssessment.Core.AddressBook.Specifications;

public class AddressBookEntryByIdSpecification(Guid id) : ISpecification<AddressBookEntry>
{
	public IQueryable<AddressBookEntry> Apply(IQueryable<AddressBookEntry> queryable)
	{
		return queryable.Where(entry => entry.Id == id);
	}
}
