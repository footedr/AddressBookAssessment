namespace AddressBookAssessment.Services.Web.Auth;

public static class AuthExtensions
{
	public static void ConfigureAuth(this WebApplicationBuilder builder)
	{
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = ApiKeyOptions.DefaultScheme;
			options.DefaultChallengeScheme = ApiKeyOptions.DefaultScheme;
		})
			.AddScheme<ApiKeyOptions, ApiKeyAuthenticationHandler>(ApiKeyOptions.DefaultScheme, options =>
			{
				options.HeaderName = "X-API-Key";
			});

		builder.Services.AddAuthorization();
	}
}
