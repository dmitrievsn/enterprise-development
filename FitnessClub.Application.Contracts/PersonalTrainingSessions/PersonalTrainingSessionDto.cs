using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// dto для персональной тренировки
/// </summary>
/// <param name="Id">id персональной тренировки</param>
/// <param name="Client">dto клиента для персональной тренировка</param>
/// <param name="Trainer">dto тренера для персональной тренировка</param>
/// <param name="TrainDay">Время тренировки</param>
/// <param name="HallName">Название зала</param>
/// <param name="IsTrial">Пробная ли тренировка</param>
public record PersonalTrainingSessionDto(
    int Id,
    ClientDto Client,
    TrainerDto Trainer,
    DateTime TrainDay,
    string HallName,
    bool IsTrial);