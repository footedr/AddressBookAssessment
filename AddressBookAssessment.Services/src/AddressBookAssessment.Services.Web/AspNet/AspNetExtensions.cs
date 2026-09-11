using AddressBookAssessment.Core.Shared;
using Microsoft.AspNetCore.Http.Json;
using System.Data;
using System.Text.Json;

namespace AddressBookAssessment.Services.Web.AspNet;

public static class AspNetExtensions
{
	// Note: We typically put this in a separate project (ProjectName.Services.AspNet)

	public static void ConfigureAspNet(this WebApplicationBuilder builder)
	{
		builder.Services.Configure<JsonOptions>(options =>
		{
			options.SerializerOptions.ConfigureSerializerOptions();
		});

		// This was necessary to get SwaggerGen to document enum types as strings rather than ints
		// https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1269#issuecomment-1044868195
		builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options => options.JsonSerializerOptions.ConfigureSerializerOptions());

		builder.Services.AddProblemDetails(options =>
		{
			options.MapExceptionToStatusCode<NotFoundException>(StatusCodes.Status404NotFound);
			options.MapExceptionToStatusCode<InvalidOperationException>(StatusCodes.Status400BadRequest);
			options.MapExceptionToStatusCode<ForbiddenAccessException>(StatusCodes.Status403Forbidden);
			options.MapExceptionToStatusCode<FormatException>(StatusCodes.Status422UnprocessableEntity);
			options.MapExceptionToStatusCode<DuplicateNameException>(StatusCodes.Status422UnprocessableEntity);
			options.MapExceptionToStatusCode<JsonException>(StatusCodes.Status400BadRequest);
		});

		builder.Services.AddHttpContextAccessor();
	}
}
