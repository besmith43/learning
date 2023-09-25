using System.Runtime.Caching;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

object lockObject = new object();
int currentTerm = 0;
DateTime dtLastUpdateTerm = DateTime.Now;


int counter = 0;
DateTime dateTime = DateTime.Now;

app.MapGet("/counter", () => {

    if (MemoryCache.Default["counter"] == null)
    {
        MemoryCache.Default["counter"] = 0;
    }
    else
    {
        counter = (int)MemoryCache.Default["counter"];
    }

    if (dateTime.AddSeconds(10) < DateTime.Now)
    {
        counter++;
        MemoryCache.Default["counter"] = counter;
        dateTime = DateTime.Now;
    }

    return $"Counter: {counter}";
});


app.MapGet("/reset", () => {
    MemoryCache.Default["counter"] = 0;
    return "Counter reseted";
});


app.MapGet("/currentTerm", () => {
    if (dtLastUpdateTerm.AddSeconds(10) < DateTime.Now)
    {
        lock(lockObject)
        {
            currentTerm++;
            dtLastUpdateTerm = DateTime.Now;
        }
    }

    var result = $"Current Term: {currentTerm}\nLast Updated: {dtLastUpdateTerm}\nAPI Response: {DateTime.Now}\n";
    return result;
});


app.Run();
