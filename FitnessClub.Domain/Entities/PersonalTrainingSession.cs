namespace FitnessClub.Domain.Entities;

/// <summary>
/// Персональная тренировка
/// </summary>
public class PersonalTrainingSession
{
    /// <summary>
    /// Уникальный id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Клиент персональной тренировки
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Тренер персональной тренировки
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    /// Дата и время персональной тренировки
    /// </summary> 
    public required DateTime TrainDay { get; set; }

    /// <summary>
    /// Название зала
    /// </summary>
    public required string HallName { get; set; }

    /// <summary>
    /// Является ли занятие пробным
    /// </summary>
    public required bool IsTrial { get; set; }
}