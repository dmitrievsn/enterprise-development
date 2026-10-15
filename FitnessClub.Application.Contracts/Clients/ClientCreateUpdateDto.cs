using FitnessClub.Domain.Shared.Enums;

namespace FitnessClub.Application.Contracts.Clients;

/// <summary>
/// dto для создания и обновления клиента
/// </summary>
/// <param name="PassportNumber">Номер паспорта клиента</param>
/// <param name="LastName">Фамилия клиента</param>
/// <param name="FirstName">Имя клиента</param>
/// <param name="Patronymic">Отчество клиента</param>
/// <param name="Gender">Пол клиента</param>
/// <param name="DateOfBirth">День рождения клиента</param>
/// <param name="PhoneNumber">Номер телефона</param>
/// <param name="StartSub">Начало абонемента</param>
/// <param name="EndSub">Конец абонемента</param>
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