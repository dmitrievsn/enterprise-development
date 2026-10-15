using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с персональными тренировками
/// </summary>
public class PersonalTrainingSessionService(
    IRepository<PersonalTrainingSession, int> sessionRepository,
    IRepository<Client, int> clientRepository,
    IRepository<Trainer, int> trainerRepository,
    ILogger<PersonalTrainingSessionService> logger)
    : IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>
{
    /// <inheritdoc/>
    public PersonalTrainingSessionDto Create(PersonalTrainingSessionCreateUpdateDto dto)
    {
        logger.LogInformation("Создание персональной тренировки");
        var client = clientRepository.Get(dto.ClientId);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",dto.ClientId);
            throw new KeyNotFoundException($"Клиент с Id: {dto.ClientId} не найден");
        }
        var trainer = trainerRepository.Get(dto.TrainerId);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",dto.TrainerId);
            throw new KeyNotFoundException($"Тренер с Id: {dto.TrainerId} не найден");
        }
        var personalTrainingSession = new PersonalTrainingSession
        {
            Id = 0,
            Client = client,
            Trainer = trainer,
            TrainDay = dto.TrainDay,
            HallName = dto.HallName,
            IsTrial = dto.IsTrial
        };
        sessionRepository.Add(personalTrainingSession);
        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            ToClientDto(client),
            ToTrainerDto(trainer),
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);
        logger.LogInformation("Id созданного персонального занятия: {Id}",personalTrainingSession.Id);
        return result;
    }
    /// <inheritdoc/>
    public PersonalTrainingSessionDto? Get(int id)
    {
        logger.LogInformation("Получение персональной тренировки c Id: {Id}",id);
        var personalTrainingSession = sessionRepository.Get(id);
        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);
            return null;
        }
        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            ToClientDto(personalTrainingSession.Client),
            ToTrainerDto(personalTrainingSession.Trainer),
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);
        logger.LogInformation("Персональная тренировка c Id: {Id} найдена",id);
        return result;
    }
    /// <inheritdoc/>
    public IList<PersonalTrainingSessionDto> GetAll()
    {
        logger.LogInformation("Получение списка персональных тренировок");
        var result = sessionRepository.GetAll()
            .Select(session=>new PersonalTrainingSessionDto
            (
                session.Id,
                ToClientDto(session.Client),
                ToTrainerDto(session.Trainer),
                session.TrainDay,
                session.HallName,
                session.IsTrial))
            .ToList();
        logger.LogInformation("Получено персональных тренировок: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public PersonalTrainingSessionDto Update(PersonalTrainingSessionCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление персональной тренировки с Id: {Id}",id);
        var personalTrainingSession = sessionRepository.Get(id);
        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);
            throw new KeyNotFoundException($"Персональная тренировка с Id: {id} не найдена");
        }
        var client = clientRepository.Get(dto.ClientId);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",dto.ClientId);
            throw new KeyNotFoundException($"Клиент с Id: {dto.ClientId} не найден");
        }
        var trainer = trainerRepository.Get(dto.TrainerId);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",dto.TrainerId);
            throw new KeyNotFoundException($"Тренер с Id: {dto.TrainerId} не найден");
        }
        personalTrainingSession.Client = client;
        personalTrainingSession.Trainer = trainer;
        personalTrainingSession.TrainDay = dto.TrainDay;
        personalTrainingSession.HallName = dto.HallName;
        personalTrainingSession.IsTrial = dto.IsTrial;
        sessionRepository.Update(personalTrainingSession);
        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            ToClientDto(personalTrainingSession.Client),
            ToTrainerDto(personalTrainingSession.Trainer),
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);
        logger.LogInformation("Персональная тренировка с Id: {Id} обновлена",id);
        return result;
    }
    /// <inheritdoc/>
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление персональной тренировки с Id: {Id}",id);
        var personalTrainingSession = sessionRepository.Get(id);
        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);
            return false;
        }
        var result = sessionRepository.Delete(id);
        if (result)
        {
            logger.LogInformation("Персональная тренировка с Id: {Id} удалена",id);
        }
        return result;
    }

    private static ClientDto ToClientDto(Client client)
    {
        return new ClientDto(
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
    }

    private static TrainerDto ToTrainerDto(Trainer trainer)
    {
        return new TrainerDto(
            trainer.Id,
            trainer.PassportNumber,
            trainer.LastName,
            trainer.FirstName,
            trainer.Patronymic,
            trainer.Gender,
            trainer.DateOfBirth,
            new SpecializationDto(
                trainer.Specialization.Id,
                trainer.Specialization.Name),
            trainer.WorkYears);
    }
}