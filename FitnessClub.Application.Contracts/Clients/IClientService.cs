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
    public Task<IList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int id);
}