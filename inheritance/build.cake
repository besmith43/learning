var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	DotNetClean("./DataStoreExample/DataStoreExample.csproj", new DotNetCleanSettings
	{
		Configuration = configuration
	});

	DotNetClean("./DataStoreExample.Test/DataStoreExample.Test.csproj", new DotNetCleanSettings
	{
        Configuration = configuration
	});
});

Task("Build")
        .Does(() =>
{
        DotNetBuild("./DataStoreExample/DataStoreExample.csproj", new DotNetBuildSettings
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
        DotNetTest("./DataStoreExample.Test/DataStoreExample.Test.csproj", new DotNetTestSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal
        });
});

Task("Pack")
    .Does(() =>
{
    DotNetPack("./DataStoreExample/DataStoreExample.csproj", new DotNetPackSettings
    {
        Configuration = "Release",
        NoRestore = false,
        NoLogo = true,
        Verbosity = DotNetVerbosity.Minimal,
        OutputDirectory = "./artifacts"
    });

    var nugetFile = GetFiles("./artifacts/DataStoreExample*.nupkg").Single();

    DotNetNuGetPush(nugetFile, new DotNetNuGetPushSettings
    {
        Source = "https://easappp01.tntech.edu/BaGet/v3/index.json"
    });

    DeleteFile(nugetFile);
});

Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Test");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument

