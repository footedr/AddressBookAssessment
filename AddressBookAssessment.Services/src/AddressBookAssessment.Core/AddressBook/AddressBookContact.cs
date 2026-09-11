namespace AddressBookAssessment.Core.AddressBook;

public record AddressBookContact
{
	public required string Name { get; set; }
	public PhoneNumber? OfficePhoneNumber { get; set; }
	public PhoneNumber? CellPhoneNumber { get; set; }
	public EmailAddress? EmailAddress { get; set; }
	public bool IsPrimary { get; set; }
}