namespace AddressBookAssessment.Core.AddressBook;

public record AddressBookEntryRequest
{
	public required string Name { get; init; }
	public required Address Address { get; init; }
	public required TimezoneId TimezoneId { get; init; }
	public IReadOnlyList<AddressBookContact> Contacts { get; init; } = [];
}