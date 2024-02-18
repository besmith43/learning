var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Run")
    .Does(() =>
{
    DotNetRun("./BasicCRUD.AppHost/BasicCRUD.AppHost.csproj", new DotNetRunSettings
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
    DotNetPublish("./BasicCRUD.AppHost/BasicCRUD.AppHost.csproj", new DotNetPublishSettings
    {
        Configuration = configuration
    });
});

Task("Publish-Web")
    .Does(() =>
{
    DotNetPublish("./BasicCRUD.AppHost/BasicCRUD.AppHost.csproj", new DotNetPublishSettings
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