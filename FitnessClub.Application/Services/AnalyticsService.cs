using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для аналитических запросов
/// </summary>
public class AnalyticsService(
    IRepository<Trainer, int> trainerRepository,
    IRepository<Client, int> clientRepository,
    IRepository<PersonalTrainingSession, int> sessionRepository,
    ILogger<AnalyticsService> logger)
    : IAnalyticsService
{
    /// <inheritdoc/>
    public IList<TrainerDto> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        logger.LogInformation("Получение тренеров со стажем не менее 5 лет");
        var result = trainerRepository.GetAll()
            .Where(trainer => trainer.WorkYears >= 5)
            .Select(trainer=>new TrainerDto
            (
                trainer.Id,
                trainer.PassportNumber,
                trainer.LastName,
                trainer.FirstName,
                trainer.Patronymic,
                trainer.Gender,
                trainer.DateOfBirth,
                trainer.Specialization.Id,
                trainer.WorkYears ))     
            .ToList();
        logger.LogInformation("Получено тренеров со стажем не менее 5 лет: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public bool CheckHallAvailability(string hallName, DateTime checkTime)
    {
        logger.LogInformation(
            "Проверка, является ли зал {hallName} доступным для записи в {checkTime}",
            hallName,
            checkTime);
        var sessions = sessionRepository.GetAll();
        if (!sessions.Any(session => session.HallName == hallName))
        {
            logger.LogWarning("Зал {HallName} не существует",hallName);
            throw new ArgumentException($"Зал {hallName} не существует");
        }
        var isAvailable = !sessionRepository.GetAll()
            .Any(session =>
                session.HallName == hallName &&
                session.TrainDay == checkTime);
        logger.LogInformation(
            "Доступность зала {HallName} на {CheckTime}: {IsAvailable}",
            hallName,
            checkTime,
            isAvailable);
        return isAvailable;
    }
    /// <inheritdoc/>
    public IList<ClientDto> GetClientsWithExpiredSubscription()
    {
        logger.LogInformation("Получение клиентов с просроченным абонементом");
        var today = DateOnly.FromDateTime(DateTime.Today);
        var result = clientRepository.GetAll()
            .Where(client => client.EndSub < today)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .Select(client=>new ClientDto
            (
                client.Id,
                client.PassportNumber,
                client.LastName,
                client.FirstName,
                client.Patronymic,
                client.Gender,
                client.DateOfBirth,
                client.PhoneNumber,
                client.StartSub,
                client.EndSub))
            .ToList();
        logger.LogInformation("Получено клиентов с просроченным абонементом: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public IList<PersonalTrainingSessionDto> GetSessionsForCurrentMonthInSelectedHall(string hallName)
    {
        logger.LogInformation("Получение информации о занятиях за текущий месяц, проходящих в зале {hallName}",hallName);
        var today = DateTime.Today;
        var result = sessionRepository.GetAll()
            .Where(session =>
                session.HallName == hallName &&
                session.TrainDay.Year == today.Year &&
                session.TrainDay.Month == today.Month)
            .Select(p =>new PersonalTrainingSessionDto
            (
                p.Id,
                p.Client.Id,
                p.Trainer.Id,
                p.TrainDay,
                p.HallName,
                p.IsTrial
                ))
            .ToList();
        logger.LogInformation("Получено занятий за текущий месяц в зале {HallName}: {Count}",hallName,result.Count);
        return result;
    }
    /// <inheritdoc/>
    public IList<TrainerDto> GetTopFivePopularTrainers()
    {
        logger.LogInformation("Получение топ 5 наиболее популярных тренеров");
        var trainerIds = sessionRepository.GetAll()
            .GroupBy(session => session.Trainer.Id)
            .OrderByDescending(group => group.Count())
            .Take(5)
            .Select(group => group.Key)
            .ToList();
        var allTrainers = trainerRepository.GetAll();
        var trainers = trainerIds
            .Select(id => allTrainers
                .First(trainer => trainer.Id == id))
            .Select(trainer=>new TrainerDto
            (
                trainer.Id,
                trainer.PassportNumber,
                trainer.LastName,
                trainer.FirstName,
                trainer.Patronymic,
                trainer.Gender,
                trainer.DateOfBirth,
                trainer.Specialization.Id,
                trainer.WorkYears ))   
            .ToList();
        logger.LogInformation("Получено популярных тренеров: {Count}",trainers.Count);
        return trainers;
    }
}
