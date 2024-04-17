#addin nuget:?package=Cake.Electron.Net&version=1.1.0
#tool "nuget:?package=ElectronNet.CLI&version=9.31.2"

using Cake.Electron.Net;
using Cake.Electron.Net.Commands.Settings;

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./src";
var projFile = "./src/fixingCakeTool";
var outputFolder = "./output";
var PublishReadyToRunOSX = true;
var PublishReadyToRunWin = true;
var PublishReadyToRunLinux = true;

if (IsRunningOnLinux())
{
	PublishReadyToRunWin = false;
	PublishReadyToRunLinux = true;
	PublishReadyToRunOSX = false;
}
else if (IsRunningOnMacOs())
{
	PublishReadyToRunWin = false;
	PublishReadyToRunLinux = false;
	PublishReadyToRunOSX = true;
}
else if (IsRunningOnWindows())
{
	PublishReadyToRunWin = true;
	PublishReadyToRunLinux = false;
	PublishReadyToRunOSX = false;
}
else
{
	PublishReadyToRunWin = false;
	PublishReadyToRunLinux = false;
	PublishReadyToRunOSX = false;
}

Task("Init")
    .Does(() => {
        ElectronNetInit(projFile);
    });

Task("Clean")
	.Does(() => {
		CleanDirectory(outputFolder);
		CleanDirectory($"{ outputFolder }/win-x64");
		CleanDirectory($"{ outputFolder }/win-x86");
		CleanDirectory($"{ outputFolder }/win-arm64");
		CleanDirectory($"{ outputFolder }/osx-x64");
		CleanDirectory($"{ outputFolder }/linux-x64");
		CleanDirectory($"{ outputFolder }/linux-arm");
	});

Task("Version")
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

Task("Test")
    .IsDependentOn("Version")
    .Does(() => {
        DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings{
            Configuration = configuration
        });
    });

Task("Run")
    .IsDependentOn("Test")
    .Does(() => {
        ElectronNetStart(projFile);
    });

Task("Publish-Win-x64")
	.IsDependentOn("Clean")
    .Does(() => {
        ElectronNetBuild(new ElectronNetBuildSettings{
            WorkingDirectory = projFile,
            ElectronTarget = ElectronTarget.Win,
            DotNetConfig = DotNetConfig.Release,
			PublishReadyToRun = PublishReadyToRunWin
        });

		if (DirectoryExists($"{ projFile }/bin/Desktop"))
		{
			var callerInfo = Context.GetCallerInfo();
			var pwd = callerInfo.SourceFilePath.ToString();
			pwd = pwd.Remove(pwd.Length - 11);
		
			Information($"{ pwd }");
			Information($"{ pwd }/src/fixingCakeTool/bin/Desktop");
			Information($"{ pwd }/output/win-x64");
			CopyDirectory($"{ pwd }/src/fixingCakeTool/bin/Desktop", $"{ outputFolder }/win-x64");
			CleanDirectory($"{ pwd }/output/win-x64");
		}
    });

Task("Publish-Linux-x64")
    .Does(() => {
        // looks like this is still failing too
        ElectronNetBuild(new ElectronNetBuildSettings{
            WorkingDirectory = projFile,
            ElectronTarget = ElectronTarget.Linux,
            DotNetConfig = DotNetConfig.Release,
			PublishReadyToRun = PublishReadyToRunLinux
        });
    });

Task("Publish-Osx-x64")
    .Does(() => {
        // cmd: electronize build /target osx /PublishReadyToRun false
        // but can still only be built on macos
        ElectronNetBuild(new ElectronNetBuildSettings{
            WorkingDirectory = projFile,
            ElectronTarget = ElectronTarget.MacOs,
            DotNetConfig = DotNetConfig.Release,
			PublishReadyToRun = PublishReadyToRunOSX
        });
    });

Task("Publish-Win-arm64")
    .Does(() => {
        // cmd: electronize build /target custom "win-arm64;win" /electron=arch arm64 /PublishReadyToRun false
        ElectronNetBuildCustom(new ElectronNetCustomBuildSettings{
			WorkingDirectory = projFile,
			ElectronTargetCustom = ElectronTargetCustom.PortableWinArm64,
			DotNetConfig = DotNetConfig.Release,
			ElectronArch = "arm64",
			PublishReadyToRun = false
		}); 
    });

Task("Publish-Win-x86")
    .Does(() => {
        // needs to be modified to run this
        // electronize build /target custom win7-x86;win32 /electron-arch ia32 
        ElectronNetBuildCustom(new ElectronNetCustomBuildSettings{
            WorkingDirectory = projFile,
            ElectronTargetCustom = ElectronTargetCustom.PortableWinX86,
            DotNetConfig = DotNetConfig.Release,
            ElectronArch = "ia32",
			PublishReadyToRun = PublishReadyToRunWin
        });
    });

Task("Publish-Linux-arm64")
    .Does(() => {
        // cmd: electronize build /target custom "win-arm64;win" /electron=arch arm64 /PublishReadyToRun false
        ElectronNetBuildCustom(new ElectronNetCustomBuildSettings{
			WorkingDirectory = projFile,
			ElectronTargetCustom = ElectronTargetCustom.PortableLinuxArm,
			DotNetConfig = DotNetConfig.Release,
			ElectronArch = "arm64",
			PublishReadyToRun = false
		}); 
    });

Task("Publish-Win")
    .IsDependentOn("Test")
    .IsDependentOn("Publish-Win-x64")
    .IsDependentOn("Publish-Win-arm64")
    .IsDependentOn("Publish-Win-x86");

Task("Publish-Linux")
    .IsDependentOn("Test")
    .IsDependentOn("Publish-Linux-x64")
    .IsDependentOn("Publish-Linux-arm64");


Task("Publish")
    .IsDependentOn("Test")
    .IsDependentOn("Publish-Linux")
    .IsDependentOn("Publish-Osx-x64")
    .IsDependentOn("Publish-Win");

Task("Default")
    .IsDependentOn("Run");

RunTarget(target);
