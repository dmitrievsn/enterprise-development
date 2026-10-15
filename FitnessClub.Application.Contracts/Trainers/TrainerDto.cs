using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// dto для тренера
/// </summary>
/// <param name="Id">id тренера</param>
/// <param name="PassportNumber">Номер паспорта тренера</param>
/// <param name="LastName">Фамилия тренера</param>
/// <param name="FirstName">Имя тренера</param>
/// <param name="Patronymic">Отчество тренера</param>
/// <param name="Gender">Пол тренера</param>
/// <param name="DateOfBirth">День рождения тренера</param>
/// <param name="Specialization">Специализация тренера</param>
/// <param name="WorkYears">Стаж работы тренера в годах</param>
public record TrainerDto(
    int Id,
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    SpecializationDto Specialization,
    int WorkYears);