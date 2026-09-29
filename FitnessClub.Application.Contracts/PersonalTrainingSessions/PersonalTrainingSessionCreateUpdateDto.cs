namespace FitnessClub.Application.Contracts.PersonalTrainingSessions;

/// <summary>
/// dto для создания и обновления персональной тренировки
/// </summary>
public record PersonalTrainingSessionCreateUpdateDto(
    int ClientId,
    int TrainerId,
    DateTime TrainDay,
    string HallName,
    bool IsTrial);