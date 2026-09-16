namespace FitnessClub.Domain.Entities;

/// <summary>
/// Специализация тренера
/// </summary>
public class Specialization
{
    /// <summary>
    /// Уникальный id
    /// </summary>
    public required int Id {get; set; }

    /// <summary>
    /// Наименование специализации
    /// </summary>
    public required string Name {get; set; }
}