using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Data;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для аналитических запросов
/// </summary>
public class AnalyticsService(ILogger<AnalyticsService> logger): IAnalyticsService
{
    /// <summary>
    /// Получает информацию о всех тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    public Task<IList<TrainerDto>> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        logger.LogInformation("Получение тренеров со стажем не менее 5 лет");

        IList<TrainerDto> result = FitnessClubData.Trainers
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
                new SpecializationDto(
                    trainer.Specialization.Id,
                    trainer.Specialization.Name),
                trainer.WorkYears ))     
            .ToList();

        logger.LogInformation("Получено тренеров со стажем не менее 5 лет: {Count}",result.Count);

        return Task.FromResult(result);
    }
    /// <summary>
    /// Проверяет, является ли зал доступным для записи в данный момент
    /// </summary>
    public Task<bool> CheckHallAvailability(string hallName, DateTime checkTime)
    {
        logger.LogInformation(
            "Проверка, является ли зал {hallName} доступным для записи в {checkTime}",
            hallName,
            checkTime);

        var isAvailable = !FitnessClubData.PersonalTrainingSessions
            .Any(session =>
                session.HallName == hallName &&
                session.TrainDay == checkTime);

        logger.LogInformation(
            "Доступность зала {HallName} на {CheckTime}: {IsAvailable}",
            hallName,
            checkTime,
            isAvailable);

        return Task.FromResult(isAvailable);
    }
    /// <summary>
    /// Выводит информацию о клиентах, у которых просрочен абонемент, упорядочена по ФИО
    /// </summary>
    public Task<IList<ClientDto>> GetClientsWithExpiredSubscription()
    {
        logger.LogInformation("Получение клиентов с просроченным абонементом");

        var today = DateOnly.FromDateTime(DateTime.Today);

        IList<ClientDto> result = FitnessClubData.Clients
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

        return Task.FromResult(result);
    }
    /// <summary>
    /// Выводит информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    public Task<IList<PersonalTrainingSessionDto>> GetSessionsForCurrentMonthInSelectedHall(string hallName)
    {
        logger.LogInformation("Получение информации о занятиях за текущий месяц, проходящих в зале {hallName}",hallName);
        
        var today = DateTime.Today;

        IList<PersonalTrainingSessionDto> result = FitnessClubData.PersonalTrainingSessions
            .Where(session =>
                session.HallName == hallName &&
                session.TrainDay.Year == today.Year &&
                session.TrainDay.Month == today.Month)
            .Select(p =>new PersonalTrainingSessionDto
            (
                p.Id,
                new ClientDto(
                    p.Client.Id,
                    p.Client.PassportNumber,
                    p.Client.LastName,
                    p.Client.FirstName,
                    p.Client.Patronymic,
                    p.Client.Gender,
                    p.Client.DateOfBirth,
                    p.Client.PhoneNumber,
                    p.Client.StartSub,
                    p.Client.EndSub),
                new TrainerDto(
                    p.Trainer.Id,
                    p.Trainer.PassportNumber,
                    p.Trainer.LastName,
                    p.Trainer.FirstName,
                    p.Trainer.Patronymic,
                    p.Trainer.Gender,
                    p.Trainer.DateOfBirth,
                    new SpecializationDto(
                        p.Trainer.Specialization.Id,
                        p.Trainer.Specialization.Name),
                    p.Trainer.WorkYears),
                p.TrainDay,
                p.HallName,
                p.IsTrial
                ))
            .ToList();

        logger.LogInformation("Получено занятий за текущий месяц в зале {HallName}: {Count}",hallName,result.Count);

        return Task.FromResult(result);
    }
    /// <summary>
    /// Выводит топ 5 наиболее популярных тренеров
    /// </summary>
    public Task<IList<TrainerDto>> GetTopFivePopularTrainers()
    {
        logger.LogInformation("Получение топ 5 наиболее популярных тренеров");

        var trainerIds = FitnessClubData.PersonalTrainingSessions
            .GroupBy(session => session.Trainer.Id)
            .OrderByDescending(group => group.Count())
            .Take(5)
            .Select(group => group.Key)
            .ToList();

        IList<TrainerDto> trainers = trainerIds
            .Select(id => FitnessClubData.Trainers
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
                new SpecializationDto(
                    trainer.Specialization.Id,
                    trainer.Specialization.Name),
                trainer.WorkYears ))   
            .ToList();

        logger.LogInformation("Получено популярных тренеров: {Count}",trainers.Count);

        return Task.FromResult(trainers);
    }
}
