// for list
// just returns all the entries that have not been deleted,
// is ordered by entry name
using AddressBookAssessment.Core.Shared;

namespace AddressBookAssessment.Core.AddressBook.Specifications;

public class AllAddressBookEntriesSpecification : ISpecification<AddressBookEntry>
{
	public IQueryable<AddressBookEntry> Apply(IQueryable<AddressBookEntry> queryable)
	{
		return queryable.Where(entry => entry.DeletedAt == null).OrderBy(entry => entry.Name);
	}
}
