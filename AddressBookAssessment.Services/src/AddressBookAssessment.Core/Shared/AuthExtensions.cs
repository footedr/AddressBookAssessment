using System.Security.Claims;

namespace AddressBookAssessment.Core.Shared;

public static class AuthExtensions
{
	public static bool IsAdmin(this ClaimsPrincipal principal)
	{
		return principal.HasClaim(c =>
			c.Type == ClaimTypes.Role &&
			c.Value == "ADMIN");
	}

	public static void AssertIsAdmin(this ClaimsPrincipal principal)
	{
		if (!principal.IsAdmin())
		{
			throw new ForbiddenAccessException("User is not an administrator.");
		}
	}
}
