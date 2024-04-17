#addin nuget:?package=Cake.Docker&version=1.2.0
#addin nuget:?package=Cake.Kubectl&version=1.0.0

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var arch = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();
var os = System.Runtime.InteropServices.RuntimeInformation.OSDescription.ToString();


Task("Clean")
    .Does(() =>
{
    CleanDirectory("./src/auth/output");
    CleanDirectory("./src/publicAPI/output");
    CleanDirectory("./src/privateAPI1/output");
    CleanDirectory("./src/privateAPI2/output");
    CleanDirectory("./src/privateAPI3/output");
    // CleanDirectory("./src/frontend/output");
});


Task("Build")
    .Does(() =>
{
    var settings = new DotNetPublishSettings
    {
        Configuration = "Release",
        SelfContained = true,
        OutputDirectory = "./src/auth/output"
    };

    Information($"Current Architecture: { arch }");

    if (arch.ToLower() == "arm64")
    {
        Information("Building for Arm64");
        settings.Runtime = "linux-arm64";
    }
    else
    {
        Information("Building for x86");
        settings.Runtime = "linux-x64";
    }

    DotNetPublish("./src/auth", settings);

    settings.OutputDirectory = "./src/publicAPI/output";
    DotNetPublish("./src/publicAPI", settings);

    settings.OutputDirectory = "./src/privateAPI1/output";
    DotNetPublish("./src/privateAPI1", settings);

    settings.OutputDirectory = "./src/privateAPI2/output";
    DotNetPublish("./src/privateAPI2", settings);

    settings.OutputDirectory = "./src/privateAPI3/output";
    DotNetPublish("./src/privateAPI3", settings);

    settings = new DotNetPublishSettings
    {
        Configuration = "Release",
        OutputDirectory = "./src/frontend/output"
    };
    DotNetPublish("./src/frontend", settings);
});


Task("Docker")
    .Does(() =>
{
    string[] tagName = { "auth" };
    var dockerSettings = new DockerImageBuildSettings
    {
        Rm = true,
        Pull = true,
        NoCache = true,
        Tag = tagName
    };
    DockerBuild(dockerSettings, "./src/auth");

    tagName[0] = "publicapi";
    dockerSettings.Tag = tagName;
    DockerBuild(dockerSettings, "./src/publicapi");

    tagName[0] = "privateapi1";
    dockerSettings.Tag = tagName;
    DockerBuild(dockerSettings, "./src/privateapi1");

    tagName[0] = "privateapi2";
    dockerSettings.Tag = tagName;
    DockerBuild(dockerSettings, "./src/privateapi2");

    tagName[0] = "privateapi3";
    dockerSettings.Tag = tagName;
    DockerBuild(dockerSettings, "./src/privateapi3");

    tagName[0] = "frontend";
    dockerSettings.Tag = tagName;
    DockerBuild(dockerSettings, "./src/frontend");
});


Task("Deploy")
    .Does(() =>
{
    var kubectlSettings = new KubectlApplySettings();

    kubectlSettings.Filename = "./src/auth/deploy.yml";
    KubectlApply(kubectlSettings);

    kubectlSettings.Filename = "./src/publicAPI/deploy.yml";
    KubectlApply(kubectlSettings);

    kubectlSettings.Filename = "./src/privateAPI1/deploy.yml";
    KubectlApply(kubectlSettings);

    kubectlSettings.Filename = "./src/privateAPI2/deploy.yml";
    KubectlApply(kubectlSettings);

    kubectlSettings.Filename = "./src/privateAPI3/deploy.yml";
    KubectlApply(kubectlSettings);

    kubectlSettings.Filename = "./src/frontend/deploy.yml";
    KubectlApply(kubectlSettings);
});


Task("StopKub")
    .Does(() =>
{
    var kubectlSettings = new KubectlDeleteSettings();

    // kubectlSettings.All = true;
    // kubectlSettings.AllNamespaces = true;
    // kubectlSettings.Selector = "app=auth";
    // KubectlDelete(kubectlSettings);

    kubectlSettings.Filename = "./src/auth/deploy.yml";
    // kubectlSettings.Selector = "app=auth";
    KubectlDelete(kubectlSettings);

    kubectlSettings.Filename = "./src/publicAPI/deploy.yml";
    // kubectlSettings.Selector = "app=publicapi";
    KubectlDelete(kubectlSettings);

    kubectlSettings.Filename = "./src/privateAPI1/deploy.yml";
    // kubectlSettings.Selector = "app=privateapi1";
    KubectlDelete(kubectlSettings);

    kubectlSettings.Filename = "./src/privateAPI2/deploy.yml";
    // kubectlSettings.Selector = "app=privateapi2";
    KubectlDelete(kubectlSettings);

    kubectlSettings.Filename = "./src/privateAPI3/deploy.yml";
    // kubectlSettings.Selector = "app=privateapi3";
    KubectlDelete(kubectlSettings);

    kubectlSettings.Filename = "./src/frontend/deploy.yml";
    // kubectlSettings.Selector = "app=frontend";
    KubectlDelete(kubectlSettings);
});


Task("DockerCompose")
    .Does(() =>
{
    DockerComposeUp(new DockerComposeUpSettings
    {
        Build = true,
        DetachedMode = true,
        Files = new string[] { "./docker-compose.yml" },
    });
});

Task("StopCompose")
    .Does(() =>
{
    DockerComposeDown(new DockerComposeDownSettings
    {
        Files = new string[] { "./docker-compose.yml" },
    });
});

Task("Clean&Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Build");

Task("UsingCompose")
    .IsDependentOn("Clean&Build")
    .IsDependentOn("DockerCompose");


Task("UsingKub")
    .IsDependentOn("Clean&Build")
    .IsDependentOn("Docker")
    .IsDependentOn("Deploy");


Task("Default")
    // .IsDependentOn("UsingCompose");
    .IsDependentOn("UsingKub");

RunTarget(target);