var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Worker>("Worker");

builder.Build().Run();
