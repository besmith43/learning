#addin nuget:?package=Cake.Docker
#addin nuget:?package=Cake.Electron.Net

var target = Argument("target", "Default");
var DebugConfiguration = Argument("configuration", "Debug");
var ReleaseConfiguration = Argument("configuration", "Release");
var solutionFolder = "./src";
var consoleProjFile = "./src/console-interface";
var pwshProjFile = "./src/powershell-interface";
var electronProjFile = "./src/electron-interface";
var outputFolder = "./output";
var selfcontainedOutputFolder = $"{ outputFolder }/self-contained";
var dependentOutputFolder = $"{ outputFolder }/framework-dependent";

Task("Clean")
    .Does(() => {
        CleanDirectory(outputFolder);
    });

Task("Version")
    .IsDependentOn("Clean")
    .Does(() => {
        var propsFile = "./src/Directory.Build.props";
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

        Information(version);
    });

Task("Major-Release")
    .Does(() => {
        var propsFile = "./src/Directory.Build.props";
        var readedVersion = XmlPeek(propsFile, "//Version");
        var currentVersion = new Version(readedVersion);

        var semVersion = new Version(currentVersion.Major + 1, 0, 0);
        var version = semVersion.ToString();

        XmlPoke(propsFile, "//Version", version);
    });

Task("Restore")
    .IsDependentOn("Version")
    .Does(() => {
        DotNetCoreRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings{
            Configuration = DebugConfiguration,
            NoRestore = true
        });
    });

Task("Dotnet-Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings{
            Configuration = DebugConfiguration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Run-ConsoleAdd")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
		var arguments = new ProcessArgumentBuilder();
		arguments.Append("add");
		arguments.Append("2");
		arguments.Append("5");
        DotNetCoreRun(consoleProjFile, arguments, new DotNetCoreRunSettings{
            Configuration = DebugConfiguration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Run-ConsoleSub")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
		var arguments = new ProcessArgumentBuilder();
		arguments.Append("sub");
		arguments.Append("8");
		arguments.Append("5");
        DotNetCoreRun(consoleProjFile, arguments, new DotNetCoreRunSettings{
            Configuration = DebugConfiguration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Run-ConsoleMul")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
		var arguments = new ProcessArgumentBuilder();
		arguments.Append("mul");
		arguments.Append("2");
		arguments.Append("5");
        DotNetCoreRun(consoleProjFile, arguments, new DotNetCoreRunSettings{
            Configuration = DebugConfiguration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Run-ConsoleDiv")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
		var arguments = new ProcessArgumentBuilder();
		arguments.Append("div");
		arguments.Append("10");
		arguments.Append("5");
        DotNetCoreRun(consoleProjFile, arguments, new DotNetCoreRunSettings{
            Configuration = DebugConfiguration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Run-PowerShell")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        var callerInfo = Context.GetCallerInfo();
        var pwd = callerInfo.SourceFilePath.ToString();
        pwd = pwd.Remove(pwd.Length - 11);

        DockerRun(new DockerContainerRunSettings{
            Volume = new string[] { $"{ pwd }/src/powershell-interface/bin/Debug/netstandard2.0:/root", $"{ pwd }/test:/scripts" }
        }, "mcr.microsoft.com/powershell", "pwsh", "-F /scripts/test.ps1");
    });

Task("Run-Electron")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        ElectronNetStart(electronProjFile);
    });

Task("Run")
	.IsDependentOn("Run-ConsoleAdd")
	.IsDependentOn("Run-ConsoleSub")
	.IsDependentOn("Run-ConsoleMul")
	.IsDependentOn("Run-ConsoleDiv")
    .IsDependentOn("Run-PowerShell")
    .IsDependentOn("Run-Electron");

Task("Publish-Win-x64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ selfcontainedOutputFolder }/win-x64",
            PublishSingleFile = true,
            SelfContained = true,
            Runtime = "win-x64"
        });
    });

Task("Publish-Linux-x64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ selfcontainedOutputFolder }/linux-x64",
            PublishSingleFile = true,
            SelfContained = true,
            Runtime = "linux-x64"
        });
    });

Task("Publish-Osx-x64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ selfcontainedOutputFolder }/osx-x64",
            PublishSingleFile = true,
            SelfContained = true,
            Runtime = "osx-x64"
        });
    });

Task("Publish-Win-arm64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ selfcontainedOutputFolder }/win-arm64",
            PublishSingleFile = true,
            SelfContained = true,
            Runtime = "win-arm64"
        });
    });

Task("Publish-Win-x86")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ selfcontainedOutputFolder }/win-x86",
            PublishSingleFile = true,
            SelfContained = true,
            Runtime = "win-x86"
        });
    });

Task("Publish-Dependent-Win-x64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ dependentOutputFolder }/win-x64",
            PublishSingleFile = true,
            SelfContained = false,
            Runtime = "win-x64"
        });
    });

Task("Publish-Dependent-Linux-x64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ dependentOutputFolder }/linux-x64",
            PublishSingleFile = true,
            SelfContained = false,
            Runtime = "linux-x64"
        });
    });

Task("Publish-Dependent-Osx-x64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ dependentOutputFolder }/osx-x64",
            PublishSingleFile = true,
            SelfContained = false,
            Runtime = "osx-x64"
        });
    });

Task("Publish-Dependent-Win-arm64")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ dependentOutputFolder }/win-arm64",
            PublishSingleFile = true,
            SelfContained = false,
            Runtime = "win-arm64"
        });
    });

Task("Publish-Dependent-Win-x86")
    .IsDependentOn("Dotnet-Test")
    .Does(() => {
        DotNetCorePublish(consoleProjFile, new DotNetCorePublishSettings{
            Configuration = ReleaseConfiguration,
            OutputDirectory = $"{ dependentOutputFolder }/win-x86",
            PublishSingleFile = true,
            SelfContained = false,
            Runtime = "win-x86"
        });
    });

Task("Docker-Test")
    .IsDependentOn("Dotnet-Test")
    .IsDependentOn("Publish-Linux-x64")
    .Does(() => {
        var callerInfo = Context.GetCallerInfo();
        var pwd = callerInfo.SourceFilePath.ToString();
        pwd = pwd.Remove(pwd.Length - 11);

        DockerRun(new DockerContainerRunSettings{
            Volume = new string[] { $"{ pwd }/output/self-contained/linux-x64:/root", $"{ pwd }/test:/scripts" }
        }, "mcr.microsoft.com/powershell", "pwsh", "-F /scripts/test.ps1");
    });

Task("Test")
    .IsDependentOn("Docker-Test");

Task("Publish")
    .IsDependentOn("Publish-Win-x64")
    .IsDependentOn("Publish-Linux-x64")
    .IsDependentOn("Publish-Osx-x64")
    .IsDependentOn("Publish-Win-arm64")
    .IsDependentOn("Publish-Win-x86")
    .IsDependentOn("Publish-Dependent-Win-x64")
    .IsDependentOn("Publish-Dependent-Linux-x64")
    .IsDependentOn("Publish-Dependent-Osx-x64")
    .IsDependentOn("Publish-Dependent-Win-arm64")
    .IsDependentOn("Publish-Dependent-Win-x86");

Task("Default")
    .IsDependentOn("Run");

RunTarget(target);
