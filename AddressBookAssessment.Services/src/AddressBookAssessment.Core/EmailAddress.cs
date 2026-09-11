using AddressBookAssessment.Core.Shared;
using System.Diagnostics.CodeAnalysis;

namespace AddressBookAssessment.Core;

/// <example>jbelfort@oakmont-stratton.com</example>
public record EmailAddress : StringValueObject<EmailAddress>, IStringValueObject<EmailAddress>
{
	private EmailAddress(string value) : base(value) { }

	public static bool TryCreate(string? value, [NotNullWhen(returnValue: true)] out EmailAddress? emailAddress, [NotNullWhen(returnValue: false)] out string? errorMessage)
	{
		if (value == null || !value.Contains('@'))
		{
			emailAddress = null;
			errorMessage = $"Invalid email address: {value ?? "NULL"}";
			return false;
		}

		emailAddress = new EmailAddress(value);
		errorMessage = null;
		return true;
	}
}