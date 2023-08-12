var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

System.Environment.SetEnvironmentVariable("idontknow", "this is a test of the emergency broadcast system");


app.MapGet("/", () => {
	return System.Environment.GetEnvironmentVariable("idontknow");
});

app.Run();
