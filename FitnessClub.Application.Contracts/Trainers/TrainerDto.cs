using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Trainers;

/// <summary>
/// dto для тренера
/// </summary>
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