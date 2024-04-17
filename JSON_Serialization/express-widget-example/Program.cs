using System.Text.Json;
using System.Text.Json.Serialization;
using Express;

var widgetsJson = File.ReadAllText("availableWidgets.json");

var options = new JsonSerializerOptions();
options.WriteIndented = true;
options.Converters.Add(new ObjectToInferredTypesConverter());
var widgets = JsonSerializer.Deserialize<List<Widget>>(widgetsJson, options);

foreach (var widget in widgets)
{
    Console.WriteLine($"{widget.WidgetID} - {widget.ReadableTitle}");
}


public class ObjectToInferredTypesConverter : JsonConverter<object>
{
    public override object Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) => reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.Number when reader.TryGetInt64(out long l) => l,
            JsonTokenType.Number => reader.GetDouble(),
            JsonTokenType.String when reader.TryGetDateTime(out DateTime datetime) => datetime,
            JsonTokenType.String => reader.GetString()!,
            _ => JsonDocument.ParseValue(ref reader).RootElement.Clone()
        };

    public override void Write(
        Utf8JsonWriter writer,
        object objectToWrite,
        JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, objectToWrite, objectToWrite.GetType(), options);
}
