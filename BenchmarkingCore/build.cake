var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var outputFolder = "./output";
var selfcontainedOutputFolder = $"{ outputFolder }/self-contained";
var dependentOutputFolder = $"{ outputFolder }/framework-dependent";

var arch = "x86";
var OS = "win";

Task("Build-netcore3.1")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings{
            Configuration = configuration,
            Framework = "netcoreapp3.1",
            OutputDirectory = $"{ selfcontainedOutputFolder }/{ OS }-{ arch }",
            PublishSingleFile = true,
            SelfContained = true,
            Runtime = $"{ OS }-{ arch }"
        });
    });

Task("Build-net5.0")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild(solutionFolder, new DotNetCoreBuildSettings{
            Configuration = configuration,
            Framework = "net5.0",
            OutputDirectory = $"{ selfcontainedOutputFolder }/{ OS }-{ arch }"
        });
    });