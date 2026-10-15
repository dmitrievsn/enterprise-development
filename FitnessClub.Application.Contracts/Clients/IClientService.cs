using FitnessClub.Application.Contracts.PersonalTrainingSessions;

namespace FitnessClub.Application.Contracts.Clients;

/// <summary>
/// Контракт для работы с клиентами
/// </summary>
public interface IClientService:
    IApplicationService<
    ClientDto,
    ClientCreateUpdateDto,
    int>
{
    /// <summary>
    /// Получает персональные тренировки клиента
    /// </summary>
    IList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int id);
}