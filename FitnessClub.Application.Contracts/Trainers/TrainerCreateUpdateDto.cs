using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// dto для создания и обновления тренера
/// </summary>
/// <param name="PassportNumber">Номер паспорта тренера</param>
/// <param name="LastName">Фамилия тренера</param>
/// <param name="FirstName">Имя тренера</param>
/// <param name="Patronymic">Отчество тренера</param>
/// <param name="Gender">Пол тренера</param>
/// <param name="DateOfBirth">День рождения тренера</param>
/// <param name="SpecializationId">id cпециализации тренера</param>
/// <param name="WorkYears">Стаж работы тренера в годах</param>
public record TrainerCreateUpdateDto(
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    int SpecializationId,
    int WorkYears);