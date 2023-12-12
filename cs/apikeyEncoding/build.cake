var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	CleanDirectory("./.build");
	CleanDirectory("./.publish");
});

Task("Build")
	.Does(() =>
{
	DotNetBuild("./apikeyEncoding/apikeyEncoding.csproj", new DotNetBuildSettings
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
	DotNetExecute("./.build/apikeyEncoding.dll");
});


Task("Publish")
	.Does(() =>
{
	DotNetPublish("./apikeyEncoding/apikeyEncoding.csproj", new DotNetPublishSettings
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

