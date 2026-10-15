namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// dto для создания и обновления персональной тренировки
/// </summary>
/// <param name="ClientId">id клиента для персональной тренировка</param>
/// <param name="TrainerId">id тренера для персональной тренировка</param>
/// <param name="TrainDay">Время тренировки</param>
/// <param name="HallName">Название зала</param>
/// <param name="IsTrial">Пробная ли тренировка</param>
public record PersonalTrainingSessionCreateUpdateDto(
    int ClientId,
    int TrainerId,
    DateTime TrainDay,
    string HallName,
    bool IsTrial);