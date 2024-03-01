var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

Task("CheckPackages")
    .Does(() => {
        DotNetTool("./config.csproj", "outdated");
});

Task("UpgradeAssistant")
    .Does(() => {
        // DotNetTool("./config.csproj", "upgrade-assistant", "upgrade");

});


Task("Default")
    .IsDependentOn("CheckPackages");


RunTarget(target); // this is going to run the default task if you don't pass in a --target argument
