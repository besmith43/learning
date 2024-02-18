var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
        .Does(() =>
{
        CleanDirectory("./.build");

        DotNetClean("./Express/Express.csproj", new DotNetCleanSettings
        {
                Configuration = configuration,
                OutputDirectory = "./.build/Express"
        });

        CleanDirectory("./.publish");

        DotNetClean("./Express/Express.csproj", new DotNetCleanSettings
        {
                Configuration = "Release",
                OutputDirectory = "./.publish"
        });
});

Task("Nuget")
	.Does(() =>
{
	Information("Nuget Source Exists: " + DotNetNuGetHasSource("https://easappp01.tntech.edu/BaGet/v3/index.json"));

	if (!DotNetNuGetHasSource("https://easappp01.tntech.edu/BaGet/v3/index.json"))
	{
		 DotNetNuGetAddSource("https://easappp01.tntech.edu/BaGet/v3/index.json", new DotNetNuGetAddSourceSettings
		 {
			 Source = "https://easappp01.tntech.edu/BaGet/v3/index.json"
		 });
	}
});

Task("Build")
    .IsDependentOn("Nuget")
        .Does(() =>
{
        DotNetBuild("./Express/Express.csproj", new DotNetBuildSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal,
                OutputDirectory = "./.build"
        });
});

Task("Test")
        .Does(() =>
{
        DotNetTest("./Express.Test/Express.Test.csproj", new DotNetTestSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal,
        });
});

Task("Run")
        .Does(() =>
{
        DotNetRun("./Express/Express.csproj", new DotNetRunSettings
        {
                Configuration = configuration,
                Verbosity = DotNetVerbosity.Minimal,
        });
});


Task("Publish")
    .IsDependentOn("Clean")
    .IsDependentOn("Nuget")
	.Does(() =>
{
	DotNetPublish("./Express/Express.csproj", new DotNetPublishSettings
	{
		Configuration = "Release",
		NoRestore = false,
		NoLogo = true,
		Runtime = "win-x64",
		Verbosity = DotNetVerbosity.Minimal,
		OutputDirectory = "./.publish"
	});
});


Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Test")
	.IsDependentOn("Run");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument
