using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace system_json_example;

// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/ignore-properties

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
public class Role
{
    [JsonPropertyName("employeeId")]
    public string EmployeeID { get; set; }
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; set; }
    [JsonPropertyName("namespace")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string NameSpace { get; set; }
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Description { get; set; }
    [JsonPropertyName("activeTo")]
    [JsonConverter(typeof(DateTimeConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime ActiveTo { get; set; }
    [JsonPropertyName("activeFrom")]
    [JsonConverter(typeof(DateTimeConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public DateTime ActiveFrom { get; set; }
    [JsonPropertyName("unitId")]
    public string UnitID { get; set; }
    [JsonPropertyName("id")]
    public string RoleID { get; set; }
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Type { get; set; }
    [JsonPropertyName("descendHierarchy")]
    public string? DescendHierarchy { get; set; }
}

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {

        if (reader.TokenType == JsonTokenType.String)
        {
            if (DateTime.TryParse(reader.GetString(), out DateTime date))
            {
                return date;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        else
        {
            return DateTime.MinValue;
        }
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}
