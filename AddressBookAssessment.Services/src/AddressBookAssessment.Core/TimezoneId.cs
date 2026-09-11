using AddressBookAssessment.Core.Shared;
using System.Diagnostics.CodeAnalysis;

namespace AddressBookAssessment.Core;

public record TimezoneId : StringValueObject<TimezoneId>, IStringValueObject<TimezoneId>
{
	private static string[] ValidTimezoneIds => ["America/New_York", "America/Chicago", "America/Denver", "America/Phoenix", "America/Los_Angeles", "America/Anchorage", "America/Adak", "America/Halifax", "America/Puerto_Rico", "America/St_Johns", "Pacific/Honolulu", "Pacific/Pago_Pago", "Pacific/Guam"];

	private TimezoneId(string value) : base(value)
	{

	}

	public static bool TryCreate(string? value, [NotNullWhen(true)] out TimezoneId? timezoneId, [NotNullWhen(false)] out string? errorMessage)
	{
		if (value == null || !ValidTimezoneIds.Contains(value))
		{
			timezoneId = default;
			errorMessage = $"Unsupported time zone id: {value ?? "NULL"}";
			return false;
		}

		timezoneId = new TimezoneId(value);
		errorMessage = null;
		return true;
	}
}
