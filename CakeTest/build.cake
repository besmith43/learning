#addin nuget:?package=Cake.GitVersioning&version=3.3.37
#addin nuget:?package=Cake.Git&version=0.22.0


var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var solutionFolder = "./src";
var outputFolder = "./output";

Task("Clean")
    .Does(() => {
        CleanDirectory(outputFolder);
    });

Task("Restore")
    .Does(() => {
        DotNetCoreRestore(solutionFolder);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings{
            Configuration = configuration,
            NoRestore = true
        });
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTest(solutionFolder, new DotNetCoreTestSettings{
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Run")
    .IsDependentOn("Test")
    .Does(() => {
        var arguments = new ProcessArgumentBuilder();
        arguments.Append("--testFlag");
        arguments.Append("value");
        DotNetCoreRun("./src/CakeProj", arguments, new DotNetCoreRunSettings{
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() => {
        DotNetCorePack(solutionFolder, new DotNetCorePackSettings{
            Configuration = configuration,
            OutputDirectory = outputFolder,
            NoRestore = true,
            NoBuild = true
        });
    });

Task("Default")
    .IsDependentOn("Run");

RunTarget(target);