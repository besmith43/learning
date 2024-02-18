var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Run")
    .Does(() =>
{
    DotNetRun("./BasicExample.AppHost/BasicExample.AppHost.csproj", new DotNetRunSettings
    {
        Configuration = configuration
    });
});


Task("Publish")
    .IsDependentOn("Publish-API")
    .IsDependentOn("Publish-Web");
    
Task("Publish-API")
    .Does(() =>
{
    DotNetPublish("./BasicExample.AppHost/BasicExample.AppHost.csproj", new DotNetPublishSettings
    {
        Configuration = configuration
    });
});

Task("Publish-Web")
    .Does(() =>
{
    DotNetPublish("./BasicExample.AppHost/BasicExample.AppHost.csproj", new DotNetPublishSettings
    {
        Configuration = configuration
    });
});


Task("Default")
    .IsDependentOn("Run");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);