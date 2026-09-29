using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Clients;

/// <summary>
/// dto для создания и обновления клиента
/// </summary>
public record ClientCreateUpdateDto(
    string PassportNumber,
    string LastName,
    string FirstName,
    string? Patronymic,
    Gender Gender,
    DateOnly DateOfBirth,
    string PhoneNumber,
    DateOnly StartSub,
    DateOnly EndSub);