using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Общие хар-ки человека
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный id
    /// </summary>
    public required int Id {get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary> 
    public required string PassportNumber {get;set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName {get;set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName {get;set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? SecondName {get;set; }

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender {get;set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly DateOfBirth {get;set; }
}