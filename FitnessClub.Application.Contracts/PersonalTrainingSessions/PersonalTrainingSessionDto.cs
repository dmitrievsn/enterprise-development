using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// dto для персональной тренировки
/// </summary>
public record PersonalTrainingSessionDto(
    int Id,
    ClientDto Client,
    TrainerDto Trainer,
    DateTime TrainDay,
    string HallName,
    bool IsTrial);