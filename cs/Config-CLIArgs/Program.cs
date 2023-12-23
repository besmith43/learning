using Microsoft.Extensions.Configuration;

var mappings = new Dictionary<string, string>
{
    { "--output", "output" }
};
IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("config.json")
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true)
    .AddCommandLine(args, mappings)
    .Build();



var output = configuration["output"];


Console.WriteLine($"Output: {output}");