var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.BasicStarterExample_ApiService>("apiservice");

builder.AddProject<Projects.BasicStarterExample_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
