using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AddressBookAssessment.Services.Web.Auth;

public class ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyOptions> options, IConfiguration config, ILoggerFactory logger, UrlEncoder urlEncoder)
	: AuthenticationHandler<ApiKeyOptions>(options, logger, urlEncoder)
{
	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		await Task.CompletedTask;

		if (!Request.Headers.TryGetValue(Options.HeaderName, out var requestApiKey))
		{
			return AuthenticateResult.NoResult();
		}

		var apiKey = config["ApiKey"];

		if (apiKey == null || requestApiKey != apiKey)
		{
			return AuthenticateResult.NoResult();
		}

		var principal = new ClaimsPrincipal(
				new ClaimsIdentity([
					new Claim(ClaimTypes.NameIdentifier, requestApiKey!),
					new Claim(ClaimTypes.Role, "ADMIN")
				], Scheme.Name)
			);

		return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
	}
}
