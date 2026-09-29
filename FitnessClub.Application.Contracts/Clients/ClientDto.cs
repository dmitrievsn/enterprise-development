using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Clients;

/// <summary>
/// dto для клиента
/// </summary>
public record ClientDto(
    int Id,
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    string PhoneNumber,
    DateOnly StartSub,
    DateOnly EndSub);