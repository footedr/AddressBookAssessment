namespace AddressBookAssessment.Core;

public record Address
{
	/// <example>6450 Poe Ave</example>
	public string? Line1 { get; init; }

	/// <example>Suite 400</example>
	public string? Line2 { get; init; }

	/// <example></example>
	public string? Line3 { get; init; }

	/// <example>Dayton</example>
	public required string City { get; init; }

	/// <example>OH</example>
	public required StateAbbreviation StateAbbreviation { get; init; }

	/// <example>45385</example>
	public required PostalCode PostalCode { get; init; }

	public required CountryAbbreviation CountryAbbreviation { get; init; } = CountryAbbreviation.US;
}