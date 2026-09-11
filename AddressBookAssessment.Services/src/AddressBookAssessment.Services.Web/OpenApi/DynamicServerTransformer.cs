using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace AddressBookAssessment.Services.Web.OpenApi;

public class DynamicServerTransformer : IOpenApiDocumentTransformer
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public DynamicServerTransformer(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
	{
		var httpContext = _httpContextAccessor.HttpContext;

		// Safety guard for build-time generation tools or out-of-bounds requests
		if (httpContext == null)
		{
			return Task.CompletedTask;
		}

		var pathBase = httpContext.Request.Headers["X-Forwarded-PathBase"].FirstOrDefault() ?? string.Empty;

		document.Servers!.Clear();
		document.Servers.Add(new()
		{
			Url = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{pathBase}"
		});

		return Task.CompletedTask;
	}
}
