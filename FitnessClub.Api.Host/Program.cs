using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using FitnessClub.Infrastructure.Ef.Repositories;
using FitnessClub.Infrastructure.Ef;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddSqlServerDbContext<FitnessClubDbContext>(connectionName: "fitnessclub");
// Add services to the container.
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<ITrainerService, TrainerService>();

builder.Services.AddScoped<ISpecializationService, SpecializationService>();

builder.Services.AddScoped<
    IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>,
    PersonalTrainingSessionService>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddScoped<IRepository<Client, int>, ClientEfRepository>();
builder.Services.AddScoped<IRepository<Trainer, int>, TrainerEfRepository>();
builder.Services.AddScoped<IRepository<Specialization, int>, SpecializationEfRepository>();
builder.Services.AddScoped<IRepository<PersonalTrainingSession, int>, PersonalTrainingSessionEfRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
    await context.Database.MigrateAsync();
}

app.Run();
