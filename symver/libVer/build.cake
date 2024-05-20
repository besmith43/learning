var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("Clean")
	.Does(() =>
{
	CleanDirectory("./.build");
	CleanDirectory("./.publish");
	// CleanDirectory("./artifacts");
	DotNetClean("./libVer/libVer.csproj", new DotNetCleanSettings
	{
		Configuration = configuration,
		OutputDirectory = "./.build/"
	});
	DotNetClean("./libVer/libVer.csproj", new DotNetCleanSettings
	{
		Configuration = "Release",
		OutputDirectory = "./.publish/"
	});
});

Task("Build")
        .Does(() =>
{
        DotNetBuild("./libVer/libVer.csproj", new DotNetBuildSettings
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
	DotNetTest("./libVer.test/libVer.test.csproj", new DotNetTestSettings
	{
            Configuration = configuration
	});
});

Task("UpVersion")
	.Does(() =>
{
        var propsFile = "./Directory.Build.props";
        var readedVersion = XmlPeek(propsFile, "//Version");
        var currentVersion = new Version(readedVersion);
        var newMinor = currentVersion.Minor;

        if (target == "publish")
        {
            newMinor++;
        }

        var semVersion = new Version(currentVersion.Major, newMinor, currentVersion.Build + 1);
        var version = semVersion.ToString();

        XmlPoke(propsFile, "//Version", version);


});

Task("Major-Release")
    .Does(() =>
{
        var propsFile = "./Directory.Build.props";
        var readedVersion = XmlPeek(propsFile, "//Version");
        var currentVersion = new Version(readedVersion);

        var semVersion = new Version(currentVersion.Major + 1, 0, 0);
        var version = semVersion.ToString();

        XmlPoke(propsFile, "//Version", version);
});

Task("Pack")
	.IsDependentOn("Clean")
	.IsDependentOn("UpVersion")
	.Does(() =>
{
	DotNetPack("./libVer/libVer.csproj", new DotNetPackSettings
    {
        Configuration = "Release",
        NoRestore = false,
        NoLogo = true,
        Verbosity = DotNetVerbosity.Minimal,
        OutputDirectory = "./artifacts"
    });
});


Task("Default")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Test");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument

