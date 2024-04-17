using System.Reflection;


var query = Get("scratchpad.sql");

Console.WriteLine("scratchpad: " + query);

var query2 = Get("log.sql");

Console.WriteLine("log: " + query2);

var error = new ErrorMessage
{
    Message = "test",
    StackTrace = "test"
};


string Get(string name)
{
    try
    {

    var assembly = typeof(Program).Assembly;
    var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(name));

    using var stream = assembly.GetManifestResourceStream(resourceName);
    using var reader = new System.IO.StreamReader(stream);

    return reader.ReadToEnd();
    }
    catch (System.Exception ex)
    {
        Console.WriteLine(ex);
        return "";
    }
}

class ErrorMessage
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
}