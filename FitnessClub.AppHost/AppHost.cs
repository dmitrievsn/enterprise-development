var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var database = builder.AddSqlServer("sql", password: sqlPassword)
    .WithDataVolume()
    .AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api_Host>("fitnessclub-api-host")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();