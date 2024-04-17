var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	CleanDirectory("./.build");
	CleanDirectory("./.publish");

	DotNetClean("./system_json_example/system_json_example.csproj");
});

Task("Build")
	.Does(() =>
{
	DotNetBuild("./system_json_example/system_json_example.csproj", new DotNetBuildSettings
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
	DotNetExecute("./.build/system_json_example.dll");
});


Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Run");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument

