using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts;

/// <summary>
/// Контракт для аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает информацию о всех тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    IList<TrainerDto> GetTrainersWithFiveOrMoreYearsOfExperience();
    /// <summary>
    /// Проверяет, является ли зал доступным для записи в данный момент
    /// </summary>
    bool CheckHallAvailability(string hallName, DateTime checkTime);
    /// <summary>
    /// Выводит информацию о клиентах, у которых просрочен абонемент, упорядочена по ФИО
    /// </summary>
    IList<ClientDto> GetClientsWithExpiredSubscription();
    /// <summary>
    /// Выводит информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    IList<PersonalTrainingSessionDto> GetSessionsForCurrentMonthInSelectedHall(string hallName);
    /// <summary>
    /// Выводит топ 5 наиболее популярных тренеров
    /// </summary>
    IList<TrainerDto> GetTopFivePopularTrainers();
}