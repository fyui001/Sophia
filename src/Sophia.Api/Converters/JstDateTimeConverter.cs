namespace Sophia.Api.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class JstDateTimeConverter : JsonConverter<DateTime>
{
    private static readonly TimeZoneInfo JstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDateTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var jstTime = TimeZoneInfo.ConvertTimeFromUtc(
            value.Kind == DateTimeKind.Utc ? value : value.ToUniversalTime(),
            JstTimeZone);

        var dto = new DateTimeOffset(jstTime, JstTimeZone.BaseUtcOffset);
        writer.WriteStringValue(dto.ToString("yyyy-MM-dd'T'HH:mm:sszzz"));
    }
}
