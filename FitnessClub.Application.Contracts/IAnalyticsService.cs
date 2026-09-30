using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts;

/// <summary>
/// Контракт для аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    public Task<IList<TrainerDto>> GetTrainersWithFiveOrMoreYearsOfExperience();

    public Task<bool> CheckHallAvailability(string hallName, DateTime checkTime);

    public Task<IList<ClientDto>> GetClientsWithExpiredSubscription();

    public Task<IList<PersonalTrainingSessionDto>> GetSessionsForCurrentMonthInSelectedHall(string hallName);

    public Task<IList<TrainerDto>> GetTopFivePopularTrainers();
}