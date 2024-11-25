var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddPostgres("postgres").PublishAsConnectionString();

var db = sqlServer.AddDatabase("Db");

builder.AddProject<Projects.Api>("api").WithReference(db);

builder.Build().Run();

