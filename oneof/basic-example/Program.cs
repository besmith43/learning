using OneOf;

Console.WriteLine($"Version: { System.Reflection.Assembly.GetExecutingAssembly().GetName().Version }");

OneOf<int, string> value = 1;

// for when you aren't returning a value
value.Switch(
    intVal => Console.WriteLine($"intVal = {intVal}"),
    stringVal => Console.WriteLine($"stringVal = {stringVal}")
);

if (value is int)
{
    Console.WriteLine($"intVal = {value}");
}
else if (value is string)
{
    Console.WriteLine($"stringVal = {value}");
}


Console.WriteLine($"match value: {value.Match(intVal => intVal.ToString(), stringVal => stringVal)}");

value = "hello2";

Console.WriteLine($"match value: {value.Match(intVal => intVal.ToString(), stringVal => stringVal)}");


Console.WriteLine($"DoThing(true) = {DoThing(true)}");

Console.WriteLine($"DoThing(false) = {DoThing(false)}");

OneOf<int, string> DoThing(bool noIdea)
{
    if (noIdea)
    {
        return 1;
    }
    else
    {
        return "hello";
    }
}


// see this for info: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving

int? testy = null;

// Console.WriteLine($"testy.HasValue = {testy.HasValue}");
Console.WriteLine($"testy = {testy!}"); // don't do this.  it's the null-forgiving operator.  it will tell the compiler to ignore nulls and not generate a warning that this code could contain a null value
Console.WriteLine($"testy = {testy}"); // this will generate a warning that the value could be null

string testing = "hello";
testing.ToLower();



List<int>? numbers = null;


(numbers ??= new List<int>()).Add(5);

if (numbers is null)
{
    Console.WriteLine("numbers is null");
}
else
{
    Console.WriteLine("numbers is not null");
}

// this is a way to use tuples to mimic the behavior of the OneOf type
// look at golang's is err nil pattern for the inspiration
Tuple<string?, string?> TupleFunc()
{
    return new Tuple<string?, string?>("hello", null);
}

var tupleResult = TupleFunc();

if (tupleResult.Item2 is not null)
{
    Console.WriteLine("an error was encountered");
}
else
{
    Console.WriteLine("function was successful");
}