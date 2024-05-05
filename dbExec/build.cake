var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
        .Does(() =>
{
        DotNetClean("./dbExec/dbExec.csproj", new DotNetCleanSettings
        {
                Configuration = configuration
        });

        DotNetClean("./dbExec.Test/dbExec.Test.csproj", new DotNetCleanSettings
        {
        Configuration = configuration
        });
});

Task("Build")
        .Does(() =>
{
        DotNetBuild("./dbExec/dbExec.csproj", new DotNetBuildSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal,
				OutputDirectory = "./.build/"
        });
});

Task("Test")
        .Does(() =>
{
        DotNetTest("./dbExec.Test/dbExec.Test.csproj", new DotNetTestSettings
        {
                Configuration = configuration,
                NoRestore = false,
                NoLogo = true,
                Verbosity = DotNetVerbosity.Minimal
        });
});

Task("Run")
    .Does(() =>
{
	DotNetExecute("./.build/dbExec.dll");
});

Task("Default")
        .IsDependentOn("Clean")
        .IsDependentOn("Build")
        .IsDependentOn("Test")
		.IsDependentOn("Run");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument