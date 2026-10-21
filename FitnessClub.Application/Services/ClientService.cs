using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с клиентами
/// </summary>
public class ClientService(
    IRepository<Client, int> clientRepository,
    IRepository<PersonalTrainingSession, int> sessionRepository,
    ILogger<ClientService> logger):
    IClientService
{
    /// <inheritdoc/>
    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        logger.LogInformation("Создание клиента");
        if (dto.StartSub >= dto.EndSub)
        {
            throw new ArgumentException("Дата начала подписки должна быть раньше даты окончания подписки");
        }
        var client = new Client
        {
            Id = 0,
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
        clientRepository.Add(client);
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
        return result;
    }
    /// <inheritdoc/>
    public ClientDto? Get(int id)
    {
        logger.LogInformation("Получение клиента c Id: {Id}",id);
        var client = clientRepository.Get(id);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);
            return null;
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
        return result;
    }
    /// <inheritdoc/>
    public IList<ClientDto> GetAll()
    {
        logger.LogInformation("Получение списка клиентов");
        var result = clientRepository.GetAll()
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
        return result;
    }
    /// <inheritdoc/>
    public ClientDto Update(ClientCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление клиента с Id: {Id}",id);
        var client = clientRepository.Get(id);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);
            throw new KeyNotFoundException($"Клиент с Id: {id} не найден");
        }
        if (dto.StartSub >= dto.EndSub)
        {
            throw new ArgumentException("Дата начала подписки должна быть раньше даты окончания подписки");
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
        clientRepository.Update(client);
        var result = new ClientDto
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
        return result;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление клиента с Id: {Id}",id);
        var client = clientRepository.Get(id);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);
            return false;
        }
        var hasPersonalTrainingSessions = sessionRepository.GetAll().Any(x => x.Client.Id == id);
        if (hasPersonalTrainingSessions)
        {
            logger.LogWarning(
                "Клиент с Id: {Id} не может быть удален, у него есть персональные тренировки",id);
            return false;
        }
        var result = clientRepository.Delete(id);
        if (result)
        {
            logger.LogInformation("Клиент с Id: {Id} удалён",id);
        }
        return result;
    }
    /// <inheritdoc/>
    public IList<PersonalTrainingSessionDto> GetPersonalTrainingSessions(int id)
    {
        logger.LogInformation("Получение персональных тренировок клиента с Id: {Id}",id);
        var client = clientRepository.Get(id);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",id);
            throw new KeyNotFoundException($"Клиент с Id: {id} не найден");
        }
        var result = sessionRepository.GetAll().Where(p => p.Client.Id==id)
            .Select(p =>new PersonalTrainingSessionDto
            (
                p.Id,
                p.Client.Id,
                p.Trainer.Id,
                p.TrainDay,
                p.HallName,
                p.IsTrial
                ))
            .ToList();
        logger.LogInformation("Получено персональных тренировок клиента с Id {Id}: {Count}",id,result.Count);
        return result;
    }
}