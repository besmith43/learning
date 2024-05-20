var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	CleanDirectory("./.build");

	DotNetClean("./WAN/WAN.csproj", new DotNetCleanSettings{
		Configuration = configuration,
		OutputDirectory = "./.build/"
	});
	DotNetClean("./WAN.Test/WAN.Test.csproj", new DotNetCleanSettings{
		Configuration = configuration,
		OutputDirectory = "./.build/"
	});

	CleanDirectory("./.publish");

	DotNetClean("./WAN/WAN.csproj", new DotNetCleanSettings{
		Configuration = "Release",
		OutputDirectory = "./.publish/"
	});
	DotNetClean("./WAN.Test/WAN.Test.csproj", new DotNetCleanSettings{
		Configuration = "Release",
		OutputDirectory = "./.publish/"
	});
});

Task("Build")
	.Does(() =>
{
	DotNetBuild("./WAN/WAN.csproj", new DotNetBuildSettings
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
		DotNetTest("./WAN.Test/WAN.Test.csproj", new DotNetTestSettings
		{
			Configuration = configuration,
			NoRestore = false,
			NoLogo = true,
			Verbosity = DotNetVerbosity.Minimal,
			OutputDirectory = "./.build/"
		});
	});

Task("Run")
		.Does(() =>
{
	DotNetExecute("./.build/WAN.dll", "-o");
});

Task("Publish")
	.IsDependentOn("Clean")
	.Does(() =>
{
	DotNetPublish("./WAN/WAN.csproj", new DotNetPublishSettings
	{
		Configuration = "Release",
		NoRestore = false,
		NoLogo = true,
		Runtime = "win-x64",
		SelfContained = true,
		PublishSingleFile = true,
		Verbosity = DotNetVerbosity.Minimal,
		OutputDirectory = "./.publish/"
	});
});


Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Run");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument
