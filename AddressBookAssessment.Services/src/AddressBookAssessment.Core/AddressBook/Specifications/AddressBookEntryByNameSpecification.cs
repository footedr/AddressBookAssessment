using AddressBookAssessment.Core.Shared;

namespace AddressBookAssessment.Core.AddressBook.Specifications;

public class AddressBookEntryByNameSpecification(string name) : ISpecification<AddressBookEntry>
{
	public IQueryable<AddressBookEntry> Apply(IQueryable<AddressBookEntry> queryable)
	{
		return queryable.Where(entry => entry.Name.ToUpper() == name.Trim().ToUpper());
	}
}
