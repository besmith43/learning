var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	DotNetClean("./datastore/datastore.csproj", new DotNetCleanSettings
	{
		Configuration = configuration
	});

	DotNetClean("./datastore.test/datastore.test.csproj", new DotNetCleanSettings
	{
        Configuration = configuration
	});
});

Task("Build")
        .Does(() =>
{
        DotNetBuild("./datastore/datastore.csproj", new DotNetBuildSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal
        });
});

Task("Test")
        .Does(() =>
{
        DotNetTest("./datastore.test/datastore.test.csproj", new DotNetTestSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal
        });
});


Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Test");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument

