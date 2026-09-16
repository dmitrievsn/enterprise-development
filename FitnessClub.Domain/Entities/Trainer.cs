namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер фитнес-клуба
/// </summary>
public class Trainer: Person
{
    /// <summary>
    /// Cпециализация тренера
    /// </summary>
    public required Specialization Specialization {get; set; }
    
    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public required int WorkYears {get;set; }
}