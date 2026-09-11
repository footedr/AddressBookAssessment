using System.Diagnostics;

namespace AddressBookAssessment.Core;

public class ActivitySources
{
	private static readonly List<ActivitySource> _all = new();

	static ActivitySources()
	{
		RegisterSource(Default);
	}

	public static void RegisterSource(ActivitySource source)
	{
		if (_all.Contains(source))
		{
			return;
		}

		_all.Add(source);
	}

	public static IReadOnlyList<ActivitySource> All => _all.AsReadOnly();
	public static readonly ActivitySource Default = new("AddressBookAssessment.Services");
}
