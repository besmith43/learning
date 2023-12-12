using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

string strAPIKey = configuration["apiKey"];
string strAPIAuth = configuration["apiAuth"];

var authenticationString = $"apiuser:{strAPIKey}";

var finalString = "YXBpdXNlcjpYKjBnaFBhMzQ=";


Console.WriteLine("Expected: Basic YXBpdXNlcjpYKjBnaFBhMzQ=");

Console.WriteLine($"API Auth Secret Value: {strAPIAuth}");

string attempt1 = Base64Encode(authenticationString);


Console.WriteLine($"Acutal: Basic {attempt1}");
Console.WriteLine($"Attempt 1: {finalString == attempt1}");

var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

Console.WriteLine($"Acutal Second Method: Basic {base64EncodedAuthenticationString}");
Console.WriteLine($"Attempt 2: {finalString == base64EncodedAuthenticationString}");

var attempt3 = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

Console.WriteLine($"Acutal Third Method: Basic {attempt3}");
Console.WriteLine($"Attempt 3: {finalString == attempt3}");

string Base64Encode(string plainText)
{
    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
    return System.Convert.ToBase64String(plainTextBytes);
}