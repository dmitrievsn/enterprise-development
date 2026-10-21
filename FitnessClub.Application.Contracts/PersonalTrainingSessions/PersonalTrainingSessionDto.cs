using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// dto для персональной тренировки
/// </summary>
/// <param name="Id">id персональной тренировки</param>
/// <param name="ClientId">id клиента</param>
/// <param name="TrainerId">id тренера</param>
/// <param name="TrainDay">Время тренировки</param>
/// <param name="HallName">Название зала</param>
/// <param name="IsTrial">Пробная ли тренировка</param>
public record PersonalTrainingSessionDto(
    int Id,
    int ClientId,
    int TrainerId,
    DateTime TrainDay,
    string HallName,
    bool IsTrial);