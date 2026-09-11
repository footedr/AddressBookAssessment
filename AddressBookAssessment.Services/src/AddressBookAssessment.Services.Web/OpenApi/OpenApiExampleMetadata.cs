using AddressBookAssessment.Services.Web.AspNet;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AddressBookAssessment.Services.Web.OpenApi;

public class OpenApiExampleMetadata(string name, string rawValue) : IOpenApiOperationTransformer
{
	public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
	{
		if (operation.RequestBody?.Content?.TryGetValue(MediaTypeNames.Application.Json, out var mediaType) == true)
		{
			mediaType!.Examples ??= new Dictionary<string, IOpenApiExample>();
			mediaType.Examples.Add(name, new OpenApiExample
			{
				Description = name,
				Summary = name,
				Value = JsonNode.Parse(rawValue)
			});
		}

		return Task.CompletedTask;
	}
}

public class OpenApiExampleMetadata<T>(string name, T value) : IOpenApiOperationTransformer
{
	public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
	{
		if (operation.RequestBody?.Content?.TryGetValue(MediaTypeNames.Application.Json, out var mediaType) == true)
		{
			var jsonOptions = new JsonSerializerOptions().ConfigureSerializerOptions();
			jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			jsonOptions.WriteIndented = true;

			var example = typeof(T) == typeof(string)
				? JsonValue.Create(value)
				: JsonNode.Parse(JsonSerializer.Serialize(value, jsonOptions));

			mediaType!.Examples ??= new Dictionary<string, IOpenApiExample>();
			mediaType.Examples.Add(name, new OpenApiExample
			{
				Description = name,
				Summary = name,
				Value = example
			});
		}

		return Task.CompletedTask;
	}
}
