using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.Specializations;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с персональными тренировками
/// </summary>
public class PersonalTrainingSessionService(ILogger<PersonalTrainingSessionService> logger):
    IApplicationService<
    PersonalTrainingSessionDto,
    PersonalTrainingSessionCreateUpdateDto,
    int>
{
    public Task<PersonalTrainingSessionDto> Create(PersonalTrainingSessionCreateUpdateDto dto)
    {
        logger.LogInformation("Создание персональной тренировки");

        var client = FitnessClubData.Clients.FirstOrDefault(x => x.Id == dto.ClientId);

        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",dto.ClientId);

            throw new KeyNotFoundException($"Клиент с Id: {dto.ClientId} не найден");
        }

        var trainer = FitnessClubData.Trainers.FirstOrDefault(x => x.Id == dto.TrainerId);

        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",dto.TrainerId);

            throw new KeyNotFoundException($"Тренер с Id: {dto.TrainerId} не найден");
        }

        var newId = FitnessClubData.PersonalTrainingSessions.Count==0?1:FitnessClubData.PersonalTrainingSessions.Max(x => x.Id) + 1;

        var personalTrainingSession = new PersonalTrainingSession
        {
            Id = newId,
            Client = client,
            Trainer = trainer,
            TrainDay = dto.TrainDay,
            HallName = dto.HallName,
            IsTrial = dto.IsTrial
        };

        FitnessClubData.PersonalTrainingSessions.Add(personalTrainingSession);

        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            ToClientDto(client),
            ToTrainerDto(trainer),
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);

        logger.LogInformation("Id созданного персонального занятия: {Id}",personalTrainingSession.Id);

        return Task.FromResult(result);
    }

    public Task<PersonalTrainingSessionDto?> Get(int id)
    {
        logger.LogInformation("Получение персональной тренировки c Id: {Id}",id);

        var personalTrainingSession = FitnessClubData.PersonalTrainingSessions.FirstOrDefault(x => x.Id==id);

        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);

            return Task.FromResult<PersonalTrainingSessionDto?>(null);
        }

        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            ToClientDto(personalTrainingSession.Client),
            ToTrainerDto(personalTrainingSession.Trainer),
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);

        logger.LogInformation("Персональная тренировка c Id: {Id} найдена",id);

        return Task.FromResult<PersonalTrainingSessionDto?>(result);
    }

    public Task<IList<PersonalTrainingSessionDto>> GetAll()
    {
        logger.LogInformation("Получение списка персональных тренировок");

        IList<PersonalTrainingSessionDto> result = FitnessClubData.PersonalTrainingSessions
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

        return Task.FromResult(result);
    }

    public Task<PersonalTrainingSessionDto> Update(PersonalTrainingSessionCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление персональной тренировки с Id: {Id}",id);

        var personalTrainingSession = FitnessClubData.PersonalTrainingSessions.FirstOrDefault(x => x.Id == id);

        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);

            throw new KeyNotFoundException($"Персональная тренировка с Id: {id} не найдена");
        }

        var client = FitnessClubData.Clients.FirstOrDefault(x => x.Id == dto.ClientId);

        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",dto.ClientId);

            throw new KeyNotFoundException($"Клиент с Id: {dto.ClientId} не найден");
        }

        var trainer = FitnessClubData.Trainers.FirstOrDefault(x => x.Id == dto.TrainerId);

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

        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            ToClientDto(personalTrainingSession.Client),
            ToTrainerDto(personalTrainingSession.Trainer),
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);

        logger.LogInformation("Персональная тренировка с Id: {Id} обновлена",id);

        return Task.FromResult(result);
    }

    public Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление персональной тренировки с Id: {Id}",id);

        var personalTrainingSession = FitnessClubData.PersonalTrainingSessions.FirstOrDefault(x => x.Id == id);

        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);

            return Task.FromResult(false);
        }

        FitnessClubData.PersonalTrainingSessions.Remove(personalTrainingSession);

        logger.LogInformation("Персональная тренировка с Id: {Id} удалена",id);

        return Task.FromResult(true);
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