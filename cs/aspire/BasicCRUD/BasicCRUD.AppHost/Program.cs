var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.BasicCRUD_ApiService>("apiservice");

builder.AddProject<Projects.BasicCRUD_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
