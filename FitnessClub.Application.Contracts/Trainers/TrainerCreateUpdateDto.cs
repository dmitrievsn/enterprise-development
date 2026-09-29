using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// dto для создания и обновления тренера
/// </summary>
public record TrainerCreateUpdateDto(
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    int SpecializationId,
    int WorkYears);