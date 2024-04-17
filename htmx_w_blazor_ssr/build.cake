var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	DotNetClean("./backend/backend.csproj", new DotNetCleanSettings
	{
		Configuration = configuration
	});
});

Task("Build")
        .Does(() =>
{
        DotNetBuild("./backend/backend.csproj", new DotNetBuildSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal
        });
});

Task("Run")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetRun("./backend/backend.csproj", new DotNetRunSettings
    {
        Configuration = configuration
    });
});

Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Run");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument

