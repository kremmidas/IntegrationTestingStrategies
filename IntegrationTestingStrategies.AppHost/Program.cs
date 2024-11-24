var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlserver").PublishAsConnectionString();

var db = sqlServer.AddDatabase("Db");

builder.AddProject<Projects.Api>("api").WithReference(db);

builder.Build().Run();

builder.Build().Run();
