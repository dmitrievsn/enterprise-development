using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с клиентами
/// </summary>
public class ClientService(ILogger<ClientService> logger):
    IApplicationService<
    ClientDto,
    ClientCreateUpdateDto,
    int>
{
    public Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        logger.LogInformation("Создание клиента");

        var newId = FitnessClubData.Clients.Count==0?1:FitnessClubData.Clients.Max(x => x.Id) + 1;

        var client = new Client
        {
            Id = newId,
            PassportNumber = dto.PassportNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            StartSub = dto.StartSub,
            EndSub = dto.EndSub
        };

        FitnessClubData.Clients.Add(client);

        var result = new ClientDto(
            client.Id,
            client.PassportNumber,
            client.LastName,
            client.FirstName,
            client.Patronymic,
            client.Gender,
            client.DateOfBirth,
            client.PhoneNumber,
            client.StartSub,
            client.EndSub
        );

        logger.LogInformation("Id созданного клиента: {Id}",client.Id);

        return Task.FromResult(result);
    }

    public Task<ClientDto?> Get(int id)
    {
        logger.LogInformation("Получение клиента c Id: {Id}",id);

        var client = FitnessClubData.Clients.FirstOrDefault(x => x.Id==id);

        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);

            return Task.FromResult<ClientDto?>(null);
        }

        var result = new ClientDto(
            client.Id,
            client.PassportNumber,
            client.LastName,
            client.FirstName,
            client.Patronymic,
            client.Gender,
            client.DateOfBirth,
            client.PhoneNumber,
            client.StartSub,
            client.EndSub            
        );

        logger.LogInformation("Клиент c Id: {Id} найден",id);

        return Task.FromResult<ClientDto?>(result);
    }

    public Task<IList<ClientDto>> GetAll()
    {
        logger.LogInformation("Получение списка клиентов");

        IList<ClientDto> result = FitnessClubData.Clients
            .Select(client=>new ClientDto
            (
                client.Id,
                client.PassportNumber,
                client.LastName,
                client.FirstName,
                client.Patronymic,
                client.Gender,
                client.DateOfBirth,
                client.PhoneNumber,
                client.StartSub,
                client.EndSub))
            .ToList();

        logger.LogInformation("Получено клиентов: {Count}",result.Count);

        return Task.FromResult(result);
    }

    public Task<ClientDto> Update(ClientCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление клиента с Id: {Id}",id);

        var client = FitnessClubData.Clients.FirstOrDefault(x => x.Id==id);

        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);

            throw new KeyNotFoundException($"Клиент с Id: {id} не найден");
        }

        client.PassportNumber = dto.PassportNumber;
        client.LastName = dto.LastName;
        client.FirstName = dto.FirstName;
        client.Patronymic = dto.Patronymic;
        client.Gender = dto.Gender;
        client.DateOfBirth = dto.DateOfBirth;
        client.PhoneNumber = dto.PhoneNumber;
        client.StartSub = dto.StartSub;
        client.EndSub = dto.EndSub;

        var result= new ClientDto
        (
            client.Id,
            client.PassportNumber,
            client.LastName,
            client.FirstName,
            client.Patronymic,
            client.Gender,
            client.DateOfBirth,
            client.PhoneNumber,
            client.StartSub,
            client.EndSub);

        logger.LogInformation("Клиент с Id: {Id} обновлен",id);

        return Task.FromResult(result);
    }

    public Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление клиента с Id: {Id}",id);

        var client = FitnessClubData.Clients.FirstOrDefault(x => x.Id==id);

        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);

            return Task.FromResult(false);
        }
        var hasPersonalTrainingSessions = FitnessClubData.PersonalTrainingSessions.Any(x => x.Client.Id == id);

        if (hasPersonalTrainingSessions)
        {
            logger.LogWarning(
                "Клиент с Id: {Id} не может быть удален, у него есть персональные тренировки",id);

            return Task.FromResult(false);
        }

        FitnessClubData.Clients.Remove(client);

        logger.LogInformation("Клиент с Id: {Id} удален",id);

        return Task.FromResult(true);
    }
}