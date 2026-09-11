using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace AddressBookAssessment.Services.Web.OpenApi;

public static class OpenApiExtensions
{
	public static void ConfigureOpenApi(this WebApplicationBuilder builder)
	{
		builder.Services.AddHttpContextAccessor();

		builder.Services.AddTransient<DynamicServerTransformer>();

		builder.Services.AddOpenApi(options =>
		{
			options.AddDocumentTransformer<DynamicServerTransformer>();
			options.AddDocumentTransformer((document, context, cancellationToken) =>
			{
				var scheme = new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.ApiKey,
					Name = "X-Api-Key",
					In = ParameterLocation.Header,
					Description = "API Key"
				};

				document.Components!.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

				document.Components.SecuritySchemes!.Add("ApiKey", scheme);

				return Task.CompletedTask;
			});
			options.AddExamplesTransformer();
		});
	}

	public static void MapOpenApiEndpoints(this WebApplication app)
	{
		app.MapOpenApi();

		app.MapScalarApiReference(options =>
		{
			options.AddDocument("v1", "AddressBook Assessment API")
				.WithTitle("Address Book Assessment | API Documentation")
				.WithTheme(ScalarTheme.BluePlanet)
				.WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.Http11)
				.AddPreferredSecuritySchemes("ApiKey");
		});
	}

	private static void AddExamplesTransformer(this OpenApiOptions options)
	{
		options.AddOperationTransformer(async (operation, context, cancellationToken) =>
		 {
			 foreach (var example in context.Description.ActionDescriptor.EndpointMetadata.OfType<IOpenApiOperationTransformer>())
			 {
				 await example.TransformAsync(operation, context, cancellationToken);
			 }
		 });
	}

	public static RouteHandlerBuilder WithExample<T>(this RouteHandlerBuilder builder, string name, T example)
	{
		return builder.WithMetadata(new OpenApiExampleMetadata<T>(name, example));
	}

	public static RouteHandlerBuilder WithExample<T>(this RouteHandlerBuilder builder, string name, string json)
	{
		return builder.WithMetadata(new OpenApiExampleMetadata(name, json));
	}
}
