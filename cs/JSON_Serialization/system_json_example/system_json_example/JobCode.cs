using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace system_json_example;

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
public class JobCode
{
    [JsonPropertyName("jobCode")]
    public string Job_Code { get; set; }
    [JsonPropertyName("jobTitle")]
    public string Job_Title { get; set; }
    [JsonPropertyName("active")]
    [JsonConverter(typeof(EverythingToStringJsonConverter))]
    public string Active { get; set; }
}

public class JobCodeEqualityComparison : IEqualityComparer<JobCode>
{
    public bool Equals(JobCode x, JobCode y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        return x.Job_Code == y.Job_Code && x.Job_Title == y.Job_Title;
    }

    public int GetHashCode(JobCode obj)
    {
        return obj.Job_Code.GetHashCode();
    }
}

public class EverythingToStringJsonConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {

        if (reader.TokenType == JsonTokenType.String)
        {
            return reader.GetString() ?? String.Empty;
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            var stringValue = reader.GetDouble();
            return stringValue.ToString();
        }
        else if (reader.TokenType == JsonTokenType.False ||
            reader.TokenType == JsonTokenType.True)
        {
            return reader.GetBoolean().ToString();
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            reader.Skip();
            return "(not supported)";
        }
        else
        {
            Console.WriteLine($"Unsupported token type: {reader.TokenType}");

            throw new System.Text.Json.JsonException();
        }
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
