using AddressBookAssessment.Core.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AddressBookAssessment.Services.Web.AspNet;

public class StringValueObjectJsonConverter(params Type[] exceptions) : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert)
	{
		if (exceptions.Contains(typeToConvert))
		{
			return false;
		}

		return typeToConvert
			.GetInterfaces()
			.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IStringValueObject<>));
	}

	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var converterType = typeof(StringValueObjectJsonConverter<>)
			.MakeGenericType(typeToConvert);

		return (JsonConverter?)Activator.CreateInstance(converterType);
	}
}

public class StringValueObjectJsonConverter<TValue> : JsonConverter<TValue> where TValue : IStringValueObject<TValue>
{
	private readonly bool _convertEmptyStringToNull;

	public StringValueObjectJsonConverter() : this(true) { }

	public StringValueObjectJsonConverter(bool convertEmptyStringToNull)
	{
		_convertEmptyStringToNull = convertEmptyStringToNull;
	}

	public override TValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var stringValue = reader.GetString();
		if (stringValue == null || _convertEmptyStringToNull && string.IsNullOrWhiteSpace(stringValue))
		{
			return default!;
		}

		if (TValue.TryCreate(stringValue, out var typedValue, out var errorMessage))
		{
			return typedValue;
		}
		else
		{
			throw new JsonException(errorMessage);
		}
	}

	public override void Write(Utf8JsonWriter writer, TValue? typedValue, JsonSerializerOptions options)
	{
		if (typedValue == null)
		{
			writer.WriteNullValue();
			return;
		}

		var value = typedValue.Value;
		if (value == null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value);
	}
}