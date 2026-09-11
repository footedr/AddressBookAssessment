using System.Text.Json;
using System.Text.Json.Serialization;

namespace AddressBookAssessment.Services.Web.AspNet;

public static class SerializationExtensions
{
	public static JsonSerializerOptions ConfigureSerializerOptions(this JsonSerializerOptions options)
	{
		options.Converters.Add(new JsonStringEnumConverter());
		//options.Converters.Add(new SecretJsonConverter());
		options.Converters.Add(new StringValueObjectJsonConverter());
		//options.Converters.Add(new JsonMergePatchConverter(options));
		//options.Converters.Add(new DateOnlyJsonConverter());
		//options.Converters.Add(new TimeOnlyJsonConverter());
		//options.Converters.Add(new IntValueObjectJsonConverter());
		//options.Converters.Add(new DecimalValueObjectJsonConverter());
		options.PropertyNameCaseInsensitive = true;
		return options;
	}
}
