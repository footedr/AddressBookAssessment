using Microsoft.AspNetCore.Authentication;

namespace AddressBookAssessment.Services.Web.Auth;

public class ApiKeyOptions : AuthenticationSchemeOptions
{
	public const string DefaultScheme = "ApiKey";
	public string HeaderName { get; set; } = "X-API-Key";
}