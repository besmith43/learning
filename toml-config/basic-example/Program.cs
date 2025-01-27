using Tomlyn;
using System.Runtime.Serialization; // this is for the DataMember and IgnoreDataMember attributes

var configFile = "./config.toml";

if (!File.Exists(configFile))
{
    Console.Error.WriteLine("config.toml was not found");
}


var toml = File.ReadAllText(configFile);

var model = Toml.ToModel<MyModel>(toml);
// Prints "this is a string"
Console.WriteLine($"found global = \"{model.Global}\"");
// Prints 1
var key = model.MyTable!.Key;
Console.WriteLine($"found key = {key}");
// Check list
var list = model.MyTable!.ListOfIntegers; // can't call this as .List
Console.WriteLine($"found list = {string.Join(", ", list)}");

// Simple model that maps the TOML string above
class MyModel
{
    public string? Global { get; set; }

    public MyTable? MyTable { get; set; }
}

class MyTable
{
    public MyTable()
    {
        ListOfIntegers = new List<int>();
    }

    public int Key { get; set; }

    public bool Value { get; set; }

    // The type can be an interface if it is pre-instantiated by this instance.
    [DataMember(Name = "list")] // Name = list is for the name of the property in the toml file being called list
    public List<int> ListOfIntegers { get; } // this can't be an IList type

    [IgnoreDataMember]
    public string? ThisPropertyIsIgnored {get; set;}
}

