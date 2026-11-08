using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using FitnessClub.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
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

builder.Services.AddScoped<IRepository<Client, int>,
    ClientRepository>();

builder.Services.AddScoped<IRepository<Trainer, int>,
    TrainerRepository>();

builder.Services.AddScoped<IRepository<Specialization, int>,
    SpecializationRepository>();

builder.Services.AddScoped<IRepository<PersonalTrainingSession, int>,
    PersonalTrainingSessionRepository>();

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

app.Run();
