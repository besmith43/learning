using OneOf;


OneOf<int, string> value = 1;

// for when you aren't returning a value
value.Switch(
    intVal => Console.WriteLine($"intVal = {intVal}"),
    stringVal => Console.WriteLine($"stringVal = {stringVal}")
);


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
