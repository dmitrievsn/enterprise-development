using FitnessClub.Application.Contracts.PersonalTrainingSessions;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// Контракт для работы с тренерами
/// </summary>
public interface ITrainerService:
    IApplicationService<
    TrainerDto,
    TrainerCreateUpdateDto,
    int>
{
    public Task<IList<PersonalTrainingSessionDto>> GetPersonalTrainingSessions(int id);
}