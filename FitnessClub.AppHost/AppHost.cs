var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddSqlServer("sql")

    .AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api_Host>("fitnessclub-api-host")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();